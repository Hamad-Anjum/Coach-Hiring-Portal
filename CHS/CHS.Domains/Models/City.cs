using System;
using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class City : Common
    {
        public string CityName { get; set; }
        public Guid StateId { get; set; }
        public State State { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
