using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace LISTR
{
    public static class DesignTimeHouseBinding
    {
        public static House HouseSampleData
        {
            get
            {
                return MainWindow.houses.AsQueryable().ToList()[0];
            }
        }

        public static List<House> HouseSampleList
        {
            get
            {
                return MainWindow.houses.AsQueryable().ToList();
            }
        }
    }
}