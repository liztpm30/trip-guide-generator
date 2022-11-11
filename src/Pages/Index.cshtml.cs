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
    private readonly IUserService _userService; 

    public IndexModel(ILogger<IndexModel> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public AppUser? User { get; set; }

    [BindProperty]
    public Guide? Guide { get; set; }

    [BindProperty]
    public Activity? Activity { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userService.GetUserbyUserName("liztesting");

        var guide = user.Guides[0];

        guide.GuideName = "This is the guide";

        await _userService.EditGuide("liztesting", guide);

        //if (Activity != null && Guide != null && User != null)
        //{
        //    //This will be the first activity
        //    Activity.Id = "1";

        //    var dayPlan = new DayPlan();
        //    dayPlan.Id = "1";
        //    dayPlan.DayNumber = 1;
        //    dayPlan.Activities = new List<Activity>
        //    {
        //        Activity
        //    };

        //    Guide.PlanPerDay = new List<DayPlan>
        //    {
        //        dayPlan
        //    };
        //    Guide.Id = "1";

        //    User.Guides = new List<Guide>
        //    {
        //        Guide
        //    };
        //    User.Id = Guid.NewGuid().ToString();

        //    try
        //    {
        //        await _userService.AddUser(User);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError("user data could not be saved");
        //    }
        //}

        return RedirectToPage("Index");
    }
}

