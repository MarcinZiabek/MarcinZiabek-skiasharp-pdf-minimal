using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace SkiasharpMinimalPdf.Controllers
{
    [ApiController]
    [Route("generation")]
    public class HomeController : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult Get()
        {
            // input stream
            using var stream = new MemoryStream();
            
            // initialize
            var document = SKDocument.CreatePdf(stream);
            
            // begin page
            var canvas = document.BeginPage(400, 300);

            // draw
            using var paint = new SKPaint
            {
                Color = SKColors.DarkGreen,
                TextAlign = SKTextAlign.Center
            };
            
            canvas.DrawRect(50, 50, 300, 150, paint);
            canvas.DrawText("This is test", 200, 250, paint);
            
            // end page
            document.EndPage();
            canvas.Dispose();
            
            // end document
            canvas.Dispose();
            document.Close();
            document.Dispose();
            
            // result
            return File(stream.ToArray(), "application/pdf", "test.pdf");
        }
    }
}