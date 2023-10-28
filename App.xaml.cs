namespace WRWpr
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1JpQXxbf1xzZFBMZFpbQHdPMyBoS35RdURjW3dedXBTQmFaWEBz");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}