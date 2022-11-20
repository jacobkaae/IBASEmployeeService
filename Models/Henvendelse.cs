namespace IBASEmployeeService.Models
{
    public class Henvendelse
    {
        public int Id {get;set;}
        public string? Beskrivelse {get;set;}

        public string Dato {get;set;}

        public string Kategori  {get;set;} 

        public Bruger bruger {get;set; }


    }
}