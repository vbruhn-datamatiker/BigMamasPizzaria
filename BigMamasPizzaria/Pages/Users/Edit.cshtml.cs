using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BigMamasPizzaria.Pages.Users;

public class EditModel : PageModel
{
	private readonly UserRepository _userRepository;

	public EditModel(UserRepository userRepository) => _userRepository = userRepository;

	[BindProperty]
	public UserEditForm User { get; set; } = new();

	public IActionResult OnGet(int id)
	{
		var user = _userRepository.GetById(id);
		if (user == null)
		{
			return NotFound();
		}

		User = new UserEditForm
		{
			UserId = user.UserId,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			Phone = user.Phone
		};

		return Page();
	}

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var user = _userRepository.GetById(User.UserId);
		if (user == null)
		{
			return NotFound();
		}

		var updatedUser = new User
		{
			UserId = User.UserId,
			FirstName = User.FirstName,
			LastName = User.LastName,
			Email = User.Email,
			Phone = User.Phone,
			Password = user.Password // Keep existing password
		};

		_userRepository.UpdateUser(User.UserId, updatedUser);

		return RedirectToPage("Index");
	}
}

public class UserEditForm
{
	public int UserId { get; set; }

	[Required(ErrorMessage = "First name is required")]
	[StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
	public string FirstName { get; set; } = string.Empty;

	[Required(ErrorMessage = "Last name is required")]
	[StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
	public string LastName { get; set; } = string.Empty;

	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Please provide a valid email address")]
	public string Email { get; set; } = string.Empty;

	[Required(ErrorMessage = "Phone is required")]
	[Phone(ErrorMessage = "Please provide a valid phone number")]
	public string Phone { get; set; } = string.Empty;
}
