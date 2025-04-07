// Arquivo usado para fazer a conexão com a API e receber os dados.

using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataServices
    {
        // <Tempo?> Se refere ao arquivo Tempo.cs na pasta Models.
        // O "<?>" Significa que o valor pode ser nulo.
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "c00731e93e6c7fef887f3e1bfdfec2e3";  // Chave de acesso para a API.

            string URL = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&APPID={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URL);

                try
                {

                    if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Noma de cidade Inválida", "OK");
                    }

                    if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();

                        var rascunho = JObject.Parse(json);

                        DateTime Time = new();
                        DateTime sunrise = Time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = Time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                        // Receber os valores do OpenWeather
                        t = new()
                        {
                             lat = (double)rascunho["coord"]["lat"],                         //Cordenadas Latitude
                             lon = (double)rascunho["coord"]["lon"],                         //Cordenadas Longitude
                             description = (string)rascunho["weather"][0]["description"],    //Descrição do clima
                             main = (string)rascunho["weather"][0]["main"],                  //Clima
                             temp_min = (double)rascunho["main"]["temp_min"],                //Temperatura Mínima
                             temp_max = (double)rascunho["main"]["temp_max"],                //Temperatura Máxima
                             speed = (double)rascunho["wind"]["speed"],                      //Velocidade do vento
                             visibility = (int)rascunho["visibility"],                       //Taxa de visibilidade
                             sunrise = sunrise.ToString(),                                   //Nascer do Sol
                             sunset =  sunset.ToString()                                     //Por do Sol

                        }; // Fecha objeto do tempo.
                    } // Fecha o if se o status do servidor tiver sucesso.
                }

                catch (HttpRequestException)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro de Conexão", "Sem conexão com a internet. Verifique sua rede.", "OK");
                }

            } // Fecha o laço do using.

            return t;
        }
    }
}
