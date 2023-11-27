using ProductoApp1.Services;

namespace ProductoApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            APIServices aPIServices = new APIServices();
            MainPage = new NavigationPage(new ProductoPage(aPIServices));
            //MainPage = new ProductoPage();
        }
    }
}