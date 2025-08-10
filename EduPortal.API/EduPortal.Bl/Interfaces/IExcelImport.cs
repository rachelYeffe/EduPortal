using EduPortal.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Bl.Interfaces
{
    public interface IExcelImport
    {
        Task ImportIGraduateFromExcel(Stream fileStream);
        Task ImportYeshivaStudentsFromExcel(Stream fileStream);
        Task<List<ChildDetails>> ExtractPhonePairs(
            Stream fileStream,
            string phoneHeader = null,
            string fatherPhoneHeader = null,
            string houseNumberHeader = null,
            string firstNameHeader = null,
            string lastNameHeader = null,
            string fatherNameHeader = null,
            string classNameHeader = null,
            string addressHeader = null);
    }

}
