using Syncfusion.Maui.Maps;

namespace WRWpr
{
    public class MapsBehavior : Behavior<ContentPage>
    {
        private MapTileLayer mapa;
        private MapMarker marker;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            this.mapa = bindable.FindByName<MapTileLayer>("mapa");
            this.mapa.Tapped += TileLayer_Tapped;
            this.marker = bindable.FindByName<MapMarker>("marker");
        }

        private void TileLayer_Tapped(object sender, Syncfusion.Maui.Maps.TappedEventArgs e)
        {
            var location = mapa.GetLatLngFromPoint(e.Position);
            marker.Latitude = location.Latitude;
            marker.Longitude = location.Longitude;

            //if (variables.Default.g_first < 2)
            //{
            //    variables.Default.g_first++;
            //}
            //else
            //{
            //    variables.Default.g_lat = marker.Latitude.ToString();
            //    variables.Default.g_lon = marker.Longitude.ToString();
            //}
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.mapa != null)
            {
                this.mapa.Tapped -= TileLayer_Tapped;
            }

            this.mapa = null;
            this.marker = null;
        }
    }
}