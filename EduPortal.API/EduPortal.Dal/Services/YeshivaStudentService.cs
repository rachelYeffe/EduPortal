using EduPortal.Dal.Interfaces;
using EduPortal.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Dal.Services
{
    public class YeshivaStudentService : IYeshivaStudent
    {

        private readonly EduPortalContext context;

        public YeshivaStudentService(EduPortalContext context)
        {
            this.context = context;
        }
        public async Task<List<YeshivaStudent>> GetYeshivaStudents()
        {
            List<YeshivaStudent> YeshivaStudentList = await context.YeshivaStudent.ToListAsync();
            if (YeshivaStudentList != null ||YeshivaStudentList.Count > 0)
                    return YeshivaStudentList;
                else
                    return null;
        }



        public async Task<YeshivaStudent> GetYeshivaStudentById(string studentId)
        {
            try
            {
                YeshivaStudent yeshivaStudentById = await context.YeshivaStudent.FirstOrDefaultAsync(student => student.IdNumber == studentId);
                if (yeshivaStudentById == null)
                {
                    return null;
                }
                return yeshivaStudentById;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<YeshivaStudent> CreateYeshivaStudent(YeshivaStudent yeshivaStudent)
        {
            try
            {
                var existing = await this.context.YeshivaStudent.FindAsync(yeshivaStudent.IdNumber);
                if (existing != null)
                {
                    return existing;
                }
                await this.context.YeshivaStudent.AddAsync(yeshivaStudent);
                await this.context.SaveChangesAsync();
                return yeshivaStudent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
