namespace WRWpr;

public partial class DetailPage : ContentPage
{
    private string selectedEventName;

    public DetailPage(string eventEventName)
	{
		InitializeComponent();
		selectedEventName = eventEventName;
	}
}