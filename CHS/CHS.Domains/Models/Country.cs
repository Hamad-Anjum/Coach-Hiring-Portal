using System.Collections.Generic;

namespace CHS.Domains.Models
{
    public class Country : Common
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public ICollection<State> States { get; set; }
    }
}
