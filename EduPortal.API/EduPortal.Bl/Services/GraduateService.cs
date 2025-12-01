using AutoMapper;
using EduPortal.Bl.Interfaces;
using EduPortal.Dal;
using EduPortal.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Bl.Services
{
    public class GraduateService : IGraduate
    {

        private DalManager dalManager;
        readonly IMapper mapper;

        public GraduateService(IMapper mapper, DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dal.Models.Graduate, Dto.Models.Graduate>();
                cfg.CreateMap<Dto.Models.Graduate, Dal.Models.Graduate>();

            });
            this.mapper = config.CreateMapper();

        }
        public async Task<Graduate> GetGraduateById(string id)
        {
            Dal.Models.Graduate graduate = await dalManager.Graduates.GetGraduateById(id);
            if (graduate != null)
                return mapper.Map<Dto.Models.Graduate>(graduate);
            else
                return null;
        }

        public async Task<List<Graduate>> GetGraduates()
        {
            var graduateList = await dalManager.Graduates.GetGraduates();
            if (graduateList == null || graduateList.Count() <= 0)
                return null;
            else
                return mapper.Map<List<Graduate>>(graduateList);


        }
        public async Task<Graduate?> CreateGraduate(Graduate graduate)
        {
            try
            {
                var exists = await dalManager.Graduates.CreateGraduate(mapper.Map<Dal.Models.Graduate>(graduate)); 
                if (exists==null)
                    return null;

                return mapper.Map<Graduate>(graduate); 
            }
            catch (Exception)
            {
                throw;
            }
        }
    

    }
}
