using System;

namespace CHS.Infrastructure.ViewModels
{
    public class StudentHistoryViewModel
    {
        public string UserId { get; set; }
        public string SubscriptionName { get; set; }
        /// <summary>
        /// Use for Student to get Name of the Trainer or Gym
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Use for Student to now which one paid to...Trainer or Gym
        /// </summary>
        public string ForGymOrTrainer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
