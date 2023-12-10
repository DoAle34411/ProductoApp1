using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class DetalleEventosPage : ContentPage
{
    private Eventos _evento;
    private readonly APIServices _APIServices;
    public DetalleEventosPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _evento = BindingContext as Eventos;
        Nombre.Text = _evento.NombreEvento;
        Descripcion.Text = _evento.DescripcionEvento;
        Expositores.Text = _evento.Expositores;
        Fecha.Text = _evento.diaEvento.ToString();
        Hora.Text = _evento.horaEvento.ToString();
    }

    private async void OnClickVolver(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickEditar(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevoEventosPage(_APIServices)
        {
            BindingContext = _evento,
        });
    }
    private async void OnClickBorrar(object sender, EventArgs e)
    {
        await _APIServices.DeleteProducto(_evento.idEvento);
        await Navigation.PopAsync();
    }
}