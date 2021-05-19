using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHS.Domains.Models
{
    public class TrainerAttendance : Common
    {
        /// <summary>
        /// Trainer Id
        /// </summary>
        [Column(nameof(Member))]
        public Guid TrainerId { get; set; }
        public Member Member { get; set; }
        [Column(nameof(TrainingSession))]
        public Guid TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; }
        public int Taken { get; set; }
        public int Left { get; set; }
        public int Total { get; set; }
    }
}
