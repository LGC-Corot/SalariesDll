using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SalariesDll
{
    public class Salarie
    {
        static void Main(string[] args)
        {
        }
        private string _matricule;
        private string _nom;
        private string _prenom;
        private decimal _salaireBrut;
        private decimal _tauxCS;
        private DateTime _dateNaissance;
        //private decimal _salaireNet;

        public string Matricule
        {
            get => _matricule;
            set
            {
                if (IsMatriculeOk(value)) this._matricule = value;
            }
        }
        public string Nom
        {
            get => _nom; set
            {
                if (IsNomPrenomOk(value)) this._nom = value;
            }
        }
        public string Prenom
        {
            get => _prenom; set
            {
                if (IsNomPrenomOk(value)) this._prenom = value;
            }
        }
        public decimal SalaireBrut { get => _salaireBrut; set => _salaireBrut = value; }
        public decimal TauxCS
        {
            get => _tauxCS; set
            {
                if (IsTauxCSOk(value)) this._tauxCS = value;
            }
        }
        public DateTime DateNaissance
        {
            get => _dateNaissance; set
            {
                if (IsDateNaissanceOk(value)) this._dateNaissance = value;
            }
        }
        public decimal SalaireNet { get => _salaireBrut * (1 - _tauxCS); }

        // mise en places des verifications d'intégrité
        private bool IsNomPrenomOk(String value)
        {
            if (value.Length < 3 && value.Length > 30) return false;
            for (int i =0; i < value.Length; i++)
            {
                if (!char.IsLetter(value[i])) return false;
            }
            return true;
        }  
        private bool IsMatriculeOk(String value)
        {
            if (String.IsNullOrEmpty(value) || value.Length != 7) return false;
            for (int i=0; i < value.Length; i++)
            {
                if(!char.IsDigit(value[i]) && (i <2 || i > 4)) return false;
                if(!char.IsLetter(value[i]) && (i > 1 && i < 5)) return false;
            }
            return true;
        }
        private bool IsTauxCSOk(decimal value)
        {
            if (value >= 0 && value <= 0.6m) return true;
            return false;
        }
        private bool IsDateNaissanceOk(DateTime value)
        {
            if ((value.CompareTo(new DateTime(1900, 01, 01)) < 0) || value.CompareTo(DateTime.Now.AddYears(-15)) > 0) return false;
            return true;
        }
        // creation constructeur
        public virtual decimal GetSalaireNet()
        {
            decimal value = SalaireBrut * (1 - TauxCS);
            return value;
        }
        public Salarie(string nom, string prenom, string matricule)
        {
            Nom = nom;
            Prenom = prenom;
            Matricule = matricule;

        }
        public Salarie() { }
        #region méthodes surchargées ou substituées de la classe object
        public override bool Equals(object obj)
        {
            Salarie tocompare = obj as Salarie;
            return (this.Matricule == tocompare.Matricule);
        }
        public override string ToString()
        {
            return this.Matricule + ";" + this.Nom + ";" + this.Prenom + ";" + this.SalaireBrut + ";" +this.TauxCS +";"+ this.SalaireNet + ";" + this.DateNaissance + ";";
        }
        //override du hashCode? a quoi ca sert ?
        public override int GetHashCode()
        {
            return 1593405470 + EqualityComparer<string>.Default.GetHashCode(_matricule);
        }
        #endregion
    }
    public class Commercial : Salarie
    {
        private decimal _chiffreAffaire;
        private decimal _commission;

        public decimal ChiffreAffaire { get => _chiffreAffaire; set => _chiffreAffaire = value; }
        public decimal Commission { get => _commission; set => _commission = value; }
        
        //constructeur par défaut 
        public Commercial() { }
        //constructeur de recopie heritant du constructeur de base
        public Commercial(string nom,string prenom, string matricule) : base(nom, prenom, matricule)
        {
            Nom = nom;
            Prenom = prenom;
            Matricule = matricule;
        }
        public override decimal GetSalaireNet()
        {
            return base.GetSalaireNet() + (Commission * ChiffreAffaire);
        }
    }

}
