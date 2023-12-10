using CommunityToolkit.Maui.Core;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class NuevoUserPage : ContentPage
{
    private User _user;
    private readonly APIServices _APIServices;
    public NuevoUserPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _user = BindingContext as User;
        if ( _user != null )
        {
            Nombres.Text = _user.Nombres;
            Apellidos.Text = _user.Apellidos;
            Cedula.Text = _user.Cedula;
            CodigoAcceso.Text=_user.CodigoAcceso.ToString();
        }
    }

    private async void OnClickGuardarNuevoProducto(object sender, EventArgs e)
    {
        try {
            if (_user != null)
            {
                _user.Nombres = Nombres.Text;
                _user.Apellidos = Apellidos.Text;
                _user.Cedula = Cedula.Text;
                _user.CodigoAcceso = Int32.Parse(CodigoAcceso.Text);
                _user.IdUsuarioActivo = 0;
                await _APIServices.PutUser(_user.IdUsuario, _user);
            }
            else 
            {
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Valores Incorrectos", "Inserte Valores Correctos", "Ok");
        }

    }
}