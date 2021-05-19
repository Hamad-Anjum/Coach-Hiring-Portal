using System;

namespace CHS.Domains.Models
{
    /// <summary>
    /// Optional Class.
    /// </summary>
    public class StudentAttendance : Common
    {
        /// <summary>
        /// Student Id
        /// </summary>
        public Guid StudentCourseId { get; set; }
        public StudentCourse StudentCourse { get; set; }
        public int Taken { get; set; }
        public int Left { get; set; }
        public int Total { get; set; }
    }
}
