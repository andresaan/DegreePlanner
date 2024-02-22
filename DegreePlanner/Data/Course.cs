using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Table("Course")]
    public class Course : BaseItem
    {
        [PrimaryKey, AutoIncrement]
        [Column("course_id")]
        public int CourseId { get => ObjectId; set => ObjectId = value; }

        [NotNull]
        [Column("term_id")]
        public int TermId { get; set; }

        [NotNull]
        [Column("instructor_id")]
        public int InstructorId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("start")]
        public DateTime Start { get; set; }

        [Column("end")]
        public DateTime End { get; set; }

        [Column("total_cus")]
        public int TotalCus { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Ignore]
        public CourseInstructor CourseInstructor { get; set; }

        [Ignore]
         public List<Assessment> Assessments { get; set; }
    }
}
