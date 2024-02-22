using Data;
using ApplicationCore.DAL;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class TermService : ITermService
    {
        private DatabaseHandler _db = new DatabaseHandler();

        public void AddItem<T>(T item)
        {
            _db.AddItem(item);
        }

        public void InitializeDb()
        {
            _db.InitializeDatabase();
        }

        public List<Term> GetAllTerms()
        {
            return _db.GetAllTerms();
        }

        public List<CourseInstructor> GetAllInstructors()
        {
            return _db.GetAllCourseInstructors();
        }

        public void SaveCourseInformation(CourseInstructor instructor, Course course, List<Assessment> assessments)
        {
            var existingInstructor = _db.FindInstructor(instructor);

            if (existingInstructor == null)
            {
                _db.AddItem<CourseInstructor>(instructor);
            }

            instructor = existingInstructor != null ? existingInstructor : _db.FindInstructor(instructor);

            course.InstructorId = instructor.InstructorId;

            _db.AddItem<Course>(course);
            course = _db.FindCourse(course);

            foreach (Assessment assessment in assessments)
            {
                assessment.CourseId = course.CourseId;
                _db.AddItem(assessment);
            }
        }
    }
}
