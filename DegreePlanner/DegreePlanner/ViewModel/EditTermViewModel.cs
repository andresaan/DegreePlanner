using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Data;
using CommunityToolkit.Mvvm.Input;
using DegreePlanner.View;
using ApplicationCore.Interfaces;

namespace DegreePlanner.ViewModel
{
    [QueryProperty(nameof(Term), "Term")]
    public partial class EditTermViewModel : ObservableObject
    {
        private ITermService _termService;

        [ObservableProperty]
        public Term term;

        [ObservableProperty]
        public ObservableCollection<Course> courses;

        public EditTermViewModel(ITermService termService)
        {
            _termService = termService;
        }

        [RelayCommand]
        public void EditTerm(Term term)
        {
            _termService.UpdateItem(term);
        }

        [RelayCommand]
        public async Task DeleteTerm(Term term)
        {
            _termService.CascadeDeleteTerm(term.TermId);

            await GoBack();
        }

        [RelayCommand]
        async Task AddCourse(int termId)
        {
            await Shell.Current.GoToAsync($"{nameof(AddEditCourseView)}?TermId={termId}");
        }

        [RelayCommand]
        async Task EditCourse(Course course)
        {
            var parameters = new Dictionary<string, object>
            {
                {"Course", course }
            };

            await Shell.Current.GoToAsync(nameof(AddEditCourseView), parameters);
        }

        public void LoadCourses()
        {
            Courses = new ObservableCollection<Course>(_termService.GetCoursesByTermId(Term.TermId));
        }

        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
