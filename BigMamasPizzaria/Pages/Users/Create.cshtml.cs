using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BigMamasPizzaria.Pages.Users;

public class CreateModel : PageModel
{
	private readonly UserRepository _userRepository;

	public CreateModel(UserRepository userRepository) => _userRepository = userRepository;

	[BindProperty]
	public UserForm User { get; set; } = new();

	public IActionResult OnGet()
	{
		return Page();
	}

	public IActionResult OnPost()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		// Generate next UserId
		int nextId = _userRepository.ListAllUsers().Count > 0
			? _userRepository.ListAllUsers().Max(u => u.UserId) + 1
			: 1;

		var newUser = new User
		{
			UserId = nextId,
			FirstName = User.FirstName,
			LastName = User.LastName,
			Email = User.Email,
			Phone = User.Phone,
			Password = User.Password
		};

		_userRepository.AddUser(newUser);

		return RedirectToPage("Index");
	}
}

public class UserForm
{
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

	[Required(ErrorMessage = "Password is required")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
	public string Password { get; set; } = string.Empty;
}
