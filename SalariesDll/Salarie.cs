using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
//using Newtonsoft.Json;

namespace SalariesDll
{
    [Serializable()]

    public partial class Salarie
    {

        private string _matricule;
        private string _nom;
        private string _prenom;
        private decimal _salaireBrut;
        private decimal _tauxCS;
        private DateTime _dateNaissance;
        //private decimal _salaireNet;

        public string Matricule
        {
            get { return (this._matricule); }
            set
            {
                if (!IsMatriculeOk(value)) throw new ApplicationException(string.Format(CultureInfo.CurrentCulture, "Le matricule {0} n'est pas valide.", value));
                this._matricule = value;
            }
        }
        public string Nom
        {
            get => _nom; set
            {
                if (!IsNomPrenomOk(value)) throw new ApplicationException(string.Format(CultureInfo.CurrentCulture, "Le nom {0} n'est pas au bon format", value)); 
                this._nom = value;
            }
        }
        public string Prenom
        {
            get => _prenom; set
            {
                if (!IsNomPrenomOk(value)) throw new ApplicationException(string.Format(CultureInfo.CurrentCulture, "Le prénom {0} n'est pas au bon format", value));
                this._prenom = value;
            }
        }
        //public decimal SalaireBrut { get => _salaireBrut; set => _salaireBrut = value; }

        public decimal SalaireBrut
        {
            get { return (this._salaireBrut); }
            set
            {
                if (_salaireBrut != 0 && _salaireBrut != value)
                {
                    OnChangementSalaire(new ChangementSalaireEventArgs(_salaireBrut, value));
                }
                _salaireBrut = value;
            }

        }

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
        public static bool IsNomPrenomOk(String value)
        {
            if (value==null || value.Length < 3 && value.Length > 30) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsLetter(value[i])) return false;
            }
            return true;
        }
        public static bool IsMatriculeOk(String value)
        {
            if (String.IsNullOrEmpty(value) || value.Length != 7) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]) && (i < 2 || i > 4)) return false;
                if (!char.IsLetter(value[i]) && (i > 1 && i < 5)) return false;
            }
            return true;
        }
        public static bool IsTauxCSOk(decimal value)
        {
            if (value >= 0 && value <= 0.6m) return true;
            return false;
        }
        public static bool IsDateNaissanceOk(DateTime value)
        {
            if ((value.CompareTo(new DateTime(1900, 01, 01)) < 0) || value.CompareTo(DateTime.Now.AddYears(-15)) > 0) return false;
            return true;
        }

        public static bool IsSalarieValide(Salarie salarie)
        {
                
                if (!IsNomPrenomOk(salarie._nom)) return false;
                if (!IsNomPrenomOk(salarie._prenom)) return false;
                if (!IsMatriculeOk(salarie._matricule)) return false;
                if (!IsDateNaissanceOk(salarie._dateNaissance)) return false;
                if (!IsTauxCSOk(salarie._tauxCS)) return false;

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
            return this.Matricule + ";" + this.Nom + ";" + this.Prenom + ";" + this.SalaireBrut + ";" + this.TauxCS + ";" + this.SalaireNet + ";" + this.DateNaissance + ";";
        }
        //override du hashCode? what is the point ?
        public override int GetHashCode()
        {
            return (_matricule != null) ? _matricule.GetHashCode() : 0;
        }
 
        #endregion
    }   
}
