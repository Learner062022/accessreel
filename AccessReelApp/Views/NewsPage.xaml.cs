using AccessReelApp.ViewModels;

namespace AccessReelApp.Views;

public partial class NewsPage : ContentPage
{
	public NewsPage(NewsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}