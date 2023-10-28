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
       
    }
}