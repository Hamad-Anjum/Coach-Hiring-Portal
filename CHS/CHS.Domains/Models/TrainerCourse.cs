using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    /// <summary>
    /// Trainer Enrollment
    /// </summary>
    public class TrainerCourse : Common
    {
        public Guid CourseId { get; set; }
        public TrainingSession Course { get; set; }
        [Column(nameof(Member))]
        public Guid TrainerId { get; set; }
        public Member Member { get; set; }
    }
}
