using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class District : Common
    {
        public string Name { get; set; }
        [ForeignKey(nameof(City))]
        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
