using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CHS.Infrastructure.ViewModels
{
    public class CityViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public Guid StateId { get; set; }
        public string StateName { get; set; }

        [Required]
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }

        public string Status { get; set; }
    }

    public class StateViewModel
    {
        public Guid StateId { get; set; }
        [Required]
        public string StateName { get; set; }
        public string Status { get; set; }
        [Required]
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class CountryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class CountryStateCityDistrict
    {
        public ICollection<DistrictViewModel> Districts { get; set; }
        public ICollection<CityViewModel> Cities { get; set; }
        public ICollection<StateViewModel> States { get; set; }
        public ICollection<CountryViewModel> Countries { get; set; }
        public int StatesCount { get; set; }
        public int CitiesCount { get; set; }
    }

    public class CountryStateCity
    {
        public ICollection<CityViewModel> Cities { get; set; }
        public ICollection<StateViewModel> States { get; set; }
        public ICollection<CountryViewModel> Countries { get; set; }
        public int StatesCount { get; set; }
    }

    public class CountryAndState
    {
        public ICollection<CountryViewModel> Countries { get; set; }
        public int CountriesCount { get; set; }
        public ICollection<StateViewModel> States { get; set; }
    }
}
