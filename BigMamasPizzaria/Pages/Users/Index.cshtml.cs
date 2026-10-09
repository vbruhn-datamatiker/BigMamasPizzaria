using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages.Users;

public class IndexModel : PageModel
{
    private readonly UserRepository _userRepository;

    public IndexModel(UserRepository userRepository) => _userRepository = userRepository;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public List<User> Users { get; set; } = new();

    public void OnGet()
    {
        var allUsers = _userRepository.ListAllUsers();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            Users = allUsers.Where(u =>
                u.FirstName.Contains(Search, StringComparison.OrdinalIgnoreCase) ||
                u.LastName.Contains(Search, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Contains(Search, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
        else
        {
            Users = allUsers;
        }
    }
}