namespace KiaFeed.Parser.Controllers
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using Google.GData.Client;
    using Google.GData.Spreadsheets;
    using KiaFeed.Parser.Helpers;
    using KiaFeed.Parser.Models;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var service = await GoogleDriveHelper.AuthenticateServiceAccount("kia-feed@kia-feed-1275.iam.gserviceaccount.com", HttpRuntime.AppDomainAppPath + "Kia feed-69b23139ac76.p12");
            var filePath = HttpRuntime.AppDomainAppPath + "Kia feeds.xlsx";
            var file = await GoogleDriveHelper.UploadFile(service, filePath, "");
            var files = await GoogleDriveHelper.GetFiles(service);

            return View();
        }

    }
}