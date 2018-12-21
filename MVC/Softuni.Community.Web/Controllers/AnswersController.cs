using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Controllers
{
    public class AnswersController : ApiController
    {
        private readonly IDiscussionsService discussionsService;

        public AnswersController(IDiscussionsService discussionsService)
        {
            this.discussionsService = discussionsService;
        }

        // Like 
        // POST api/answers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<AnswerRatingBindingModel> Post(AnswerRatingBindingModel bindingModel)
        {
            var answer = discussionsService.RateAnswer(bindingModel);
            if (answer == null)
            {
                return NotFound();
            }
            return bindingModel;
        }
    }
}
