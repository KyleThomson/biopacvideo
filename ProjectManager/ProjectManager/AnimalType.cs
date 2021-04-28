using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class AnimalType
    {
        public string ID;
        public List<WeightType> WeightInfo;
        public List<SeizureType> Sz;
        public List<MealType> Meals;
        public List<ImportantDateType> ImportantDates;
        public List<BloodDrawType> BloodDraws;
        public List<RemovalType> Removals;
        public GroupType Group;
        public List<InjectionType> Injections;
        public List<SzMetrics> metrics;
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
            metrics = new List<SzMetrics>();
        }
    }
}
