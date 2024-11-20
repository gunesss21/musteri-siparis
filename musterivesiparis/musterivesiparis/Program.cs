using System;
using System.Collections.Generic;
using System.Linq;

// Ürün sınıfı: Ürün bilgilerini temsil eder
public class Urun
{
    public int UrunId { get; private set; }
    public string Ad { get; private set; }
    public string Telefon { get; private set; }

    public Urun(int urunId, string ad, string telefon)
    {
        UrunId = urunId;
        Ad = ad;
        Telefon = telefon;
    }

    public override string ToString()
    {
        return $"Ürün ID: {UrunId}, Ad: {Ad}, Telefon: {Telefon}";
    }
}

// Sipariş sınıfı: Sipariş bilgilerini temsil eder
public class Siparis
{
    public int SiparisId { get; private set; }
    public Urun Urun { get; private set; }
    public int Miktar { get; private set; }
    public DateTime SiparisTarihi { get; private set; }
    public string Durum { get; private set; }

    public Siparis(int siparisId, Urun urun, int miktar, string durum)
    {
        SiparisId = siparisId;
        Urun = urun ?? throw new ArgumentNullException(nameof(urun), "Ürün boş olamaz.");
        Miktar = miktar;
        SiparisTarihi = DateTime.Now;
        Durum = durum;
    }

    public override string ToString()
    {
        return $"Sipariş ID: {SiparisId}, {Urun}, Miktar: {Miktar}, Tarih: {SiparisTarihi}, Durum: {Durum}";
    }
}

// Sipariş Yönetimi sınıfı: Siparişlerin müşteri referansı olmadan yönetilmesini sağlar
public class SiparisYoneticisi
{
    private List<Siparis> Siparisler { get; set; }

    public SiparisYoneticisi()
    {
        Siparisler = new List<Siparis>();
    }

    // Sipariş ekleme metodu
    public void SiparisEkle(Siparis siparis)
    {
        if (siparis == null)
        {
            throw new ArgumentNullException(nameof(siparis), "Sipariş boş olamaz.");
        }

        if (Siparisler.Any(s => s.SiparisId == siparis.SiparisId))
        {
            throw new InvalidOperationException($"Sipariş ID {siparis.SiparisId} zaten mevcut.");
        }

        Siparisler.Add(siparis);
    }

    // Tüm siparişleri listeleme metodu
    public List<Siparis> TumSiparisleriGetir()
    {
        return Siparisler;
    }

    // Belirli bir siparişi ID'ye göre bulma metodu
    public Siparis SiparisiGetir(int siparisId)
    {
        return Siparisler.FirstOrDefault(s => s.SiparisId == siparisId) 
               ?? throw new KeyNotFoundException($"Sipariş ID {siparisId} bulunamadı.");
    }
}

// Test amaçlı kullanım
public class Program
{
    public static void Main(string[] args)
    {
        // Ürünler oluşturuluyor
        var urun1 = new Urun(1, "Laptop", "123-456-7890");
        var urun2 = new Urun(2, "Mouse", "987-654-3210");

        Console.WriteLine("Ürünler:");
        Console.WriteLine(urun1);
        Console.WriteLine(urun2);

        // Sipariş yönetimi sistemi
        var siparisYoneticisi = new SiparisYoneticisi();

        // Siparişler ekleniyor
        var siparis1 = new Siparis(101, urun1, 1, "Onaylandı");
        var siparis2 = new Siparis(102, urun2, 2, "Hazırlanıyor");

        siparisYoneticisi.SiparisEkle(siparis1);
        siparisYoneticisi.SiparisEkle(siparis2);

        // Tüm siparişleri listeleme
        Console.WriteLine("\nTüm Siparişler:");
        foreach (var siparis in siparisYoneticisi.TumSiparisleriGetir())
        {
            Console.WriteLine(siparis);
        }

        // Belirli bir siparişi ID ile bulma
        Console.WriteLine("\nBelirli Sipariş:");
        var belirliSiparis = siparisYoneticisi.SiparisiGetir(102);
        Console.WriteLine(belirliSiparis);
    }
}
