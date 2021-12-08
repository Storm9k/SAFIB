using Microsoft.AspNetCore.Mvc;
using SAFIB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SAFIB.Services;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SAFIB.Controllers
{
    public class HomeController : Controller
    {
        private Bservice bservice;
        private readonly IHostingEnvironment env;
        public HomeController(Bservice _bservice, IHostingEnvironment _env)
        {
            bservice = _bservice;
            env = _env;
        }
        public IActionResult Index()
        {

            return View(bservice.GetSubvisions());
        }

        public IActionResult Sync()
        {
            using (FileStream fs = new FileStream($"{env.ContentRootPath}/Subvision.json", FileMode.OpenOrCreate))
            {
                var serialize_option = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                JsonSerializer.SerializeAsync<List<Subvision>>(fs, bservice.SubvisionsResult, serialize_option);
            }

            return View();
        }

    }
}
