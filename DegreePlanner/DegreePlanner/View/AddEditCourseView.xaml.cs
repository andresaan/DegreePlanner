using CommunityToolkit.Maui.Views;
using DegreePlanner.ViewModel;
using Data;

namespace DegreePlanner.View;

[QueryProperty(nameof(Course), nameof(Course))]
[QueryProperty(nameof(TermId), nameof(TermId))]
public partial class AddEditCourseView : ContentPage
{
    private AddEditCourseViewModel _viewModel;

    public string TermId { get; set; }
    public Course Course { get; set; } 

    public AddEditCourseView(AddEditCourseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

        StartDatePicker.MinimumDate = DateTime.Now;
        StartDatePicker.MaximumDate = DateTime.MaxValue;

        EndDatePicker.MinimumDate = DateTime.Now;
        EndDatePicker.MaximumDate = DateTime.MaxValue;

        ReminderDatePicker.MinimumDate = DateTime.Now;
        ReminderDatePicker.MaximumDate = DateTime.MaxValue;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var canParse = int.TryParse(TermId, out var parsedTermId);

        if (canParse)
        {
            _viewModel.TermId = parsedTermId;
        }

        if (Course != null)
        {
            _viewModel.Course = Course;

            PageTitle.Text = Course != null ? "Edit Course" : "Add Course";

            _viewModel.SetCourseToEdit();
        }
    }

    private void ShowAddAssessmentPopup(object sender, EventArgs e)
    {
        this.ShowPopup(new AddAssessmentPopup(_viewModel));
    }
}