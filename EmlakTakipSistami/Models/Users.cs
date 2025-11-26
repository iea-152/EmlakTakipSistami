public class Users
{
    public int Id { get; set; }  // PRIMARY KEY zorunlu

    public string Ad { get; set; }
    public string Soyad { get; set; }

    public string KullaniciAdi { get; set; }

    public string Email { get; set; }

    public string Sifre { get; set; }  // Hash yapabilirsin ama bu hali de çalışır

    public string Rol { get; set; } = "User"; // Admin / User

    public DateTime KayitTarihi { get; set; } = DateTime.Now;

    public bool AktifMi { get; set; } = true;
}
