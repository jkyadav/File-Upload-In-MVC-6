using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Runtime;
using Microsoft.Net.Http.Headers;

namespace FileUploadSample.Controllers
{
    public class HomeController : Controller
    {
		IApplicationEnvironment hostingEnvironment;
		public HomeController(IApplicationEnvironment _hostingEnvironment)
		{
			hostingEnvironment = _hostingEnvironment;
		}

		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Index(IList<IFormFile> files)
		{
			foreach (var file in files)
			{
				var fileName = ContentDispositionHeaderValue
					.Parse(file.ContentDisposition)
					.FileName
					.Trim('"');
				var filePath = hostingEnvironment.ApplicationBasePath + "\\Documents\\" + DateTime.Now.ToString("yyyyddMHHmmss") + fileName;
				file.SaveAs(filePath);
			}

			return Index();
		}

		public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}