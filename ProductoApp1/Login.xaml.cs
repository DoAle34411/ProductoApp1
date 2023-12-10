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
    public partial class Login : ContentPage
    {

        private readonly APIServices _APIService;

        public Login(APIServices apiservice)
        {
            InitializeComponent();
            _APIService = apiservice;
        }

        private async void OnClickLogin(object sender, EventArgs e)
        {
            string username = Cedula.Text;
            string password = Clave.Text;
            Console.WriteLine(2);
            User usuario_encontrado = await _APIService.GetUser(username, password);
            if (usuario_encontrado != null)
            {
                Console.WriteLine(3);
                Preferences.Set("username", usuario_encontrado.Cedula);
                Preferences.Set("IdUser", usuario_encontrado.IdUsuario.ToString());
                await Navigation.PopAsync();
            }
            else {
                await DisplayAlert("Valores Incorrectos", "Inserte Valores Correctos", "Ok");
            }
        }
        private async void OnClickSignUp(object sender, EventArgs e) 
        {
            await Navigation.PushAsync(new SignUpPage(_APIService));
        }

    }
}