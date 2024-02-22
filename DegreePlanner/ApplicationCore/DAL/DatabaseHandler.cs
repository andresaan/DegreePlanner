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
                //connection.CreateTable<Term>();
                connection.CreateTable<Course>();
                //connection.CreateTable<CourseInstructor>();
                //connection.CreateTable<Assessment>();

                //foreach (Term term in _testData.TestTerms)
                //{
                //    AddItem(term);
                //}

                //foreach (Course course in _testData.TestCourses)
                //{
                //    AddItem(course);
                //}

                //foreach (CourseInstructor instructor in _testData.TestInstructors)
                //{
                //    AddItem(instructor);
                //}

                //foreach (Assessment assessment in _testData.TestAssessments)
                //{
                //    AddItem(assessment);
                //}
            }
        }

        public void AddItem<T>(T item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Insert(item);
            }
        }

        public CourseInstructor FindInstructor(CourseInstructor instructor)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Table<CourseInstructor>().FirstOrDefault(i => i.InstructorName == instructor.InstructorName && i.Phone == instructor.Phone && i.Email == instructor.Email);
            }
        }

        public Course FindCourse(Course course)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Table<Course>().FirstOrDefault(i => i.Name == course.Name);
            }
        }

        public List<CourseInstructor> GetAllCourseInstructors()
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
    }
}
