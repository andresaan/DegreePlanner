using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Table("Course")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("term_id")]
        public int TermId { get; set; }

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
    }
}
