using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Controllers
{
    public class QuestionsController : ApiController
    {
        private readonly IDiscussionsService discussionsService;
        private readonly UserManager<CustomUser> userMgr;

        public QuestionsController(IDiscussionsService discussionsService, UserManager<CustomUser> userMgr)
        {
            this.discussionsService = discussionsService;
            this.userMgr = userMgr;
        }

        // Like 
        // POST api/questions
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<QuestionRatingBindingModel> Post(QuestionRatingBindingModel bindingModel)
        {
            var user = userMgr.FindByNameAsync(bindingModel.Username).Result;
            var question = discussionsService.RateQuestion(bindingModel,user);
            if (question == null)
            {
                return NotFound();
            }
            return bindingModel;
        }
    }
}
