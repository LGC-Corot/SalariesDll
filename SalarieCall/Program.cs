using System;
using SalariesDll;

namespace SalarieCall
{
    class SalarieTest
    {
        static void Main(string[] args)
        {
            Salarie first = new Salarie("jacques", "jean", "12gjd56");
            Console.WriteLine(first.Matricule);
            Console.WriteLine(first.Nom);
            Console.WriteLine(first.Prenom);
            first.SalaireBrut = 5000;
            first.TauxCS = 0.4m;
            Console.WriteLine(first.SalaireNet);
            first.Matricule = "125mk12";
            Console.WriteLine(first.Matricule);
            first.DateNaissance = new DateTime(1993,12,17);
            Console.WriteLine(first.ToString());
            Commercial com1 = new Commercial("richard", "charles", "18afj69");
            com1.DateNaissance = new DateTime(1990, 05, 26);
            com1.ChiffreAffaire = 10000;
            com1.Commission = 0.2m;
            com1.SalaireBrut = 4000;
            com1.TauxCS = 0.5m;
            Console.WriteLine(com1.GetSalaireNet());
            Console.WriteLine(com1.ToString());
        }
    }
}
