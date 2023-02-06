using System;
using System.Collections.Generic;

namespace ToDoApi.Models
{
    public partial class Automobili
    {
        public int Id { get; set; }
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public DateTime? Godiste { get; set; }
        public int? ZapreminaMotora { get; set; }
        public int? Snaga { get; set; }
        public string? Gorivo { get; set; }
        public string? Karoserija { get; set; }
        public string? Opis { get; set; }
        public decimal? Cena { get; set; }
        public string? Kontakt { get; set; }
    }
}
