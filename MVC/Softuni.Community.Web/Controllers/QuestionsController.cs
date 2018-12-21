using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Controllers
{
    public class QuestionsController : ApiController
    {
        private readonly IDiscussionsService discussionsService;

        public QuestionsController(IDiscussionsService discussionsService)
        {
            this.discussionsService = discussionsService;
        }

        // Like 
        // POST api/questions
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<QuestionRatingBindingModel> Post(QuestionRatingBindingModel bindingModel)
        {
            var question = discussionsService.RateQuestion(bindingModel);
            if (question == null)
            {
                return NotFound();
            }
            return bindingModel;
        }
    }
}
