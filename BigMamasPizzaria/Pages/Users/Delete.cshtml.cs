using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages.Users;

public class DeleteModel : PageModel
{
	private readonly UserRepository _userRepository;

	public DeleteModel(UserRepository userRepository) => _userRepository = userRepository;

	public User? User { get; set; }

	public IActionResult OnGet(int id)
	{
		User = _userRepository.GetById(id);
		if (User == null)
		{
			return NotFound();
		}

		return Page();
	}

	public IActionResult OnPost(int id)
	{
		var user = _userRepository.GetById(id);
		if (user == null)
		{
			return NotFound();
		}

		_userRepository.RemoveUser(id);

		return RedirectToPage("Index");
	}
}
