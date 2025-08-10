using EduPortal.Bl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduPortal.Dto.Models;

namespace EduPortal.Bl.Services
{
    public class SearchService : ISearch
    {

        public async Task<List<SearchResult>> SearchGraduateAndGraduateByPhone(
                                                                                List<ChildDetails> listNumOfChild,
                                                                                List<Graduate> listGraduate,
                                                                                List<Dto.Models.YeshivaStudent> listYeshivaStudent)
        {

            var results = new List<SearchResult>();

            foreach (ChildDetails child in listNumOfChild)
            {
                var childPhones = new[] { child.Phone, child.FatherPhone }
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Select(NormalizePhone)
                    .ToList();

                var graduateMatches = listGraduate.Where(g =>
                {
                    var graduatePhones = GetGraduatePhones(g);
                    return graduatePhones.Any(p => childPhones.Contains(p));
                }).ToList();

                var yeshivaMatches = listYeshivaStudent
                    .Where(y => !string.IsNullOrWhiteSpace(y.Phone) &&
                                childPhones.Contains(NormalizePhone(y.Phone)))
                    .ToList();

                if (graduateMatches.Any() || yeshivaMatches.Any())
                {
                    results.Add(new SearchResult
                    {
                        Child = child,
                        GraduateMatch = graduateMatches,
                        YeshivaStudentMatch = yeshivaMatches
                    });

                }
            }
            return results;
        }

        private List<string> GetGraduatePhones(Graduate g)
        {
            return new[]
            {
            g.MobilePhone,
            g.HomePhone,
            g.AddHomePhone,
            g.FatherPhone,
            g.FatherBusinessPhone,
            g.AddFatherBusinessPhone
        }
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(NormalizePhone)
            .ToList();
        }

        private string NormalizePhone(string phone)
        {
            return phone?.Trim().Replace("-", "").Replace(" ", "");
        }

    }
}