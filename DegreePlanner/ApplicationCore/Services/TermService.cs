using Data;
using ApplicationCore.DAL;
using ApplicationCore.Interfaces;
using Plugin.LocalNotification;
using System.Text;

namespace ApplicationCore.Services
{
    public class TermService : ITermService
    {
        private DatabaseHandler _db = new DatabaseHandler();

        public void InitializeDb()
        {
            _db.InitializeDatabase();
        }

        public void AddItem<T>(T item)
        {
            _db.AddItem(item);
        }

        public void RemoveById<T>(int id)
        {
            _db.DeleteItemById<T>(id);
        }

        public List<Term> GetAllTerms()
        {
            return _db.GetAllTerms();
        }

        public List<CourseInstructor> GetAllInstructors()
        {
            return _db.GetAllInstructors();
        }

        public List<Course> GetCoursesByTermId(int termId)
        {
            return _db.GetCoursesByTermId(termId);
        }

        public string SaveCourseInformation(CourseInstructor instructor, Course course, List<Assessment> assessments)
        {
            if (string.IsNullOrEmpty(instructor.InstructorName) || string.IsNullOrEmpty(instructor.Phone) || string.IsNullOrEmpty(instructor.Email))
            {
                return "Instructor information cannot be null";
            }

            var existingInstructor = _db.FindInstructor(instructor);

            if (existingInstructor == null)
            {
                instructor.InstructorId = 0;
                _db.AddItem<CourseInstructor>(instructor);
            }

            instructor = existingInstructor != null ? existingInstructor : _db.FindInstructor(instructor);

            course.InstructorId = instructor.InstructorId;

            if (course.CourseId <= 0)
            {
                _db.AddItem<Course>(course);
            }
            else
            {
                _db.UpdateItem(course);
            }

            course = _db.FindCourse(course);

            foreach (Assessment assessment in assessments)
            {
                assessment.CourseId = course.CourseId;
                _db.AddItem(assessment);
            }

            return null;
        }

        public void CascadeDeleteCourse(int courseId)
        {
            _db.CascadeDeleteCourse(courseId);
        }

        public void CascadeDeleteTerm(int termId)
        {
            _db.CascadeDeleteTerm(termId);
        }

        public void UpdateItem(object item)
        {
            _db.UpdateItem(item);
        }

        
    }
}
