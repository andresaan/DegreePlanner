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
        public int CourseId { get; set; }

        [ObservableProperty]
        public string courseName;

        [ObservableProperty]
        public DateTime courseStart;

        [ObservableProperty]
        public DateTime courseEnd;

        [ObservableProperty]
        public string status;

        [ObservableProperty]
        public string totalCus;

        [ObservableProperty]
        public string courseNotes = "";

        [ObservableProperty]
        public string instructorName;

        [ObservableProperty]
        public string instructorPhone;

        [ObservableProperty]
        public string instructorEmail;

        [ObservableProperty]
        public string assessmentName;

        [ObservableProperty]
        public string assessmentType;

        [ObservableProperty]
        public DateTime assessmentStart;

        [ObservableProperty]
        public DateTime assessmentEnd;

        [ObservableProperty]
        public ObservableCollection<Assessment> assessments = new ObservableCollection<Assessment>();

        [ObservableProperty]
        public ObservableCollection<CourseInstructor> instructors;


        public AddEditCourseViewModel(ITermService termService)
        {
            _termService = termService;

            SaveCourseInformationCommand = new AsyncRelayCommand(SaveCourseInformation);

            LoadInstructors();
        }

        public IAsyncRelayCommand SaveCourseInformationCommand { get; }

        async Task SaveCourseInformation()
        {
            // add new course flow: Validate - Instructor - Course - Assessment

            // Course object - TermId, InstructorId, Name, Start, End, CUs, Status
            // TermId - DegreePlan query param
            // InsId - Quick duplicate check, add new, grab id

            // Instructor obj - Name, Phone, Email
            // Only add new if InsId is needed

            // Assessment obj - CourseId, Type, Start, End
            // CourseId - get after adding course object

            var instructor = new CourseInstructor()
            {
                InstructorName = InstructorName,
                Phone = InstructorPhone,
                Email = InstructorEmail
            };

            var course = new Course()
            {
                TermId = TermId,
                InstructorId = instructor.InstructorId,
                Name = CourseName,
                Start = CourseStart,
                End = CourseEnd,
                TotalCus = Convert.ToInt32(TotalCus),
                Status = Status,
                Notes = CourseNotes
            };

            _termService.SaveCourseInformation(instructor, course, Assessments.ToList());

            await GoBack();
        }

        [RelayCommand]
        public void AddAssessment()
        {
            Assessments.Add(new Assessment()
            {
                Name = AssessmentName,
                Type = AssessmentType,
                Start = AssessmentStart,
                End = AssessmentEnd
            });
        }
        
        [RelayCommand]
        public void DeleteAssessment(Assessment assessment)
        {
            Assessments.Remove(assessment);
        }

        private void LoadInstructors()
        {
            Instructors = new ObservableCollection<CourseInstructor>(_termService.GetAllInstructors());
        }

        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
