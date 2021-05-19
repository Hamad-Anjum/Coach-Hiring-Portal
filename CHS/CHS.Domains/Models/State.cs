using System;
using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class State : Common
    {
        public string StateName { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
