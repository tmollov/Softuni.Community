using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Controllers
{
    public class JokesController : ApiController
    {
        private readonly IJokesService jokesService;
        private readonly UserManager<CustomUser> userMgr;

        public JokesController(IJokesService jokesService, UserManager<CustomUser> userMgr)
        {
            this.jokesService = jokesService;
            this.userMgr = userMgr;
        }

        // Like 
        // POST api/answers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<JokeRatingBindingModel> Post(JokeRatingBindingModel bindingModel)
        {
            var user = userMgr.FindByNameAsync(bindingModel.Username).Result;
            var joke = jokesService.RateJoke(bindingModel, user);
            if (joke == null)
            {
                return NotFound();
            }
            return bindingModel;
        }
    }
}
