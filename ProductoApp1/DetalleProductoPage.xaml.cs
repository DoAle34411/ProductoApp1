using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class DetalleProductoPage : ContentPage
{
    private Producto _producto;
    private readonly APIServices _APIServices;
    public int UserAccess { get; set; }
    public DetalleProductoPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        Nombre.Text = _producto.Nombre;
        Cantidad.Text = _producto.Cantidad.ToString();
        Descripcion.Text = _producto.Descripcion;
        Genero.Text = _producto.Genero;
        Autor.Text = _producto.Autor;
        string username = Preferences.Get("username", "0");
        string idUser = Preferences.Get("IdUser".ToString(), "0");
        int idUsuario = int.Parse(idUser);
        Models.User user = await _APIServices.GetUser(idUsuario);
        UserAccess = user.IdUsuario;
    }

    private async void OnClickVolver(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickRentar(object sender, EventArgs e)
    {
        int idUsuarioNum = UserAccess;
        int idLibro = _producto.IdProducto;
        Models.User user = await _APIServices.GetUser(idUsuarioNum);
        bool puedeRentar = user.HaRetirado;
        if (puedeRentar)
        {
            await DisplayAlert("Incorrecto", "Tiene que devolver los libros retirados antes de rentar uno nuevo", "Ok");
            await Navigation.PopAsync();
        }
        else 
        {
            await DisplayAlert("Correcto", "Acerquese al mostrador con el libro, IdUsuario="+idUsuarioNum+" e IdLibro="+idLibro+" para rentar el libro", "Ok");
            await Navigation.PopAsync();
        }
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