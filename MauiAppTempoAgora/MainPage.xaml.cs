//using Android.OS;
//using Android.Provider;
using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Previsao(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataServices.GetPrevisao(txt_cidade.Text);

                    if(t != null)
                    {
                        string dados_previsão = "";

                        dados_previsão = $"Latitude:                {t.lat}             \n" +
                                         $"Longitude:               {t.lon}             \n" +
                                         $"Nascer do Sol:           {t.sunrise}         \n" +
                                         $"Por do sol:              {t.sunset}          \n" +
                                         $"Temperatura máxima:      {t.temp_max}        \n" +
                                         $"Temperatura Mínima:      {t.temp_min}        \n" +
                                         $"Descrição:               {t.description}     \n" +
                                         $"Velocidade do Vento:     {t.speed}           \n" +
                                         $"Visibilidade:            {t.visibility}      \n" ;

                        lbl_resp.Text = dados_previsão;
                    }
                    else
                    {
                        lbl_resp.Text = "Sem dados de previsão";
                    }
                }
                else
                {
                    lbl_resp.Text = "Preencha a cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void Button_Clicked_Localizacao(object sender, EventArgs e)
        {
            try
            {
                //Faz a requisição da Geolocation.
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                Location? local = await Geolocation.Default.GetLocationAsync(request);

                if(local != null)
                {
                    string local_dispositivo = $"Latitude: {local.Latitude}     \n" +
                                               $"Longitude: {local.Longitude}   \n";

                    lbl_coords.Text = local_dispositivo;
                }
                else
                {
                    lbl_coords.Text = "Nenhuma Localização";
                }
            }

            //Mosta uma mensagem de erro caso o dispositivo não suporte a feature.
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Dispositivo não suporta", fnsEx.Message, "OK");
            }

            //Mosta uma mensagem de erro caso a feature não esteja atividad.
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Feature não está habilitada", fneEx.Message, "OK");
            }

            //Mostra uma mensagem de erro caso a permissão da localização não esteja habilitada.
            catch (PermissionException perEx)
            {
                await DisplayAlert("Erro: Sem permissão de localização", perEx.Message, "OK");
            }

            //Mostra uma mensagem para erros genéricos.
            catch (Exception ex)
            {
                await DisplayAlert("Erro: ", ex.Message, "OK");
            }
        }
    }
}