using SQLite;
using Data;

namespace Data
{
    [Table("Term")]
    public class Term : BaseItem
    {
        [PrimaryKey, AutoIncrement]
        [Column("term_id")]
        public int TermId { get => ObjectId; set => ObjectId = value; }

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
