using EduPortal.Dal.Interfaces;
using EduPortal.Dal.Models;
using EduPortal.Dal.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace EduPortal.Dal
{
    public class DalManager
    {
        public IYeshivaStudent YeshivaStudents { get; set; }
        public IGraduate Graduates { get; set; }
        public DalManager()
        {
            ServiceCollection collections = new ServiceCollection();
            collections.AddSingleton<EduPortalContext>();
            collections.AddSingleton<IYeshivaStudent, YeshivaStudentService>();
            collections.AddSingleton<IGraduate, GraduateService>();

            var serviceprovider = collections.BuildServiceProvider();
            YeshivaStudents = serviceprovider.GetRequiredService<IYeshivaStudent>();
            Graduates = serviceprovider.GetRequiredService<IGraduate>();
    

        }

    }
}
