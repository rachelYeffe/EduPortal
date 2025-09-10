using EduPortal.Bl.Interfaces;
using EduPortal.Dal;
using EduPortal.Dal.Models;
using EduPortal.Dal.Services;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;

namespace EduPortal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "server=blxjl1jwddgoyrqgkke1-mysql.services.clever-cloud.com;port=3306;database=blxjl1jwddgoyrqgkke1;user=uhs1xtg46mbaud0f;password=ivFaPXiTd9MHycYNJioJ";
            builder.Services.AddDbContext<EduPortalContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)))
            );

            builder.Services.AddScoped<DalManager>();
            builder.Services.AddScoped(typeof(IYeshivaStudent), typeof(Bl.Services.YeshivaStudent));
            builder.Services.AddScoped(typeof(IGraduate), typeof(Bl.Services.GraduateService));
            builder.Services.AddScoped(typeof(IExcelImport), typeof(Bl.Services.ExcelImportService));
            builder.Services.AddScoped(typeof(ISearch), typeof(Bl.Services.SearchService));

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigin",
                                  policy =>
                                  {
                                      policy.WithOrigins(
                                              "http://localhost:4200", // dev server
                                              "https://eduportal-front.onrender.com" // production
                                          )
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // CORS צריך להיות ראשון
            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
