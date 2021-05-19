using System.Collections.Generic;

namespace CHS.Web.Helpers
{
    public enum Gender
    {
        male, female, other
    }

    public static class Filters
    {
        public static ICollection<string> TrainerFilters = new List<string>
        {
            "Fitness Trainer",
            "Weight Lifting",
            "Cycling",
            "Boxer",
            "Karate",
            "Judo",
            "Martial Arts",
        };
    }

}
