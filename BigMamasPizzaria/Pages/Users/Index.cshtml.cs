using BMPClassLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BigMamasPizzaria.Pages.Users;

public class IndexModel : PageModel
{
    private readonly UserRepository _users;

    public IndexModel(UserRepository users) => _users = users;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public void OnGet()
    {
        // TODO: filter _users by Search
    }
}