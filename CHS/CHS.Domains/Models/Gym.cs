using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class Gym : Common
    {
        public string Name { get; set; }

        /// <summary>
        /// Available space in gym
        /// </summary>
        public bool AvaiableSpace { get; set; } = true;

        /// <summary>
        /// Total capacity of members in Gym
        /// </summary>
        public int TotalCapacity { get; set; }
        public bool IsHotelGym { get; set; }
        public Address Address { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<GymMember> GymMembers { get; set; }
        public ICollection<GymSubscription> GymSubscriptions { get; set; }
    }
}
