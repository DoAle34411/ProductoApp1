using CommunityToolkit.Maui.Core;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class NuevoProductoPage : ContentPage
{
    private Producto _producto;
    private readonly APIServices _APIServices;
    public NuevoProductoPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        if ( _producto != null )
        {
            Nombre.Text = _producto.Nombre;
            Cantidad.Text = _producto.Cantidad.ToString();
            Descripcion.Text = _producto.Descripcion;
            Genero.Text = _producto.Genero;
            Autor.Text = _producto.Autor;
            Costo.Text = _producto.Costo.ToString();
        }
    }

    private async void OnClickGuardarNuevoProducto(object sender, EventArgs e)
    {
        try {
            if (_producto != null)
            {
                _producto.Nombre = Nombre.Text;
                _producto.Descripcion = Descripcion.Text;
                _producto.Cantidad = Int32.Parse(Cantidad.Text);
                _producto.Genero = Genero.Text;
                _producto.Autor = Autor.Text;
                _producto.Costo = Double.Parse(Costo.Text);
                await _APIServices.PutProducto(_producto.IdProducto, _producto);
                //Utils.Utils.ListaProductos.Insert(_producto.IdProducto, _producto);
                //await Navigation.
            }
            else 
            {
                //int id = Utils.Utils.ListaProducto.Count + 1;
                Producto producto = new Producto
                {

                    //IdProducto = id,
                    Nombre = Nombre.Text,
                    Descripcion = Descripcion.Text,
                    Cantidad = Int32.Parse(Cantidad.Text),
                    Genero = Genero.Text,
                    Autor = Autor.Text,
                    Costo = Int32.Parse(Costo.Text)
                };
                await _APIServices.PostProducto(producto);
            }
            
            await Navigation.PopAsync();

        }
        catch (Exception ex) {
            await DisplayAlert("Valores Incorrectos", "Inserte Valores Correctos", "Ok");
        }

    }
}