using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TicketsBasket.Functions.ThumpGenerator
{
    public static class ThumpGenerator
    {
        [FunctionName("ThumpGenerator")]
        public static void Run([BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob,
                               [Blob("images-thumbs/{name}", FileAccess.Write)]Stream thumpStream,                    
                               string name, 
                               ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            // Check extension 
            var allowedExtensions = new[] { ".jpg", ".png", ".bmp" };
            string extension = Path.GetExtension(name); 
            if(!allowedExtensions.Contains(extension))
            {
                log.LogError($"{name} blob is not a valid image");
                return; 
            }

            var image = Image.FromStream(myBlob);
            // Calculate the width and the height 
            int newWidth = 200;
            double ratio = Convert.ToDouble(image.Width) / Convert.ToDouble(image.Height);
            int newHeight = Convert.ToInt32(Math.Round(newWidth / ratio, 0));

            var bitmap = new Bitmap(image);
            var thumpImage = bitmap.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            thumpImage.Save(thumpStream, image.RawFormat);
            log.LogInformation($"Thump for {name} has been created with the dimensions {newWidth} x {newHeight}"); 
        }
    }
}
