using CommunityToolkit.Maui.Core;
using ProductoApp1.Models;
using ProductoApp1.Services;

namespace ProductoApp1;

public partial class NuevoEventosPage : ContentPage
{
    private Eventos _evento;
    private readonly APIServices _APIServices;
    public NuevoEventosPage(APIServices apiservice)
    {
        InitializeComponent();
        _APIServices = apiservice;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _evento = BindingContext as Eventos;
        if (_evento != null )
        {
            Nombre.Text = _evento.NombreEvento;
            Descripcion.Text = _evento.DescripcionEvento;
            Expositores.Text = _evento.Expositores;
            anioEvento.Text = _evento.diaEvento.Year.ToString();
            mesEvento.Text = _evento.diaEvento.Month.ToString();
            diaEvento2.Text = _evento.diaEvento.Day.ToString();
            horaEvento2.Text = _evento.horaEvento.Hours.ToString();
            minutoEvento.Text = _evento.horaEvento.Minutes.ToString();
        }
    }

    private async void OnClickGuardarNuevoProducto(object sender, EventArgs e)
    {
        try {
            if (_evento != null)
            {
                _evento.NombreEvento = Nombre.Text;
                _evento.DescripcionEvento = Descripcion.Text;
                _evento.Expositores = Expositores.Text;
                _evento.diaEvento = new DateTime(Int32.Parse(anioEvento.Text), Int32.Parse(mesEvento.Text), Int32.Parse(diaEvento2.Text));
                _evento.horaEvento = new TimeSpan(Int32.Parse(horaEvento2.Text), Int32.Parse(minutoEvento.Text), 0);
                _evento.IdUsuario = 0;
                await _APIServices.PUTEventos(_evento.idEvento, _evento);
            }
            else 
            {
                //int id = Utils.Utils.ListaProducto.Count + 1;
                Eventos evento = new Eventos
                {
                    NombreEvento = Nombre.Text,
                    DescripcionEvento = Descripcion.Text,
                    Expositores = Expositores.Text,
                    diaEvento = new DateTime(Int32.Parse(anioEvento.Text), Int32.Parse(mesEvento.Text), Int32.Parse(diaEvento2.Text)),
                    horaEvento = new TimeSpan(Int32.Parse(horaEvento2.Text), Int32.Parse(minutoEvento.Text), 0),
                    IdUsuario = 0,
                };
                await _APIServices.POSTEventos(evento);
            }
            
            await Navigation.PopAsync();

        }
        catch (Exception ex) {
            await DisplayAlert("Valores Incorrectos", "Inserte Valores Correctos", "Ok");
        }

    }
}