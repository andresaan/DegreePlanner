using Data;

namespace ApplicationCore.Interfaces
{
    public interface ITermService
    {
        public void InitializeDb();
        public void AddItem<T>(T item);
        public List<Term> GetAllTerms();
        public List<CourseInstructor> GetAllInstructors();
        public void SaveCourseInformation(CourseInstructor instructor, Course course, List<Assessment> assessments);
    }
}
