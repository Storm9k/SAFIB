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
        public ServiceAController(Aservice _aservice)
        {
            aservice = _aservice;
        }

        [HttpGet]
        public IEnumerable<Subvision> Get()
        {

            return aservice.subvisions.ToList();
        }

        [HttpGet("name={name}")]
        public IEnumerable<Subvision> Get(string name)
        {
            return aservice.subvisions.Where(n => n.Name == name).ToList();
        }

        [HttpGet("id={id}")]
        public Subvision Get(int id)
        {
            return aservice.subvisions.FirstOrDefault(i => i.ID == id);
        }
    }
}
