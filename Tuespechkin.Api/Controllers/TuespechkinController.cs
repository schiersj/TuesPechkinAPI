using System.Drawing.Printing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TuesPechkin;

namespace Tuespechkin.Api.Controllers
{
    public class TuesPechkinController : ApiController
    {
        private static readonly IConverter PdfConverter = 
            new ThreadSafeConverter(new RemotingToolset<PdfToolset>(new WinAnyCPUEmbeddedDeployment(new TempFolderDeployment())));

        [HttpGet]
        public IHttpActionResult Hello()
        {
            return Ok("Hello");
        }

        // GET api/tuespeckin?url=http://www.google.com
        public HttpResponseMessage Get(string url)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    PaperSize = PaperKind.Letter,
                    Margins =
                    {
                        Top = 0.25,
                        Left = 0.125,
                        Right = 0.125,
                        Bottom = 0.25,
                        Unit = Unit.Inches
                    }
                },
                Objects = { new ObjectSettings { PageUrl = url } }
            };

            var pdf = PdfConverter.Convert(doc);
            return ReturnBytes(pdf);
        }

        private static HttpResponseMessage ReturnBytes(byte[] bytes)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/octet-stream")
                    }
                }
            };
        }
    }
}
