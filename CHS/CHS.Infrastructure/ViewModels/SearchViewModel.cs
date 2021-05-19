using System;

namespace CHS.Infrastructure.ViewModels
{
    public class SearchViewModel
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Picture { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string AboutYourSelf { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool GroupTrainer { get; set; }
        public bool PrivateTrainer { get; set; }
        public bool AvaiableToHireOrSpaceAvailable { get; set; }
    }
}
