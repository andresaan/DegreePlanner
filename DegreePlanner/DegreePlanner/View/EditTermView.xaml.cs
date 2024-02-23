using DegreePlanner.ViewModel;

namespace DegreePlanner.View;

public partial class EditTermView : ContentPage
{
    private EditTermViewModel _viewModel;

	public EditTermView(EditTermViewModel viewModel)
	{
        _viewModel = viewModel;

		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.LoadCourses();
    }
}