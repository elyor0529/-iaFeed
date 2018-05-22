namespace KiaFeed.Parser.Jobs
{
    using KiaFeed.Parser.Helpers;
    using Quartz;
    using System.IO;
    using System.Web;
     
    public class DocJob : IJob
    {

        /// <summary>
        /// https://github.com/LindaLawton/Google-Dotnet-Samples/tree/master/Google-Drive
        /// </summary>
        /// <param name="context"></param>
        public async void Execute(IJobExecutionContext context)
        {
            // connect with a Service Account
            const string serviceAccountEmail = "854132074015-compute@developer.gserviceaccount.com";
            var serviceAccountkeyFile = Path.Combine(HttpRuntime.AppDomainAppPath, "Temp", "My Project-b1363c7ae5f1.p12");
            var service = await GoogleDriveHelper.AuthenticateServiceAccount(serviceAccountEmail, serviceAccountkeyFile);
            var uploadFile = Path.Combine(HttpRuntime.AppDomainAppPath, "Temp", "Kia  feed#1.xlsx");

            await GoogleDriveHelper.UploadFile(service, uploadFile, "Kia vehicle feeds");
        }
    }
}