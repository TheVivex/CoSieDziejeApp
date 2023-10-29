using openstreetmapAPI;
using Syncfusion.Maui.Maps;
using System.Drawing;
using System.Security.AccessControl;
using static openstreetmapAPI.DataBaseConnection;



namespace WRWpr
{
    public partial class MainPage : ContentPage
    {
        DataBaseConnection connection2 = new DataBaseConnection();

        public MainPage()
        {
            InitializeComponent();

            List<string> list2 = new List<string>();
            List<listaMarkerow> lista2 = connection2.Markers();
            List<string> mar2 = new List<string>();
            foreach (listaMarkerow p2 in lista2)
            {
                mar2 = connection2.Adres(p2.lat, p2.lon);
                
                MapMarker s = new MapMarker();
                s.Latitude = double.Parse(p2.lat, System.Globalization.CultureInfo.InvariantCulture);
                s.Longitude = double.Parse(p2.lon, System.Globalization.CultureInfo.InvariantCulture);
                s.IconFill = Brush.Red;                
                s.IconType = MapIconType.Diamond;
                s.IconWidth = 20;
                s.IconHeight = 20;
                ls2.Add(s);
            }
        }
        private void Centrowanie(object sender, EventArgs e)
        {
            mapa.Center = new MapLatLng(51.1103, 17.0589);
        }

        private void Nowy_marker(int id, string lon,string lat)
        {
                       
        }

        
    }
}