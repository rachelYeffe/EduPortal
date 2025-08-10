using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Dal.Models
{
    public class YeshivaStudent
    {
        public string FullName { get; set; }
        public string Phone { get; set; }

        [Key]
        public string IdNumber { get; set; }
        public string Address { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
    }


}
