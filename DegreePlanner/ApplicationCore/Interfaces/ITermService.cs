using Data;

namespace ApplicationCore.Interfaces
{
    public interface ITermService
    {
        public void CascadeDeleteCourse(int courseId);
        public void CascadeDeleteTerm(int termId);
        public void InitializeDb();
        public void AddItem<T>(T item);
        public void RemoveById<T>(int id);
        public List<Term> GetAllTerms();
        public List<CourseInstructor> GetAllInstructors();
        public List<Course> GetCoursesByTermId(int termId);
        public string SaveCourseInformation(CourseInstructor instructor, Course course, List<Assessment> assessments);

        public void UpdateItem(object item);
    }
}
