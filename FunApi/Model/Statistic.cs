using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Model
{
    public class Statistic
    {
        private static double _avgNameLength;
        
        private static GeneratedName _shortestName { get; set; }
        
        private static GeneratedName _longestName { get; set; }

        private static List<GeneratedName> _generatedNamesToday { get; set; }
        public static double CalculateAvgNameLength(List<GeneratedName> nameList)
        {
            _avgNameLength = 0;
            int sum = 0;
            foreach (var name in nameList)
            {
                sum += name.Name.Length;
            }
            _avgNameLength = sum / nameList.Count;
            return _avgNameLength;
        }

        public static GeneratedName GetShortestName(List<GeneratedName> nameList)
        {
            var sortedNames = nameList.OrderBy(n => n.Name.Length);
            _shortestName = sortedNames.FirstOrDefault();
            return _shortestName;
        }

        public static GeneratedName GetLongestName(List<GeneratedName> nameList)
        {
            var sortedNames = nameList.OrderBy(n => n.Name.Length);
            _longestName = sortedNames.LastOrDefault();
            return _longestName;
        }

        public static List<GeneratedName> GetNamesGeneratedToday(List<GeneratedName> nameList)
        {
            var todayDate = DateTime.Now.ToUniversalTime();
            if (_generatedNamesToday == null)
            {
                _generatedNamesToday = new List<GeneratedName>();
            }
            _generatedNamesToday.Clear();
            foreach(var name in nameList)
            {
                if (name.GeneratedDate.Date == todayDate.Date)
                {
                    _generatedNamesToday.Add(name);
                }
            }
            return _generatedNamesToday;
        }
    }
}
