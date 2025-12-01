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
    public class GraduateService : IGraduate
    {
        private readonly EduPortalContext context;

        public GraduateService(EduPortalContext context)
        {
            this.context = context;
        }
        public async Task<List<Graduate>> GetGraduates()
        {
            List<Graduate> GraduateList = await context.Graduate.ToListAsync();
            if (GraduateList != null || GraduateList.Count() > 0)
                return GraduateList;
            else
                return null;

        }

        public async Task<Graduate> GetGraduateById(string id)
        {
            try
            {
                Graduate GraduateById = await context.Graduate.FirstOrDefaultAsync(Graduat => Graduat.IDNumber == id);
                if (GraduateById == null)
                {
                    return null;
                }
                return GraduateById;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Graduate> CreateGraduate(Graduate graduate)
        {
            try
            {
                var existing = await this.context.Graduate.FindAsync(graduate.IDNumber);
                if (existing != null)
                {
                    return null;
                }
                await this.context.Graduate.AddAsync(graduate);
                await this.context.SaveChangesAsync();
                return graduate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }



}

