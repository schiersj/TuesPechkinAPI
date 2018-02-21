using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using TuesPechkin.Service;

namespace TuesPechkin.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Screenshot()
        {
            var url = Url.Action("Index", "Home", routeValues: null, protocol: Request.Url.Scheme);
            var pdf = TuesPechkinService.ConvertToPdf(Uri.Local, url);
            if (pdf == null) return HttpNotFound();
            return File(pdf, MediaTypeNames.Application.Pdf);
        }

        public async Task<ActionResult> ScreenshotAsync()
        {
            var url = Url.Action("Index", "Home", routeValues: null, protocol: Request.Url.Scheme);
            var pdf = await TuesPechkinService.ConvertToPdfAsync(Uri.Local, url);
            if (pdf == null) return HttpNotFound();
            return File(pdf, MediaTypeNames.Application.Pdf);
        }
    }
}
