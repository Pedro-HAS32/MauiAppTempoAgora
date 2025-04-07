namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        public double? lon { get; set; }         // Longitude
        public double? lat { get; set; }         // Latiture
        public int? humidity { get; set; }       // Umidade do ar
        public double? temp_min { get; set; }    // Temperatura minima
        public double? temp_max { get; set; }    // Temperatura maxima
        public int? visibility { get; set; }     // Visibilidade
        public string? sunrise { get; set; }     // Nascer do sol
        public string? sunset { get; set; }      // Por do sol
        public string? main { get; set; }        // Principal
        public string? description { get; set; } // Descrição
        public double? speed { get; set; }       // Velocidade do vento

        //Opcional
        public double? temp { get; set; }        // Temperatura
        public int? sea_level { get; set; }      // Nível do mar
        public int? grnd_level { get; set; }     // Nível do solo
        public string? country { get; set; }     // País
        public double? feels_like { get; set; }  // Sensação térmica
    }
}
