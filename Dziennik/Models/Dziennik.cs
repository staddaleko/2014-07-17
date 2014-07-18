using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace AppDziennik.Models
{
    public class ExcludeChar : ValidationAttribute
    {
        private readonly string _chars;

        public ExcludeChar(string chars)
            : base("{0} contains invalid character!")
        {
            _chars = chars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                for (int i = 0; i < _chars.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(_chars[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    public class CzyParzysta : ValidationAttribute
    {
        public CzyParzysta()
            : base("{0} jest parzysta!") { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                string test = "13579";
                if (test.Contains(valueAsString[valueAsString.Length - 1]))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }

            }
            return ValidationResult.Success;
        }
    }
    //public class MyRegex : ValidationAttribute
    //{
    //    private readonly string _regex;

    //    public MyRegex(string my_regex):base("{0} contains invalid character!")
    //    {
    //        _regex = my_regex;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        if (value !=null)
    //        {
    //            for (int i = 0; i < _regex.Length; i++)
    //            {
    //                var valueAsString = value.ToString();
    //                if (valueAsString.Contains(_regex[i]))
    //                {
    //                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
    //                    return new ValidationResult(errorMessage);
    //                }
    //            }
    //        }
    //        return ValidationResult.Success;
    //    }
    //}

    public class MyRegex : ValidationAttribute
    {
        private readonly string _regex;

        public MyRegex()
            : base("{0} jest niezgodna z wymaganiami!") { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                var ValReg = new Regex(_regex);
                //string test = "13579";
                if (!ValReg.IsMatch(valueAsString))
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }

            }
            return ValidationResult.Success;
        }
    }

    public class Uczen
    {
        [Key]
        [Required]
        public int Id_u { set; get; }
        [MinLength(3, ErrorMessage = "Imię ma co najmniej 3 znaki")]
        [MaxLength(30, ErrorMessage = "Masz najdłuższe imię na świecie? Hehe. Skróć je. :D")]
        [Required(ErrorMessage = "Nie no... musisz mieć imię jakieś...")]
        [ExcludeChar("~`!@#$%^&*()_+=_|{}][:'<>,.?/")]//not sure
        public string Imie { set; get; }
        [Required(ErrorMessage = "No podziel się nazwiskiem...")]
        [MinLength(3, ErrorMessage = "Nazwisko ma co najmniej 3 znaki")]
        [MaxLength(50, ErrorMessage = "Masz najdłuższe nazwisko na świecie? Hehe. Skróć je. :D")]
        public string Nazwisko { set; get; }
        [Required]
        [Range(20, 50, ErrorMessage = "Wiek od 20 do 50 lat")]
        public int Wiek { set; get; }
        [Required(ErrorMessage = "Klasę niestety też musisz wprowadzić - od 1 do 8")]
        [Range(1, 8, ErrorMessage = "Ktoś tu nie UMI czytać... klasy od 1 do 8")]
        public int Klasa { set; get; }
        public ICollection<Ocena> _O { set; get; }

        public Uczen()
        {
            var db = new DBDziennik();
            this._O = db.Oceny.Where(m => m.Id_u == this.Id_u).ToList();

        }
    }

    public class UczenDataContext : DbContext
    {
        public UczenDataContext()
            : base("Dziennik")
        {
        }
        public DbSet<Uczen> Uczniowie { set; get; }
    }

    public class Ocena
    {
        [Key]
        [Required]

        public int Id_o { set; get; }
        [Required]
        public int Id_u { set; get; }
        [Required]
        public int Id_p { set; get; }
        [Required]
        public int Wartosc { set; get; }
        public Uczen U { set; get; }
        public Przedmiot P { set; get; }

        public Ocena()
        {

        }

        public Ocena(int id_u, int id_p)
        {
            var db = new DBDziennik();
            Id_u = id_u;
            U = new Uczen();
            U.Imie = db.Uczniowie.Find(Id_u).Imie;
            U.Nazwisko = db.Uczniowie.Find(Id_u).Nazwisko;
            U.Wiek = db.Uczniowie.Find(Id_u).Wiek;
            U.Klasa = db.Uczniowie.Find(Id_u).Klasa;
            Id_p = id_p;
            P = new Przedmiot();
            P.Nazwa = db.Przedmioty.Find(Id_p).Nazwa;
        }
    }

    public class Ocena_Przedmiot
    {
        public string Nazwa { set; get; }
        public int Wartosc { set; get; }
    }

    public class OcenaDataContext : DbContext
    {
        public OcenaDataContext()
            : base("Dziennik")
        {
        }
        public DbSet<Ocena> Oceny { set; get; }
    }

    public class Przedmiot
    {
        [Key]
        public int Id_p { set; get; }
        public string Nazwa { set; get; }
        public ICollection<Ocena> _O { set; get; }

        public Przedmiot()
        {
            var db = new DBDziennik();
            this._O = db.Oceny.Where(m => m.Id_p == this.Id_p).ToList();
        }
    }

    public class PrzedmiotDataContext : DbContext
    {
        public PrzedmiotDataContext()
            : base("Dziennik")
        {
        }
        public DbSet<Przedmiot> Przedmioty { set; get; }
    }

    public class DBDziennik : DbContext
    {
        public DBDziennik()
            : base("Dziennik")
        {
        }
        public DbSet<Uczen> Uczniowie { set; get; }
        public DbSet<Ocena> Oceny { set; get; }
        public DbSet<Przedmiot> Przedmioty { set; get; }
    }
}
