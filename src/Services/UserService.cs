using System;
using System.Numerics;
using System.Security.Cryptography;
using Microsoft.Azure.Cosmos;
using trip_guide_generator.Model;

namespace trip_guide_generator.Services
{
	public class UserService : IUserService
	{
		private ICosmosDBService _dbService;
		private ILogger<UserService> _logger;

		public UserService(ICosmosDBService dbService, ILogger<UserService> logger)
		{
			_dbService = dbService;
			_logger = logger;

        }

        public async Task AddUser(AppUser user)
        {
            //Validate user has the required data
            if(user.UserName != null && user.Guides != null)
            {
                //Define the user id
                user.Id = Guid.NewGuid().ToString();
                //Add user to the DB
                await _dbService.AddUserAsync(user);

                _logger.LogInformation($"User {user.UserName} has been added");
            }
            else
            {
                throw new AppException("User is missing at least one of the required data fields: userName or Guides");
            }
            
        }

        public async Task DeleteUser(string userId)
        {
            await _dbService.DeleteUserAsync(userId);
            _logger.LogInformation($"User with ID: {userId} has been deleted");
        }

        public async Task EditUser(AppUser user)
        {
            await _dbService.UpdateUserAsync(user.Id, user);
            _logger.LogInformation($"User {user.UserName} has been updated");
        }

        public async Task<AppUser> GetUserbyId(string id)
        {
            return await _dbService.GetUserByIdAsync(id);
        }

        public async Task<AppUser> GetUserbyUserName(string userName)
        {
            return await _dbService.GetUserByUserNameAsync(userName);
        }

        public async Task AddGuideToUser (Guide guide, string userName)
        {
            //Get the user object to be modified from the DB
            var user = await GetUserbyUserName(userName);

            //Modify the object
            user.Guides.Add(guide);

            //Update user record from the DB
            await EditUser(user);
        }

        public async Task AddDayPlanToGuide (string userName, string guideId, DayPlan plan)
        {
            var guideFound = false; //Flag to identify if guide exists

            //Get the user object to be modified from the DB
            var user = await GetUserbyUserName(userName);

            //Find the guide to be modified
            foreach(var guide in user.Guides)
            {
                if (guide.Id == guideId)
                {
                    guideFound = true;
                    guide.PlanPerDay.Add(plan);
                    break;
                }
            }

            if (!guideFound)
                throw new AppException($"Guide {guideId} could not be found for user {userName}");

            //After plan is added, update the user db object
            await EditUser(user);

        }

        public async Task AddActivityToPlan (string userName, string guideId, int dayNumber, Activity act)
        {
            var guideFound = false; //Flag to identify if guide exists

            //Get the user object to be modified from the DB
            var user = await GetUserbyUserName(userName);

            //Find the guide to be modified
            foreach (var guide in user.Guides)
            {
                if (guide.Id == guideId)
                {
                    guideFound = true;

                    //Check if the dayNumber is valid for this guide
                    if (guide.PlanPerDay.Count > dayNumber)
                    {
                        guide.PlanPerDay[dayNumber - 1].Activities.Add(act);

                    }
                    else
                    {
                        throw new AppException($"Guide {guideId} for user {userName} does not have a plan for day number {dayNumber}");
                    }
                    break;
                }
            }

            if (!guideFound)
                throw new AppException($"Guide {guideId} could not be found for user {userName}");

            //After plan is added, update the user db object
            await EditUser(user);

        }

        
        public async Task EditGuide(string userName, Guide guide)
        {
            //Get the user object to be modified from the DB
            var user = await GetUserbyUserName(userName);

            //Find the guide to be modified
            var userGuide = user.Guides.FirstOrDefault(g => g.Id == guide.Id);

            if (userGuide != null)
            {
                //Edit the guide object
                userGuide = guide;
            }
            else
            {
                throw new AppException($"Guide {guide.Id} could not be found for user {userName}");
            }

            //After guide is modified, update the user db object
            await EditUser(user);

        }

        public async Task EditDayPlan(string userName, string guideId, DayPlan plan)
        {
            //Get the user object to be modified from the DB
            var user = await GetUserbyUserName(userName);

            //Find the guide to be modified
            var userGuide = user.Guides.FirstOrDefault(g => g.Id == guideId);

            if (userGuide != null)
            {
                //Check if day plan exists
                if(userGuide.PlanPerDay.Count > plan.DayNumber)
                {
                    userGuide.PlanPerDay[plan.DayNumber - 1] = plan;
                }
            }
            else
            {
                throw new AppException($"Guide {guideId} could not be found for user {userName}");
            }

            //After guide is modified, update the user db object
            await EditUser(user);

        }

        public async Task EditActivity(string userName, string guideId, int dayNumber, Activity act)
        {
            //Get the user object to be modified from the DB
            var user = await GetUserbyUserName(userName);

            //Find the guide to be modified
            var userGuide = user.Guides.FirstOrDefault(g => g.Id == guideId);

            if (userGuide != null)
            {
                //Get the dayPlan object
                var userAct = userGuide.PlanPerDay[dayNumber - 1].Activities.FirstOrDefault(a => a.Id == act.Id);
                //Edit the activity object
                userAct = act;
            }
            else
            {
                throw new AppException($"Guide {guideId} could not be found for user {userName}");
            }

            //After guide is modified, update the user db object
            await EditUser(user);

        }

    }
}

