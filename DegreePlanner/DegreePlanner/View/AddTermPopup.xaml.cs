using CommunityToolkit.Maui.Views;
using DegreePlanner.ViewModel;

namespace DegreePlanner.View;

public partial class AddTermPopup : Popup
{
	public AddTermPopup(DegreePlanViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		StartDatePicker.MinimumDate = DateTime.Now;
		StartDatePicker.MaximumDate = DateTime.MaxValue;

        EndDatePicker.MinimumDate = DateTime.Now;
        EndDatePicker.MaximumDate = DateTime.MaxValue;
    }
}