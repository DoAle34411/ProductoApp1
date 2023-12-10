using CommunityToolkit.Maui.Core;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class SignUpPage : ContentPage
{
    private readonly APIServices _APIServices;
    public SignUpPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void OnClickGuardarNuevoUsuario(object sender, EventArgs e)
    {
        try {
            User user = new User
            {
                Cedula = Cedula.Text,
                Nombres = Nombres.Text,
                Apellidos = Apellidos.Text,
                Clave = Clave.Text,
                CodigoAcceso=4,
                HaRetirado=false,
                IdLibroRetirado=1,
                IdUsuarioActivo = 0,
            };
            await _APIServices.POSTUser(user);
            await Navigation.PopAsync();
        }
        catch (Exception ex) {
            await DisplayAlert("Valores Incorrectos", "Inserte Valores Correctos", "Ok");
        }

    }
}