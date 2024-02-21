using SQLite;
using Data;

namespace DAL
{
    public class DatabaseHandler
    {
        private string _connectionString;

        private List<Term> _testTerms = new List<Term>()
        {
            new Term()
            {
                Name = "test",
                Start = DateTime.Now,
                End = DateTime.Now,
                TotalCus = 10
            }
        };

        private List<Course> _testCourses = new List<Course>()
        {
            new Course()
            {
                Name = "test",
                Start = DateTime.Now,
                End = DateTime.Now,
                TotalCus = 8,
                TermId = 1
            }
        };

        public DatabaseHandler()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = @"..\\..\\..\\..\\..\\..\\DAL\\DegreePlannerDatabase.db";
            _connectionString = Path.GetFullPath(Path.Combine(basePath, relativePath));
        }

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.CreateTable<Term>();
                connection.CreateTable<Course>();

                foreach (Term term in _testTerms)
                {
                    AddItem<Term>(term);
                }

                foreach (Course course in _testCourses)
                {
                    AddItem<Course>(course);
                }
            }
        }

        public void AddItem<T>(T term)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Insert(term);
            }
        }

        public List<Term> GetAllTerms()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                List<Term> terms = connection.Table<Term>().ToList();
                List<Course> courses = connection.Table<Course>().ToList();

                foreach (Term term in terms)
                {
                    term.Courses = courses.Where(c => c.TermId == term.TermId).ToList();
                }

                return terms;
            }
        }
    }
}
