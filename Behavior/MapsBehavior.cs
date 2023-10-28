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