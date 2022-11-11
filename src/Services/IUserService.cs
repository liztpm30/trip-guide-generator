using System;
using trip_guide_generator.Model;

namespace trip_guide_generator.Services
{
	public interface IUserService
	{
		Task AddUser(AppUser user);
		Task DeleteUser(string userId);
		Task EditUser(AppUser user);
		Task<AppUser> GetUserbyId(string id);
		Task<AppUser> GetUserbyUserName(string userName);
		Task AddDayPlanToGuide(string userName, string guideId, DayPlan plan);
		Task AddActivityToPlan(string userName, string guideId, int dayNumber, Activity act);
		Task EditGuide(string userName, Guide guide);
    }
}

