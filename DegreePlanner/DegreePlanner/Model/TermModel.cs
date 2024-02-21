using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.Model
{
    public class TermModel
    {
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TotalCus { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TotalCus { get; set; }
        public string Status { get; set; }
        public CourseStatus CourseStatus { get; set; } = CourseStatus.NotStarted;
        public Dictionary<Enum, string> CourseStatusLabel = new Dictionary<Enum, string>()
        {
            {CourseStatus.NotStarted, "Not Started" },
            {CourseStatus.Started, "Started" },
            {CourseStatus.Completed, "Completed" },
        };
    }

    public enum CourseStatus
    {
        Completed,
        Started,
        NotStarted,
    }
}