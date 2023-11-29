
using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class ProductoPage : ContentPage
{
    private readonly APIServices _APIServices;
	public ProductoPage(APIServices aPIServices)
    {
        InitializeComponent();
       _APIServices = aPIServices;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        List<Producto> ListaProducto = await _APIServices.GetProductos();
        var productos = new ObservableCollection<Producto>(ListaProducto);
        listaProductos.ItemsSource = productos;
    }

    private async void OnClickNuevoProducto(object sender, EventArgs e)
    {
        //var toast = Toast.Make("Click en nuevo producto", ToastDuration.Short, 14);

        //await toast.Show();

        await Navigation.PushAsync(new NuevoProductoPage(_APIServices));
    }

    private async void OnClickShowDetails(object sender, SelectedItemChangedEventArgs e)
    {
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Click en editar producto", ToastDuration.Short, 14);
        await toast.Show();
        Producto producto = e.SelectedItem as Producto;
        await Navigation.PushAsync(new DetalleProductoPage(_APIServices)
        {
            BindingContext = producto,
        });
    }

    private async void OnClickEditarProducto(object sender, EventArgs e)
    {
        SwipeItem item = sender as SwipeItem;
        Producto producto = item.BindingContext as Producto;
        await Navigation.PushAsync(new NuevoProductoPage(_APIServices)
        {
            BindingContext = producto,
        });
    }
    private async void OnClickBorrarProducto(object sender, EventArgs e)
    {
        SwipeItem item = sender as SwipeItem;
        Producto producto = item.BindingContext as Producto;
        await _APIServices.DeleteProducto(producto.IdProducto);
        await Navigation.PopAsync();
        await Navigation.PushAsync(new ProductoPage(_APIServices));
        await Navigation.PopAsync();
    }
}