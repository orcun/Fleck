using System.Web.Mvc;
using MvcApp.Framework;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebSocketServer _server;

        public HomeController(IWebSocketServer server)
        {
            _server = server;
        }

        public ActionResult Index()
        {
            _server.Publish("test");
            return View();
        }
    }
}