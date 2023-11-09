using System;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
	{
		public ErrorController()
		{
		}

		[Route("403")]
		public IActionResult Forbidden()
		{
			return View("Forbidden");
		}

        [Route("404")]
        public IActionResult NotFound()
		{
			return View("NotFound");
		}

		[Route("752")]
		public IActionResult SomethingWentWrong()
		{
			return View("SomethingWentWrong");
		}
	}
}

