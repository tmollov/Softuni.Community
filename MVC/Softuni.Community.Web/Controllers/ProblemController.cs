using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Web.Controllers
{
    public class ProblemController : ApiController
    {
        private readonly IProblemsService problemsService;
        private readonly UserManager<CustomUser> userMgr;

        public ProblemController(IProblemsService problemsService, UserManager<CustomUser> userMgr)
        {
            this.problemsService = problemsService;
            this.userMgr = userMgr;
        }

        // Get Problem Details 
        // GET api/problem
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<ProblemDetailsViewModel> Get()
        {
            // TODO: GET Random Problem
            var problem = problemsService.GetRandomProblem();
            return problem;
        }
    }
}
