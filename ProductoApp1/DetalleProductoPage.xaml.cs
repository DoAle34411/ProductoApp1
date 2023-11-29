using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class DetalleProductoPage : ContentPage
{
    private Producto _producto;
    private readonly APIServices _APIServices;
    public DetalleProductoPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        Nombre.Text = _producto.Nombre;
        Cantidad.Text = _producto.Cantidad.ToString();
        Descripcion.Text = _producto.Descripcion;
        Genero.Text = _producto.Genero;
        Autor.Text = _producto.Autor;
        Costo.Text = _producto.Costo.ToString();
    }

    private async void OnClickVolver(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickEditar(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevoProductoPage(_APIServices)
        {
            BindingContext = _producto,
        });
    }
    private async void OnClickBorrar(object sender, EventArgs e)
    {
        await _APIServices.DeleteProducto(_producto.IdProducto);
        await Navigation.PopAsync();
    }
}