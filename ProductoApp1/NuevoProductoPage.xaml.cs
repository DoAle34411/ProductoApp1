using CommunityToolkit.Maui.Core;
using ProductoApp1.Models;

namespace ProductoApp1;

public partial class NuevoProductoPage : ContentPage
{
    private Producto _producto;
	public NuevoProductoPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        if ( _producto != null )
        {
            Nombre.Text = _producto.Nombre;
            Descripcion.Text=_producto.Descripcion;
            cantidad.Text=_producto.cantidad.ToString();
        }
    }

    private async void OnClickGuardarNuevoProducto(object sender, EventArgs e)
    {
        try {
            Console.WriteLine("hELLO");
            if (_producto != null)
            {
                _producto.Nombre = Nombre.Text;
                _producto.Descripcion = Descripcion.Text;
                _producto.cantidad = Int32.Parse(cantidad.Text);
                //Utils.Utils.ListaProductos.Insert(_producto.IdProducto, _producto);
                //await Navigation.
            }
            

            Producto producto = new Producto
            {
                IdProducto = 0,
                Nombre = Nombre.Text,
                Descripcion = Descripcion.Text,
                cantidad = Int32.Parse(cantidad.Text)
            };

            Utils.Utils.ListaProductos.Add(producto);

            await Navigation.PopAsync();

        }
        catch (Exception ex) {
            await DisplayAlert("Valores Incorrectos", "Inserte Valores Correctos", "Ok");
        }

    }
}