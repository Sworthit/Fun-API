using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Model
{
    public class Statistic
    {
        public static double? AvgNameLength { get; set; }
        
        public static GeneratedName ShortestName { get; set; }
        
        public static GeneratedName LongestName { get; set; }

        public static double CalculateAvgNameLength(List<GeneratedName> nameList)
        {
            if (AvgNameLength == null)
            {
                int sum = 0;
                foreach (var name in nameList)
                {
                    sum += name.Name.Length;
                }
                AvgNameLength = sum / nameList.Count;
            }
            return AvgNameLength.Value;
        }

        public static GeneratedName GetShortestName(List<GeneratedName> nameList)
        {
            if (ShortestName == null)
            {
                var minName = nameList[0];
                foreach (var name in nameList)
                {
                    if (name.Name.Length < minName.Name.Length)
                    {
                        minName = name;
                    }
                }
                ShortestName = minName;
            }
            return ShortestName;
        }

        public static GeneratedName GetLongestName(List<GeneratedName> nameList)
        {
            if (LongestName == null)
            {
                var longName = nameList[0];
                foreach (var name in nameList)
                {
                    if (name.Name.Length > longName.Name.Length)
                    {
                        longName = name;
                    }
                }
                LongestName = longName;
            }
            return LongestName;
        }
    }
}
