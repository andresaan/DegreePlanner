using SQLite;
using Data;

namespace Data
{
    [Table("Term")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
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

        [Ignore]
        public List<Course> Courses { get; set; }
    }
}
