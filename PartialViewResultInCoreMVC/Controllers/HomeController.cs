using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PartialViewResultInCoreMVC.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace PartialViewResultInCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            return View();
        }

        //SHow to Prevent the Partial Action Method from being invoked via normal GET and POST Requests
        [AjaxOnly]
        public ActionResult Details(int ProductId)
        {
            string method = HttpContext.Request.Method;

            string requestedWith = HttpContext.Request.Headers["X-Requested-With"];

            if (method == "POST")
            {
                if (requestedWith == "XMLHttpRequest")
                {
                    Product product = new Product()
                    {
                        ProductID = 101,
                        Name = "Project 101"
                    };
                    return PartialView("_ProductDetailsPartialView", product);
                }
            }
            //Create a Partial View to return Invalid Request
            return PartialView("_InvalidPartialView");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}