using Microsoft.Maui.Controls;
using openstreetmapAPI;
using static openstreetmapAPI.DataBaseConnection;

namespace WRWpr;

public partial class Wydarzenia : ContentPage
{
    DataBaseConnection connection = new DataBaseConnection();
    List<listaMarkerow> lista;
    //listaMarkerow lista = new listaMarkerow();
    class debug
    {
        public string nazwa { get; set; }
        public listaMarkerow dane { get; set; }

        public debug() { }

        public debug(string nazwa, listaMarkerow dane)
        {
            this.nazwa = nazwa;
            this.dane = dane;
        }


    }
    List<debug> lol  = new List<debug>();
    public Wydarzenia()
	{
		InitializeComponent();

        List<string> list = new List<string>();
        lista = connection.Markers();
        List<string> mar = new List<string>();
        foreach (listaMarkerow p in lista)
        {
            mar = connection.Adres(p.lat, p.lon);
            string text = string.Format("{0} {1} {2}", p.cat_id, p.name, mar[2]);
            list.Add(text);
            lol.Add(new debug(text, p));
        }
        eventsCollectionView.ItemsSource = list;
        eventsCollectionView.SelectionChanged += OnEventSelected;
	}

	private void OnEventSelected(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is string selectedEventName)
		{
            for(int i = 0; i < lol.Count; i++)
            {
                if(lol[i].nazwa == selectedEventName)
                {
                    Navigation.PushAsync(new DetailPage(lol[i].dane.id));
                }
            }
        }   
    }
}