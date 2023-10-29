using Microsoft.Maui.Controls;
using openstreetmapAPI;
using static openstreetmapAPI.DataBaseConnection;

namespace WRWpr;

public partial class Wydarzenia : ContentPage
{
    DataBaseConnection connection = new DataBaseConnection();
    //listaMarkerow lista = new listaMarkerow();

    public Wydarzenia()
	{
		InitializeComponent();

        List<string> list = new List<string>();
        connection.test(1, "s");
        List<listaMarkerow> lista = connection.Markers();
        List<string> mar = new List<string>();
        foreach (listaMarkerow p in lista)
        {
            mar = connection.Adres(p.lat, p.lon);
            list.Add(string.Format("{0} {1} {2}", p.cat_id, p.name, mar[2]));
        }
        eventsCollectionView.ItemsSource = list;
        eventsCollectionView.SelectionChanged += OnEventSelected;
	}

	private void OnEventSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is string selectedEventName)
		{
            Navigation.PushAsync(new DetailPage(selectedEventName));
        }   
    }
}