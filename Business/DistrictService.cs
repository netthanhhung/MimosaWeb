using Dal;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class DistrictService
    {
        public static List<District> GetDistricts()
        {
            using (var context = new MimosaEntities())
            {
                return context.Districts.AsNoTracking().OrderBy(x => x.DistrictCode).ToList();
            }
        }
    }
}
