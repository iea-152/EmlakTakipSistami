using EmlakTakipSistami.Models;
using System.ComponentModel.DataAnnotations;

namespace EmlakTakipSistami.Models
{
    public class Daire
    {
        [Key]
        public int Id { get; set; }

        public decimal KiraUcreti { get; set; }  // Kira Ücreti

        // Bu iki alan JSON'daki veriyi göstermek için yeterli
        [Required]
        public string Il { get; set; }       // İl ID
        [Required]
        public string Ilce { get; set; }     // İlçe ID
                                             // Burada tip ICollection<Kiraci> olmalı
        public ICollection<Kiraci> Kiracilar { get; set; } = new List<Kiraci>();
    }
}

