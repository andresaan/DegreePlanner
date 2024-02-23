using Data;
using SQLite;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace ApplicationCore.DAL
{
    public class DatabaseHandler
    {
        private string _connectionString;

        private TestData _testData = new TestData();

        public DatabaseHandler()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = @"..\\..\\..\\..\\..\\..\\ApplicationCore\\DAL\\DegreePlannerDatabase.db";
            _connectionString = Path.GetFullPath(Path.Combine(basePath, relativePath));
        }
        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.CreateTable<Term>();
                connection.CreateTable<Course>();
                connection.CreateTable<CourseInstructor>();
                connection.CreateTable<Assessment>();

                foreach (Term term in _testData.TestTerms)
                {
                    AddItem(term);
                }

                foreach (Course course in _testData.TestCourses)
                {
                    AddItem(course);
                }

                foreach (CourseInstructor instructor in _testData.TestInstructors)
                {
                    AddItem(instructor);
                }

                foreach (Assessment assessment in _testData.TestAssessments)
                {
                    AddItem(assessment);
                }
            }
        }

        public void AddItem<T>(T item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Insert(item);
            }
        }

        public void DeleteItemById<T>(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Delete<T>(id);
            }
        }

        public void UpdateItem(object item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Update(item);
            }
        }


        public Course FindCourse(Course course)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Table<Course>().FirstOrDefault(i => i.Name == course.Name);
            }
        }

        public List<Course> GetCoursesByTermId(int termId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var courses = connection.Table<Course>().ToList();
                var assessments = connection.Table<Assessment>().ToList();
                var instructors = connection.Table<CourseInstructor>().ToList();

                courses = courses.Where(c => c.TermId == termId).ToList();

                foreach (Course course in courses)
                {
                    course.Assessments = assessments.Where(a => a.CourseId == course.CourseId).ToList();
                    course.CourseInstructor = instructors.FirstOrDefault(i => i.InstructorId == course.InstructorId);
                }

                return courses;
            }
        }

        public void CascadeDeleteCourse(int courseId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                List<Assessment> assessments = connection.Table<Assessment>().ToList();
                List<CourseInstructor> instructors = connection.Table<CourseInstructor>().ToList();

                var course = connection.Find<Course>(courseId);

                if (course != null)
                {
                    var deleteInstructor = instructors.FirstOrDefault(i => i.InstructorId == course.InstructorId);

                    if (deleteInstructor != null)
                    {
                        connection.Delete(deleteInstructor);
                    }

                    var deleteAssessments = assessments.Where(a => a.CourseId == course.CourseId).ToList();

                    foreach (Assessment assessment in deleteAssessments)
                    {
                        connection.Delete(assessment);
                    }

                    connection.Delete(course);
                }
            }
        }


        public CourseInstructor FindInstructor(CourseInstructor instructor)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Table<CourseInstructor>().FirstOrDefault(i => i.InstructorName == instructor.InstructorName && i.Phone == instructor.Phone && i.Email == instructor.Email);
            }
        }

        public List<CourseInstructor> GetAllInstructors()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Table<CourseInstructor>().ToList();
            }
        }


        public List<Term> GetAllTerms()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                List<Term> terms = connection.Table<Term>().ToList();
                List<Course> courses = connection.Table<Course>().ToList();
                List<Assessment> assessments = connection.Table<Assessment>().ToList();
                List<CourseInstructor> instructors = connection.Table<CourseInstructor>().ToList();

                foreach (Term term in terms)
                {
                    term.Courses = courses.Where(c => c.TermId == term.TermId).ToList();

                    if (term.Courses != null)
                    {
                        foreach (Course course in term.Courses)
                        {
                            course.CourseInstructor = instructors.FirstOrDefault(i => i.InstructorId == course.InstructorId);

                            course.Assessments = assessments.Where(a => a.CourseId == course.CourseId).ToList();
                        }
                    }
                }

                return terms;
            }
        }

        public void CascadeDeleteTerm(int termId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                List<Course> courses = connection.Table<Course>().ToList();
                List<Assessment> assessments = connection.Table<Assessment>().ToList();
                List<CourseInstructor> instructors = connection.Table<CourseInstructor>().ToList();

                var deleteCourses = courses.Where(c => c.TermId == termId).ToList();

                foreach (Course course in deleteCourses)
                {
                    CascadeDeleteCourse(course.CourseId);
                }

                DeleteItemById<Term>(termId);
            }
        }
    }
}
