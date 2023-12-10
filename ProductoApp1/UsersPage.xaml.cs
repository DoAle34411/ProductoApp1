
using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class UsersPage : ContentPage
{
    private readonly APIServices _APIServices;
	public UsersPage(APIServices aPIServices)
    {
        InitializeComponent();
       _APIServices = aPIServices;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        List<User> ListaUsers = await _APIServices.GetUsers();
        var user = new ObservableCollection<User>(ListaUsers);
        listaUser.ItemsSource = user;
    }

    private async void OnClickNuevoUser(object sender, EventArgs e)
    {
        //var toast = Toast.Make("Click en nuevo producto", ToastDuration.Short, 14);

        //await toast.Show();

        await Navigation.PushAsync(new SignUpPage(_APIServices));
    }

    private async void OnClickShowDetailsUser(object sender, SelectedItemChangedEventArgs e)
    {
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Click en detalles evento", ToastDuration.Short, 14);
        await toast.Show();
        User user = e.SelectedItem as User;
        await Navigation.PushAsync(new DetalleUserPage(_APIServices)
        {
            BindingContext = user,
        });
    }

    private async void OnClickEditarUser(object sender, EventArgs e)
    {
        SwipeItem item = sender as SwipeItem;
        User user = item.BindingContext as User;
        await Navigation.PushAsync(new NuevoUserPage(_APIServices)
        {
            BindingContext = user,
        });
    }
}