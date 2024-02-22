using SQLite;

namespace Data
{
    [Table("Assessment")]
    public class Assessment : BaseItem
    {
        [PrimaryKey, AutoIncrement]
        [Column("assessment_id")]
        public int AssessmentId { get => ObjectId; set => ObjectId = value; }

        [NotNull]
        [Column("course_id")]
        public int CourseId { get; set; }

        [NotNull]
        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("start")]
        public DateTime Start { get; set; }

        [Column("end")]
        public DateTime End { get; set; }
    }
}
