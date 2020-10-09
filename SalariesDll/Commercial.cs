using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SalariesDll
{
    
    [Serializable()]
    public class Commercial : Salarie
    {
        private decimal _chiffreAffaire;
        private decimal _commission;

        public decimal ChiffreAffaire { get => _chiffreAffaire; set => _chiffreAffaire = value; }
        public decimal Commission { get => _commission; set => _commission = value; }

        //constructeur par défaut 
        public Commercial()
            : base()
        {

        }
        //constructeur de recopie heritant du constructeur de base
        public Commercial(string nom, string prenom, string matricule) : base(nom, prenom, matricule)
        {
            Nom = nom;
            Prenom = prenom;
            Matricule = matricule;
        }
        public override decimal GetSalaireNet()
        {
            return base.GetSalaireNet() + (Commission * ChiffreAffaire);
        }
        public override string ToString()
        {
            return (base.ToString() + string.Format(CultureInfo.CurrentCulture, @";{0};{1}", this._chiffreAffaire, this._commission));
        }
    }
}

