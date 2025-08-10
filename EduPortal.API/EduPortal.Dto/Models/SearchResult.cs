using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Dto.Models
{
    public class SearchResult
    {
        public ChildDetails Child { get; set; }
        public List<Graduate>? GraduateMatch { get; set; }
        public List<YeshivaStudent>? YeshivaStudentMatch { get; set; }
    }
}
