using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Dal.Models
{
    public partial class EduPortalContext: DbContext

    {
        public EduPortalContext()
        {
        }

        public EduPortalContext(DbContextOptions<EduPortalContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=blxjl1jwddgoyrqgkke1-mysql.services.clever-cloud.com;port=3306;database=blxjl1jwddgoyrqgkke1;user=uhs1xtg46mbaud0f;password=ivFaPXiTd9MHycYNJioJ",
                    new MySqlServerVersion(new Version(8,0,13))
                );
            }
        }

        public virtual DbSet<YeshivaStudent> YeshivaStudent { get; set; }

        public virtual DbSet<Graduate> Graduate { get; set; }

    }
}
