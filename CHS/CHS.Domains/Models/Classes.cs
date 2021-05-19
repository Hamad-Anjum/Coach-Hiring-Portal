using System;

namespace CHS.Domains.Models
{
    public class Classes : Common
    {
        public int Taken { get; set; }
        public int Left { get; set; }
        public int Total { get; set; }
        public Guid TrainingSessionId { get; set; }
        public TrainingSession TrainingSession { get; set; }
    }
}
