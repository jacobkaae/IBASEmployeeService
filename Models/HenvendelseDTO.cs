using Newtonsoft.Json;

namespace IBASEmployeeService.Models
{
    public class HenvendelseDTO
    {
        [JsonProperty(PropertyName = "beskrivelse")]
        public string? Beskrivelse {get;set;}

        [JsonProperty(PropertyName = "dato")]
        public string Dato {get;set;}

        [JsonProperty(PropertyName = "kategori")]
        public string Kategori  {get;set;}

        [JsonProperty(PropertyName = "bruger")]
        public Bruger Bruger {get;set; }


    }
}