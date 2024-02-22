using CommunityToolkit.Maui.Views;
using DegreePlanner.ViewModel;

namespace DegreePlanner.View;

public partial class DegreePlanView : ContentPage
{
	private DegreePlanViewModel _viewModel;
	public DegreePlanView(DegreePlanViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
        _viewModel = viewModel;

    }

    private void ShowAddTermPopUp(object sender, EventArgs e)
    {
		this.ShowPopup(new AddTermPopup(_viewModel));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.LoadTerms();
    }
}