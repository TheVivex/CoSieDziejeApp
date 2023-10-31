namespace WRWpr
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(keyAuth.SyncfusionKey);
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}