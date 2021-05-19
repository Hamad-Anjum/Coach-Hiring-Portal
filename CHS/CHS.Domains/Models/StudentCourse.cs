using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    /// <summary>
    /// Student Enrollment
    /// </summary>
    public class StudentCourse : Common
    {
        public Guid TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; }

        [Column(nameof(Member))]
        public Guid StudentId { get; set; }
        public Member Member { get; set; }
    }
}
