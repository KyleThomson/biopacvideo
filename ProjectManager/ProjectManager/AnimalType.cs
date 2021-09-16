using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager
{
    public class AnimalType
    {
        public string ID;
        public DateTime earliestAppearance, latestAppearance;
        public List<WeightType> WeightInfo;
        public List<SeizureType> Sz;
        public List<MealType> Meals;
        public List<ImportantDateType> ImportantDates;
        public List<BloodDrawType> BloodDraws;
        public List<RemovalType> Removals;
        public GroupType Group;
        public List<InjectionType> Injections;
        // align
        public double alignBy7Days;
        public AnimalType()
        {
            Sz = new List<SeizureType>();
            WeightInfo = new List<WeightType>();
            Meals = new List<MealType>();
            ImportantDates = new List<ImportantDateType>();
            Group = new GroupType();
            BloodDraws = new List<BloodDrawType>();
            Removals = new List<RemovalType>();
            Injections = new List<InjectionType>();
        }

        public List<double> GetInjectionTimes(string group, DateTime Earliest, double align)
        {
            List<double> groupTimes = new List<double>(); // initialize output

            // search injections for matching group
            foreach (InjectionType injection in Injections)
            {
                // check if the group is correct
                if (injection.ADDID != group) continue;

                // if it is then add to injection times
                groupTimes.Add((float)Math.Round(injection.TimePoint.Subtract(Earliest).TotalHours / 24 - align, 2));
            }

            // sort injection times in case some are out of order
            List<double> sortedTimes = groupTimes.OrderBy(g => g).ToList();

            return sortedTimes;
        }
    }
}
