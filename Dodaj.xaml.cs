using openstreetmapAPI;

namespace WRWpr;

public partial class Dodaj : ContentPage
{
    DataBaseConnection con1 = new DataBaseConnection();
	public Dodaj()
	{
		InitializeComponent();
	}
    private string filename = "";
    private void Dodaj_zdjecie_Clicked(object sender, EventArgs e)
    {     

        PickOptions options = new()
        {
            PickerTitle = "Please select a image file",
            FileTypes = FilePickerFileType.Images,
        };
        PickAndShow(options);        
    }

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                    filename = result.FullPath;
                }
            }
            Dodaj_zdjecie.Text = "Dodano zdjêcie!";
            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return null;
    }

    private void Dodaj_wydarzenie_Clicked(object sender, EventArgs e)
    {
        int x = con1.AddNewMarker(2, Nazwa.Text, Dostepnosc.SelectedIndex, Opis.Text, Data.Date.ToString("dd.MM.yyyy"), string.Format("{0}:{1}", Godz_start.Time.Hours, Godz_start.Time.Minutes), string.Format("{0}:{1}", Godz_stop.Time.Hours, Godz_stop.Time.Minutes), Kategoria.SelectedIndex, Oplata.Text, Link.Text, "1", "2", filename);
        if(x == 0)
        {
            Dodaj_wydarzenie.Text = "Dodano pomyœlnie!";
        }
        
    }

}