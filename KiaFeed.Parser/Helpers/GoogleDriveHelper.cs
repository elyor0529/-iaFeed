
namespace KiaFeed.Parser.Helpers
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v3;
    using Google.Apis.Drive.v3.Data;
    using Google.Apis.Services;
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;

    public class GoogleDriveHelper
    {

        private readonly static string[] scopes = new string[]
        {
            DriveService.Scope.Drive,
            DriveService.Scope.DriveFile,
            DriveService.Scope.DriveAppdata,
            DriveService.Scope.DriveMetadata
        };

        private static string GetMimeType(string fileName)
        {
            var mimeType = "application/octet-stream";
            var ext = System.IO.Path.GetExtension(fileName).ToLower();
            var regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = (string)regKey.GetValue("Content Type");

            return mimeType;
        }

        public static async Task<DriveService> AuthenticateServiceAccount(string serviceAccountEmail, string keyFilePath)
        {
            if (!System.IO.File.Exists(keyFilePath))
            {
                throw new System.IO.FileNotFoundException("An Error occurred - Key file does not exist");
            }

            var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);

            try
            {
                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = scopes
                }.FromCertificate(certificate));

                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Kia feed"
                });

                return await Task.FromResult(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<File> UploadFile(DriveService service, string filePath, string parentId)
        {

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException("File does not exist: " + filePath);
            }

            try
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
                {

                    var file = new File
                    {
                        Name = System.IO.Path.GetFileNameWithoutExtension(stream.Name),
                        MimeType = "application/vnd.google-apps.spreadsheet"
                    };

                    if (!string.IsNullOrWhiteSpace(parentId))
                    {
                        file.Parents = new string[]
                        {
                            parentId
                        };
                    }

                    var request = service.Files.Create(file, stream, GetMimeType(stream.Name));

                    request.Fields = "id";

                    await request.UploadAsync();

                    return request.ResponseBody;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<File> UpdateFile(DriveService service, string filePath,string fileId, string parentId)
        {

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException("File does not exist: " + filePath);
            }

            try
            {
                using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
                {

                    var file = new File
                    {
                        Name = System.IO.Path.GetFileNameWithoutExtension(stream.Name),
                        MimeType = "application/vnd.google-apps.spreadsheet"
                    };

                    if (!string.IsNullOrWhiteSpace(parentId))
                    {
                        file.Parents = new string[]
                        {
                            parentId
                        };
                    }

                    var request = service.Files.Create(file, stream, GetMimeType(stream.Name));

                    request.Fields = "id";

                    await request.UploadAsync();

                    return request.ResponseBody;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
         
        public static async Task<IList<File>> GetFiles(DriveService service)
        {
            try
            {
                var list = new List<File>();
                var pageToken = "";

                do
                {
                    var request = service.Files.List();

                    request.Spaces = "drive";
                    request.Fields = "nextPageToken, files(id, name)";
                    request.PageToken = pageToken;

                    var result = await request.ExecuteAsync();

                    list.AddRange(result.Files);

                    pageToken = result.NextPageToken;

                } while (!String.IsNullOrWhiteSpace(pageToken));

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
