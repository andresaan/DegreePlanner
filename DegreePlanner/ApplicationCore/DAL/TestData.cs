using Data;

namespace ApplicationCore.DAL
{
    public class TestData

    {
        public List<Term> TestTerms = new List<Term>()
        {
            new Term()
            {
                Name = "test",
                Start = DateTime.Now,
                End = DateTime.Now,
                TotalCus = 10
            }
        };

        public List<Course> TestCourses = new List<Course>()
        {
            new Course()
            {
                TermId = 1,
                InstructorId = 1,
                Name = "test",
                Start = DateTime.Now,
                End = DateTime.Now,
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
                Type = "Objective Assessment",
                Start = DateTime.Now,
                End = DateTime.Now
            }
        };

        public List<CourseInstructor> TestInstructors = new List<CourseInstructor>()
        {
            new CourseInstructor()
            {
                InstructorName = "Fake Name",
                Phone = "555-555-5555",
                Email = "test@test.com"
            }
        };
    }
}
