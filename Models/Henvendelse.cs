using Newtonsoft.Json;

namespace IBASEmployeeService.Models
{
    public class Henvendelse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id {get;set;}
        public string? Beskrivelse {get;set;}

        public string Dato {get;set;}

        [JsonProperty(PropertyName = "kategori")]
        public string Kategori  {get;set;} 

        public Bruger Bruger {get;set; }


    }
}