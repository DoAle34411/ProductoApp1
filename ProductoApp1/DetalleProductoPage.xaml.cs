using ProductoApp1.Models;

namespace ProductoApp1;

public partial class DetalleProductoPage : ContentPage
{
    private Producto _producto;


    public DetalleProductoPage()
	{
		InitializeComponent();
       
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        Nombre.Text = _producto.Nombre;
        Cantidad.Text = _producto.cantidad.ToString();
        Descripcion.Text = _producto.Descripcion;
    }

    private async void OnClickVolver(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickEditar(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NuevoProductoPage()
        {
            BindingContext = _producto,
        });
    }
    private async void OnClickBorrar(object sender, EventArgs e)
    {
        Utils.Utils.ListaProductos.Remove(_producto);
        await Navigation.PopAsync();
    }
}