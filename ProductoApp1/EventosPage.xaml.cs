
using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class EventosPage : ContentPage
{
    private readonly APIServices _APIServices;
    public int UserAccess { get; set; }
    public EventosPage(APIServices aPIServices)
    {
        InitializeComponent();
       _APIServices = aPIServices;
        BindingContext = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        List<Eventos> ListaEventos = await _APIServices.GetEventos();
        var eventos = new ObservableCollection<Eventos>(ListaEventos);
        listaEventos.ItemsSource = eventos;
        listaEventos2.ItemsSource = eventos;
        string idUser = Preferences.Get("IdUser", "0");
        int idUsuario = int.Parse(idUser);
        User user = await _APIServices.GetUser(idUsuario);
        UserAccess = user.CodigoAcceso;
        if (UserAccess == 1)
        {
            NuevoEventoBut.IsVisible = true;
            listaEventos.IsVisible = true;
            listaEventos2.IsVisible = false;
        }
        else
        {
            NuevoEventoBut.IsVisible = false;
            listaEventos.IsVisible = false;
            listaEventos2.IsVisible = true;
        }
    }

    private async void OnClickNuevoProducto(object sender, EventArgs e)
    {
        if (UserAccess == 1)
        {
            await Navigation.PushAsync(new NuevoEventosPage(_APIServices));
        }
        else 
        {
            await DisplayAlert("Permisos", "No cuenta con los permisos requeridos", "Ok");
        }
    }

    private async void OnClickShowDetails(object sender, SelectedItemChangedEventArgs e)
    {
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Click en detalle evento", ToastDuration.Short, 14);
        await toast.Show();
        Eventos evento = e.SelectedItem as Eventos;
        await Navigation.PushAsync(new DetalleEventosPage(_APIServices)
        {
            BindingContext = evento,
        });
    }

    private async void OnClickEditarProducto(object sender, EventArgs e)
    {
        if (UserAccess == 1)
        {
            SwipeItem item = sender as SwipeItem;
            Eventos evento = item.BindingContext as Eventos;
            await Navigation.PushAsync(new NuevoEventosPage(_APIServices)
            {
                BindingContext = evento,
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
            Eventos evento = item.BindingContext as Eventos;
            await _APIServices.DeleteEventos(evento.idEvento);
            await Navigation.PopAsync();
            await Navigation.PushAsync(new EventosPage(_APIServices));
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Permisos", "No cuenta con los permisos requeridos", "Ok");
        }
    }
}