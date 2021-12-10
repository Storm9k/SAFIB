using Microsoft.AspNetCore.Mvc;
using SAFIB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Net.Http;
using Newtonsoft.Json;

namespace SAFIB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment env;
        private AppDbContext dbContext;
        static readonly HttpClient httpClient = new HttpClient();

        //Получение зависимостей через конструктор
        public HomeController(IHostingEnvironment _env, AppDbContext context)
        {
            //bservice = _bservice;
            env = _env;
            dbContext = context;
        }
        //Действие контроллера на получения полного списка
        public async Task<IActionResult> Index()
        {
            return View(await GetSubvisions());
        }

        //Действие контроллера на поиск организации по названию
        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            return View(await GetSubvisions(name));
        }

        //Действие контроллера сохранения подразделений
        public async Task<IActionResult> Sync()
        {
            using (FileStream fs = new FileStream($"{env.ContentRootPath}/Subvision.json", FileMode.OpenOrCreate))
            {
                var serialize_option = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                await System.Text.Json.JsonSerializer.SerializeAsync<List<Subvision>>(fs,await GetSubvisions(), serialize_option);
            }

            return View();
        }

        //Выгрузка состояний из WEB API сервиса А
        private async Task<List<Subvision>> GetSubvisions()
        {
            HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://localhost:44316/api/servicea"));
            List<Subvision> SubvisionsList = JsonConvert.DeserializeObject<List<Subvision>>(await response.Content.ReadAsStringAsync());

            foreach (Subvision subvision in SubvisionsList)
            {
                if (subvision.SubjectionID != null) subvision.Subjection = SubvisionsList.FirstOrDefault(s => s.ID == subvision.SubjectionID);
            }
            return SubvisionsList;
        }

        //Выгрузка состояний из WEB API сервиса А по имени
        private async Task<List<Subvision>> GetSubvisions(string name)
        {
            HttpResponseMessage response = await httpClient.GetAsync(new Uri($"https://localhost:44316/api/servicea/name={name}"));
            List<Subvision> SubvisionsList = JsonConvert.DeserializeObject<List<Subvision>>(await response.Content.ReadAsStringAsync());

            foreach (Subvision subvision in SubvisionsList)
            {
                if (subvision.SubjectionID != null)
                {
                    HttpResponseMessage rps = await httpClient.GetAsync(new Uri($"https://localhost:44316/api/servicea/id={subvision.SubjectionID}"));
                    subvision.Subjection = JsonConvert.DeserializeObject<List<Subvision>>(await response.Content.ReadAsStringAsync())[0];
                }
            }
            return SubvisionsList;
        }

    }
}
