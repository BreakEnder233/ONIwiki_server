using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.Database.Models
{
    public class DataSet
    {
        [Key]
        public int id { get; set; }
        [MaxLength(24)]
        public string name { get; set; }
        public string hashedPassword { get; set; }
        public string data { get; set; }
    }
}
