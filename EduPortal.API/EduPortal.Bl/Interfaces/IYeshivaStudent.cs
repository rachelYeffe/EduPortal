using EduPortal.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Bl.Interfaces
{
    public interface IYeshivaStudent
    {
        Task<List<YeshivaStudent>> GetYeshivaStudents();
        Task<YeshivaStudent> GetYeshivaStudentById(string id);
        Task<YeshivaStudent> CreateYeshivaStudent(YeshivaStudent yeshivaStudent);

    }
}
