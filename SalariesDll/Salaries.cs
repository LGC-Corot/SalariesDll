using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SalariesDll
{
     public class Salaries : List< Salarie >
    {
        public new void Add(Salarie sal)
        {
            foreach (Salarie elem in this)
            {
                if (sal.Equals(elem)) return;
            }
            base.Add(sal);
        }
        public Salarie GetSalarie(string matricule)
        {
            foreach(Salarie elem in this)
            {
                if (matricule == elem.Matricule) return elem;
            }
            return null;
        }
        public new void Remove(Salarie sal)
        {
            foreach(Salarie elem in this)
            {
                if (sal.Equals(elem)) base.Remove(elem);
                return;
            }
        }
        public void Remove(string matricule)
        {
            foreach(Salarie elem in this)
            {
                if (matricule == elem.Matricule) base.Remove(elem);
                return;
            }
        }
    }
    public class SalariesHash : HashSet<Salarie>
    {

    }
}
