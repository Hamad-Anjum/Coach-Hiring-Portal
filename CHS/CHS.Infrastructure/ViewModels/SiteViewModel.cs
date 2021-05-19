using System;
using System.Collections.Generic;

namespace CHS.Infrastructure.ViewModels
{
    public class SiteViewModel
    {
        public ICollection<GymMemberShipViewModel> GymMemberShips { get; set; }
        public IEnumerable<TrainerProgramViewModel> TrainerPrograms { get; set; }
        public IEnumerable<BestGymViewModel> BestGyms { get; set; }
        public IEnumerable<BestTrainerViewModel> BestTrainer { get; set; }
        public ICollection<LatestGymViewModel> LatestGyms { get; set; }
        public ICollection<LatestTrainerViewModel> LatestTrainers { get; set; }
    }

    public class GymMemberShipViewModel
    {
        public Guid SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public Guid SubscriptionOwnerId { get; set; }
        public string AboutYourSelf { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public decimal SubscriptionMonths { get; set; }
        public bool IsFullText { get; set; }
    }

    public class TrainerProgramViewModel
    {
        public Guid SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public Guid SubscriptionOwnerId { get; set; }
        public string TrainingDays { get; set; }
        public string TrainingTiming { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public decimal SubscriptionMonths { get; set; }
    }

    public class BestGymViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Picture { get; set; }
    }

    public class BestTrainerViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string Picture { get; set; }
    }

    public class LatestGymViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GymName { get; set; }
        public string Designation { get; set; }
        public string AboutYourSelf { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool IsFullText { get; set; }
    }

    public class LatestTrainerViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string AboutYourSelf { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool IsFullText { get; set; }
    }
}
