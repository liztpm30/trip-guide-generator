using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using trip_guide_generator.Model;
using trip_guide_generator.Services;

namespace trip_guide_generator.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICosmosDBService _cosmosDB; 

    public IndexModel(ILogger<IndexModel> logger, ICosmosDBService cosmosDB)
    {
        _logger = logger;
        _cosmosDB = cosmosDB;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public User? User { get; set; }

    [BindProperty]
    public Guide? Guide { get; set; }

    [BindProperty]
    public DayActivity? DayActivity { get; set; }

    public async Task<IActionResult> OnPost()
    {
        if (DayActivity != null && Guide != null && User != null)
        {
            Guide.DayActivities = new DayActivity[1];
            DayActivity.Id = Guide.DayActivities.Count().ToString();
            Guide.DayActivities[0] = DayActivity;
            Guide.Id = Guide.DayActivities.Count().ToString();

            User.Guides = new Guide[1];
            User.Guides[0] = Guide;
            User.Id = Guid.NewGuid().ToString();

            _logger.LogInformation("********* The USER object ************");
            string strjson = JsonSerializer.Serialize<User>(User);
            _logger.LogInformation(strjson);

            _logger.LogInformation("********* The Guide object ************");
            string guidestrjson = JsonSerializer.Serialize<Guide>(Guide);
            _logger.LogInformation(guidestrjson);

            try
            {
                await _cosmosDB.AddUserAsync(User);
            }
            catch (Exception e)
            {
                _logger.LogError("user data could not be saved");
            }
        }

        return RedirectToPage("Index");
    }
}

