using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using ProductoApp1.Models;
using ProductoApp1.Services;
namespace ProductoApp1
{
    public partial class MenuPrincipal : ContentPage
    {
        private readonly APIServices _APIServices;
        public int UserAccess { get; set; }
        public MenuPrincipal(APIServices aPIServices)
        {
            InitializeComponent();
            _APIServices = aPIServices;
            BindingContext = this;
        }
        private async void ValidarLogin()
        {
            if (Preferences.Get("username", "0").Equals("0"))
            {
                Console.WriteLine(1);
                await Navigation.PushAsync(new Login(_APIServices));
            }
        }
        protected override async void OnAppearing()
        {
            Console.WriteLine(10);
            base.OnAppearing();
            ValidarLogin();
            string username = Preferences.Get("username", "0");
            string idUser = Preferences.Get("IdUser".ToString(),"0");
            int idUsuario = int.Parse(idUser);
            User user = await _APIServices.GetUser(idUsuario);
            Username.Text = user.Nombres;
            UserAccess = user.CodigoAcceso;
        }
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductoPage(_APIServices));
        }
        private async void OnEventosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventosPage(_APIServices));
        }
        private async void OnUsersClicked(object sender, EventArgs e)
        {
            if (UserAccess == 1)
            {
                await Navigation.PushAsync(new UsersPage(_APIServices));
            }
            else
            {
                await DisplayAlert("Permisos", "No cuenta con los permisos requeridos", "Ok");
            }
        }
        private async void OnCloseClicked(object sender, EventArgs e)
        {
            Preferences.Set("username", "0");
            await Navigation.PushAsync(new Login(_APIServices));
        }
    }
}