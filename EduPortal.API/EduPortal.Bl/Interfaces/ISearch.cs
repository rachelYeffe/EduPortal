using EduPortal.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Bl.Interfaces
{
    public interface ISearch
    {
        Task<List<SearchResult>> SearchGraduateAndGraduateByPhone(List<ChildDetails> listNumOfChild,List<Graduate> listGradute, List<YeshivaStudent> listYeshivaStudent);
        
    }
}
