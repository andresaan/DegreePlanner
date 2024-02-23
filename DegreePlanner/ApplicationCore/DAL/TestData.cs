using Data;

namespace ApplicationCore.DAL
{
    public class TestData

    {
        public List<Term> TestTerms = new List<Term>()
        {
            new Term()
            {
                Name = "Spring Term",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(2),
                TotalCus = 10
            }
        };

        public List<Course> TestCourses = new List<Course>()
        {
            new Course()
            {
                TermId = 1,
                InstructorId = 1,
                Name = "First Course",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(2),
                TotalCus = 8,
                Status = "Not Started"
            }
        };

        public List<Assessment> TestAssessments = new List<Assessment>()
        {
            new Assessment()
            {
                CourseId = 1,
                Name = "Mobile App",
                Type = "PA",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(2)
            },
            new Assessment()
            {
                CourseId = 1,
                Name = "Second Assessment",
                Type = "OA",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(2)
            }
        };

        public List<CourseInstructor> TestInstructors = new List<CourseInstructor>()
        {
            new CourseInstructor()
            {
                InstructorName = "Anika Patel",
                Phone = "555-123-4567",
                Email = "anika.patel@strimeuniversity.edu"
            }
        };
    }
}
