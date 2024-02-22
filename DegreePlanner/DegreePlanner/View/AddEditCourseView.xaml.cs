using CommunityToolkit.Maui.Views;
using DegreePlanner.ViewModel;

namespace DegreePlanner.View;

[QueryProperty(nameof(CourseId), nameof(CourseId))]
[QueryProperty(nameof(TermId), nameof(TermId))]
public partial class AddEditCourseView : ContentPage
{
    private AddEditCourseViewModel _viewModel;

    public string TermId { get; set; }
    public string CourseId { get; set; } = "-1";

    public AddEditCourseView(AddEditCourseViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

        StartDatePicker.MinimumDate = DateTime.Now;
        StartDatePicker.MaximumDate = DateTime.MaxValue;

        EndDatePicker.MinimumDate = DateTime.Now;
        EndDatePicker.MaximumDate = DateTime.MaxValue;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var canParse = int.TryParse(TermId, out var parsedTermId);

        if (canParse)
        {
            _viewModel.TermId = parsedTermId;
        }

        canParse = int.TryParse(CourseId, out var parsedCourseId);

        if (canParse)
        {
            _viewModel.CourseId = parsedCourseId;

            PageTitle.Text = parsedCourseId > 0 ? "Edit Course" : "Add Course";
        }
    }

    private void ShowAddAssessmentPopup(object sender, EventArgs e)
    {
        this.ShowPopup(new AddAssessmentPopup(_viewModel));
    }
}