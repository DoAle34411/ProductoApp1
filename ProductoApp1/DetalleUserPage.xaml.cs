using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class DetalleUserPage : ContentPage
{
    private User _user;
    private readonly APIServices _APIServices;
    public DetalleUserPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _user = BindingContext as User;
        Nombres.Text = _user.Nombres;
        Apellidos.Text = _user.Apellidos;
        Cedula.Text = _user.Cedula;
        CodigoAcceso.Text = _user.CodigoAcceso.ToString();
    }

    private async void OnClickVolver(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickEditar(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevoUserPage(_APIServices)
        {
            BindingContext = _user,
        });
    }
}