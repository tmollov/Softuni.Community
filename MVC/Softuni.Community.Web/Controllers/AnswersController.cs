using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Controllers
{
    public class AnswersController : ApiController
    {
        private readonly IDiscussionsService discussionsService;
        private readonly UserManager<CustomUser> userMgr;

        public AnswersController(IDiscussionsService discussionsService, UserManager<CustomUser> userMgr)
        {
            this.discussionsService = discussionsService;
            this.userMgr = userMgr;
        }

        // Like 
        // POST api/answers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [Authorize]
        public ActionResult<AnswerRatingBindingModel> Post(AnswerRatingBindingModel bindingModel)
        {
            var user = userMgr.FindByNameAsync(bindingModel.Username).Result;
            var answer = discussionsService.RateAnswer(bindingModel,user);
            if (answer == null)
            {
                return NotFound();
            }
            return bindingModel;
        }
    }
}
