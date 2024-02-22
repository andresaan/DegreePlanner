using CommunityToolkit.Maui.Views;
using DegreePlanner.ViewModel;

namespace DegreePlanner.View;

public partial class AddAssessmentPopup : Popup
{
	public AddAssessmentPopup(AddEditCourseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        StartDatePicker.MinimumDate = DateTime.Now;
        StartDatePicker.MaximumDate = DateTime.MaxValue;

        EndDatePicker.MinimumDate = DateTime.Now;
        EndDatePicker.MaximumDate = DateTime.MaxValue;
    }
}