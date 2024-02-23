using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data;
using System.Collections.ObjectModel;
using ApplicationCore.Interfaces;

namespace DegreePlanner.ViewModel
{
    public partial class AddEditCourseViewModel : ObservableObject
    {
        private ITermService _termService;

        public int TermId { get; set; }

        [ObservableProperty]
        public Course course = new Course();

        [ObservableProperty]
        public CourseInstructor courseInstructor = new CourseInstructor();

        [ObservableProperty]
        public Assessment newAssessment = new Assessment();

        [ObservableProperty]
        public ObservableCollection<Assessment> assessments = new ObservableCollection<Assessment>();


        public AddEditCourseViewModel(ITermService termService)
        {
            _termService = termService;

            SaveCourseInformationCommand = new AsyncRelayCommand(SaveCourseInformation);
        }

        public IAsyncRelayCommand SaveCourseInformationCommand { get; }

        async Task SaveCourseInformation()
        {
            Course.TermId = TermId != 0 ? TermId : Course.TermId;

            var errorMessage = _termService.SaveCourseInformation(CourseInstructor, Course, Assessments.ToList());

            if (errorMessage == null)
            {
                await GoBack();
            }

            await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "Ok");
        }

        [RelayCommand]
        public void AddAssessment()
        {
            Assessments.Add(new Assessment()
            {
                Name = NewAssessment.Name,
                Type = NewAssessment.Type,
                Start = NewAssessment.Start,
                End = NewAssessment.End
            });
        }

        [RelayCommand]
        public void DeleteAssessment(Assessment assessment)
        {
            if (assessment.AssessmentId > 0)
            {
                _termService.RemoveById<Assessment>(assessment.AssessmentId);
            }

            Assessments.Remove(assessment);
        }

        [RelayCommand]
        public async Task DeleteCourse(int courseId)
        {
            var c = courseId;
            _termService.CascadeDeleteCourse(courseId);

            await GoBack();
        }

        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        public void SetCourseToEdit()
        {
            CourseInstructor = Course.CourseInstructor;
            Assessments = new ObservableCollection<Assessment>(Course.Assessments);
        }
    }
}
