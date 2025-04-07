//using Android.OS;
//using Android.Provider;
using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
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
    }
}
