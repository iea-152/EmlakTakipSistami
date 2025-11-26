using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmlakTakipSistami.Models
{
    public class Kiraci
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        public string AdSoyad { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? DogumTarihi { get; set; }

        // Daire ile ilişki
        [Display(Name = "Daire")]
        public int DaireId { get; set; }

        // [ValidateNever] sayesinde form gönderilirken "Daire bilgisi boş" hatası almayacaksınız.
        [ValidateNever]
        public Daire Daire { get; set; }
    }
}