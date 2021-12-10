using Microsoft.AspNetCore.Mvc;
using SAFIB.Models;
using SAFIB.Services;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SAFIB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAController : ControllerBase
    {
        private Aservice aservice;

        //Получение зависимостей через конструктор
        public ServiceAController(Aservice _aservice)
        {
            aservice = _aservice;
        }

        //Получение полного списка подразделений
        [HttpGet]
        public IEnumerable<Subvision> Get()
        {

            return aservice.subvisions.ToList();
        }

        //Получение списка подразделений по имени
        [HttpGet("name={name}")]
        public IEnumerable<Subvision> Get(string name)
        {
            return aservice.subvisions.Where(n => n.Name == name).ToList();
        }

        //Получения подразделения по ID
        [HttpGet("id={id}")]
        public Subvision Get(int id)
        {
            return aservice.subvisions.FirstOrDefault(i => i.ID == id);
        }
    }
}
