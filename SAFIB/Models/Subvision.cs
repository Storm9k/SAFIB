using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAFIB.Models
{
    public class Subvision
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public bool Status { get; set; }
        public int? SubjectionID { get; set; }
        //public Subvision Subjection { get; set; }
    }
}
