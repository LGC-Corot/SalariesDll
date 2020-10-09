using System;
using System.Collections.Generic;
using System.Text;

namespace SalariesDll
{
    public partial class Salarie
    {
        public event EventHandler<ChangementSalaireEventArgs> ChangementSalaire;

        protected virtual void OnChangementSalaire(ChangementSalaireEventArgs e)
        {
            ChangementSalaire?.Invoke(this, e);
        }
        
    }
}
