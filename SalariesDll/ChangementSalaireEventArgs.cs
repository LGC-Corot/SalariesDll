using System;
using System.Collections.Generic;
using System.Text;

namespace SalariesDll
{
    public class ChangementSalaireEventArgs : EventArgs
    {
        public decimal AncienSalaire { get; set; }
        public decimal NouveauSalaire { get; set; }
       
        public decimal TauxAugmentation { get; set; }

        public ChangementSalaireEventArgs(decimal ancienSalaire, decimal nouveauSalaire)
        {
            NouveauSalaire = nouveauSalaire;
            AncienSalaire = ancienSalaire;
            TauxAugmentation = nouveauSalaire - ancienSalaire;

        }
    }   
}
