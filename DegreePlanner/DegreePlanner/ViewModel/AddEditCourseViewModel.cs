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
        private ISetNotificationService _notificationService;

        public int TermId { get; set; }

        [ObservableProperty]
        public Course course = new Course();

        [ObservableProperty]
        public CourseInstructor courseInstructor = new CourseInstructor();

        [ObservableProperty]
        public Assessment newAssessment = new Assessment();

        [ObservableProperty]
        public string notificationTitle;

        [ObservableProperty]
        public string notificationDescription;

        [ObservableProperty]
        public DateTime notificationDate;

        [ObservableProperty]
        public ObservableCollection<Assessment> assessments = new ObservableCollection<Assessment>();


        public AddEditCourseViewModel(ITermService termService, ISetNotificationService notificationService)
        {
            _termService = termService;

            SaveCourseInformationCommand = new AsyncRelayCommand(SaveCourseInformation);
            _notificationService = notificationService;
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

        [RelayCommand]
        public async Task SetReminder()
        {
            await _notificationService.SetNotification(NotificationTitle, NotificationDescription, NotificationDate);

            NotificationTitle = "";
            NotificationDescription = "";
            NotificationDate = DateTime.Now;

            await Application.Current.MainPage.DisplayAlert("Reminder", "Reminder successfully set to device", "Ok");
        }

        [RelayCommand]
        public async Task ShareNotes()
        {
            if (!string.IsNullOrEmpty(Course.Notes))
            {
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Text = Course.Notes,
                    Title = "Share Text"
                });
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Share Request", "Notes cannot be empty", "Ok");
            }
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
