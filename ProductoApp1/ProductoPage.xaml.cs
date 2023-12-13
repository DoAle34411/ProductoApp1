
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
    public int UserAccess { get; set; }
    public ProductoPage(APIServices aPIServices)
    {
        InitializeComponent();
       _APIServices = aPIServices;
        BindingContext = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        List<Producto> ListaProducto = await _APIServices.GetProducts();
        for (int i = 0; i < ListaProducto.Count; i++)
        {
            ListaProducto.ElementAt(i).urlImage = "https:\\\\crud-mvc20231209223757.azurewebsites.net" + ListaProducto.ElementAt(i).urlImage;
            ListaProducto.ElementAt(i).urlImage = ListaProducto.ElementAt(i).urlImage.Replace("\\", "/");
        }
        List<Producto> ListaProductos2 = new List<Producto>();
        ListaProductos2 = ListaProducto;
        Console.WriteLine(ListaProductos2.ElementAt(1).urlImage);
        var productos = new ObservableCollection<Producto>(ListaProductos2);
        var productosAdmin = new ObservableCollection<Producto>(ListaProducto);
        listaProductosUser.ItemsSource = productos;
        listaProductos.ItemsSource=productosAdmin;
        string idUser = Preferences.Get("IdUser", "0");
        int idUsuario = int.Parse(idUser);
        User user = await _APIServices.GetUser(idUsuario);
        UserAccess = user.CodigoAcceso;
        if (UserAccess == 1)
        {
            BotonNuevo.IsVisible = true;
            listaProductos.IsVisible = true;
            listaProductosUser.IsVisible = false;
        }
        else 
        {
            BotonNuevo.IsVisible = false;
            listaProductos.IsVisible = false;
            listaProductosUser.IsVisible = true;
        }
    }

    private async void OnClickNuevoProducto(object sender, EventArgs e)
    {
        if (UserAccess == 1)
        {
            await Navigation.PushAsync(new NuevoProductoPage(_APIServices));
        }
        else
        {
            await DisplayAlert("Permisos", "No cuenta con los permisos requeridos", "Ok");
        }
        
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
        if (UserAccess == 1)
        {
            SwipeItem item = sender as SwipeItem;
            Producto producto = item.BindingContext as Producto;
            await Navigation.PushAsync(new NuevoProductoPage(_APIServices)
            {
                BindingContext = producto,
            });
        }
        else
        {
            await DisplayAlert("Permisos", "No cuenta con los permisos requeridos", "Ok");
        }
    }
    private async void OnClickBorrarProducto(object sender, EventArgs e)
    {
        if (UserAccess == 1)
        {
            SwipeItem item = sender as SwipeItem;
            Producto producto = item.BindingContext as Producto;
            await _APIServices.DeleteProducto(producto.IdProducto);
            await Navigation.PopAsync();
            await Navigation.PushAsync(new ProductoPage(_APIServices));
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Permisos", "No cuenta con los permisos requeridos", "Ok");
        }
    }
}