using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadApp.Controllers
{
    public class HomeController : Controller
    {
        public static string[] Negs = {"That's the money shot", "Not bad... for a newbie.",
            "Is that all you've got?!", "Nice job!"};

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            String filePath = String.Empty;

            var dtn = DateTime.Now.ToString("HH-mm-ss");

            // count uploaded files
            int n = 0;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filePath = Path.GetTempFileName();

                    using (var stream = new FileStream("/home/jh/Pictures/" + dtn + 
                            filePath.Substring(filePath.LastIndexOf("/")+1), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    n++;
                }
            }

            ViewData["fileCount"] = n;
            ViewData["neg"] = Negs[n % Negs.Count()];
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
