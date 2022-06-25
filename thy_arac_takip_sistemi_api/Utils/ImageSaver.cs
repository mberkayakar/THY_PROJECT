using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace thy_arac_takip_sistemi_api.Utils
{
    public class ImageSaver
    {


        public void SaveImage(IFormFile file)
        {
            try
            {
                string uploadFolder = "thy_images";
                // If directory does not exist, create it

                // 1. get the file form the request
                var postedFile = file;
                // 2. set the file uploaded folder
                System.Console.WriteLine(uploadFolder);
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                // 3a. read the file name of the received file
                var fileName = ContentDispositionHeaderValue.Parse(postedFile.ContentDisposition)
                    .FileName.Trim('"');
                // 3b. save the file on Path
                //  var finalPath = Path.Combine(uploadFolder, DateTime.Now.ToString("yyyyMMddHHmmss") + fileName);
                var finalPath = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(finalPath, FileMode.Create))
                {
                    postedFile.CopyTo(fileStream);
                }
                return;



            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return;
            }
            //fileModel save to db

        }
    }
}
