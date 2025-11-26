using System;
using System.ComponentModel.DataAnnotations;

namespace EmlakTakipSistami.Models
{
    public class Kiraci
    {
        public int Id { get; set; }

        
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? DogumTarihi { get; set; }

        // Daire ile ilişki
        [Display(Name = "Daire")]
        public int DaireId { get; set; }
        public Daire Daire { get; set; }
    }
}

