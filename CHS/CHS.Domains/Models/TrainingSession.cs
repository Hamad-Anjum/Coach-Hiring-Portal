using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class TrainingSession : Common
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TotalClasses { get; set; }
        [Column(nameof(Member))]
        public Guid TrainerId { get; set; }
        public Member Member { get; set; }
    }
}
