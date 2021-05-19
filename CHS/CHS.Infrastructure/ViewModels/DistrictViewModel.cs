using System;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class DistrictViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        [Required]
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        [Required]
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
    }
}
