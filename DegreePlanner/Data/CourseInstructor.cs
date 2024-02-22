using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Table("Instructor")]
    public class CourseInstructor : BaseItem
    {
        [PrimaryKey, AutoIncrement]
        [Column("instructor_id")]
        public int InstructorId { get => ObjectId; set => ObjectId = value; }

        [Column("name")]
        public string InstructorName { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}
