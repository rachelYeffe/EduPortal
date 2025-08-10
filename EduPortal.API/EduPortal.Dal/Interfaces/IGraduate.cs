using EduPortal.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Dal.Interfaces
{
    public interface IGraduate
    {
        Task<List<Graduate>> GetGraduates(); 
        Task<Graduate> GetGraduateById(string id);
        Task<Graduate> CreateGraduate(Graduate graduate);

    }
}
