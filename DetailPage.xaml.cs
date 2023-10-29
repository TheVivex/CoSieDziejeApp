using openstreetmapAPI;
using static openstreetmapAPI.DataBaseConnection;

namespace WRWpr;

public partial class DetailPage : ContentPage
{
    public string selectedEventName;

    DataBaseConnection connection3 = new DataBaseConnection();

    public DetailPage(string eventEventName)
	{
		InitializeComponent();
		selectedEventName = eventEventName;
	}
}