using DegreePlanner.View;

namespace DegreePlanner
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddEditCourseView), typeof(AddEditCourseView));
        }
    }
}