using openstreetmapAPI;
using static openstreetmapAPI.DataBaseConnection;

namespace WRWpr;

public partial class DetailPage : ContentPage
{
    

    DataBaseConnection connection3 = new DataBaseConnection();

    public DetailPage(int id)
	{
		InitializeComponent();
        List<string> p = connection3.GetMarkerInfo(id);

        label.Text = string.Format("{0}\nOpis:{1}\nKategoria:{6}\nData:{2}\nGodziny:{3}\nAdres:{4}\nStrona Internetowa {5}\n\n", p[0], p[1], p[2], p[3], p[6], p[7], p[9]);
	}
}