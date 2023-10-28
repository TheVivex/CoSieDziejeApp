using Syncfusion.Maui.Maps;

namespace WRWpr
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private void Centrowanie(object sender, EventArgs e)
        {
            mapa.Center = new MapLatLng(51.1103, 17.0589);
        }

        private void Profil(object sender, EventArgs e)
        {

        }

        private void Dodawanie(object sender, EventArgs e)
        {

        }

        private void Lista(object sender, EventArgs e)
        {

        }

        private void marker_PropertyChanged(object sender, EventArgs e)
        {
            if (variables.Default.g_first < 2)
            {
                variables.Default.g_first++;
            }
            else
            {
                variables.Default.g_lat = marker.Latitude.ToString();
                variables.Default.g_lon = marker.Longitude.ToString();
            }            
        }
    }
}