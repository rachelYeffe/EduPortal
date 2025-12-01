using EduPortal.Bl.Interfaces;
using EduPortal.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduPortal.Dto.Models;

namespace EduPortal.Bl.Services
{
    public class YeshivaStudent : IYeshivaStudent

    {
        private DalManager dalManager;
        readonly IMapper mapper;

        public YeshivaStudent(IMapper mapper, DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dal.Models.YeshivaStudent, Dto.Models.YeshivaStudent>();
                cfg.CreateMap<Dto.Models.YeshivaStudent, Dal.Models.YeshivaStudent>();

            });
            this.mapper = config.CreateMapper();

        }
        public async Task<Dto.Models.YeshivaStudent> GetYeshivaStudentById(string id)
        {
            Dal.Models.YeshivaStudent student = await dalManager.YeshivaStudents.GetYeshivaStudentById(id);
            if (student != null)
                return mapper.Map<Dto.Models.YeshivaStudent>(student);
            else
                return null;
        }

        public async Task<List<Dto.Models.YeshivaStudent>> GetYeshivaStudents()
        {
            var studentsFromDal = await dalManager.YeshivaStudents.GetYeshivaStudents();
            if (studentsFromDal == null || studentsFromDal.Count() <= 0)
                return null;
            else
                return mapper.Map<List<Dto.Models.YeshivaStudent>>(studentsFromDal);

        }
        public async Task<Dto.Models.YeshivaStudent> CreateYeshivaStudent(Dto.Models.YeshivaStudent yeshivaStudent)
        {
            try
            {
                var exists=await dalManager.YeshivaStudents.CreateYeshivaStudent(mapper.Map<Dal.Models.YeshivaStudent>(yeshivaStudent));
                if(exists==null)
                    return null ;
                return mapper.Map<Dto.Models.YeshivaStudent>(yeshivaStudent);
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

    }
}
