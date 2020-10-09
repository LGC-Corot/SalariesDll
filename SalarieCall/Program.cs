using System;
using SalariesDll;
using System.IO;
using System.Runtime.ExceptionServices;

namespace SalarieCall
{
    class SalarieTest
    {
        static Salaries salaries = new Salaries();
        static void Main(string[] args)
        {
            GenererCollection();

            // chemin du dossier pour les save et load XML,JSON,TXT,BIN

            string dossier = @"C:\Users\CDA\source\repos";
            TestSaveLoadXml(dossier);
            //Savetxt2(dossier);
            //Savebin2(dossier);
            //SaveJson2(dossier);

            // test pour la bonne fonctionnalité de l'evenement
            //new DemoSalaire().Demo();

            

            /*Salarie first = new Salarie("jacques", "jean", "12gkh56");
            Salarie salarie = new Salarie("jackot", "richard", "12gkh56");
            
            Console.WriteLine(salarie.Nom);
            Console.WriteLine(salarie.Prenom);
            Console.WriteLine(salarie.Matricule);
            Console.WriteLine(first.Nom);
            Console.WriteLine(first.Prenom);
            Console.WriteLine(first.Matricule);
            first.SalaireBrut = 5000;
            first.TauxCS = 0.4m;
            Console.WriteLine(first.SalaireNet);
            first.Matricule = "12hmk12";
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
            Console.WriteLine(com1.ToString());*/

            

            //salaries.Add(new Salarie() { Matricule = "01ABC12", Nom = "Artemiche", Prenom = "Marcel", SalaireBrut = 1200.35M, TauxCS = 0.20M, DateNaissance = new DateTime(1980, 10, 25) });
            //salaries.Add(new Salarie() { Matricule = "01ABC12", Nom = "Bernard", Prenom = "Antoine", TauxCS = 0.20M, DateNaissance = new DateTime(1981, 09, 25) });
            //salaries.Add(new Salarie() { Matricule = "02jkl12", Nom = "Bernard", Prenom = "Antoine", TauxCS = 0.20M, DateNaissance = new DateTime(1981, 09, 25) });

            

        }

        //la demo pour le bon fonctionnement de l'evenement
        public class DemoSalaire
        {
            public void Demo()
            {
                Salarie salaire = new Salarie { SalaireBrut = 10000 };
                salaire.ChangementSalaire += salaire_ChangementDeSalaire;
                salaire.SalaireBrut = 12000;
                salaire.SalaireBrut = 15000;
                Salarie salaire2 = new Salarie { SalaireBrut = 20000 };
                salaire2.ChangementSalaire += salaire_ChangementDeSalaire;
                salaire2.SalaireBrut = 25000;
            }
            private void salaire_ChangementDeSalaire(object sender, ChangementSalaireEventArgs e)
            {
                Console.WriteLine("L' ancien salaire est de : " +e.AncienSalaire);
                Console.WriteLine("Le nouveau salaire est de :" +e.NouveauSalaire);
                Console.WriteLine("Le taux d'augmentation est de : " + e.TauxAugmentation+"euros");
            }
        }

        
        


        private static void TestSaveloadXml(string dossier)
        {
                Console.WriteLine(salaries.SaveXml(dossier));
                salaries.Clear();
                Console.WriteLine(salaries.LoadXml(dossier));
                Console.ReadLine();
        }

        private static void Savetxt2(string dossier)
        {
            Console.WriteLine(salaries.SaveTxt(dossier));
            salaries.Clear();
            Console.WriteLine(salaries.LoadText(dossier));
            
        }

        private static void Savebin2(string dossier)
        {
            Console.WriteLine(salaries.Savebinarie(dossier));
            salaries.Clear();
            Console.WriteLine(salaries.LoadBinarie(dossier));
        }
        private static void SaveJson2(string dossier)
        {
            Console.WriteLine(salaries.SaveJson(dossier));
            salaries.Clear();
            Console.WriteLine(salaries.LoadJson(dossier));
        }
        private static void GenererCollection()
        {
            salaries.Add(new Salarie() { Matricule = "01ABC12", Nom = "Artemiche", Prenom = "Marcel", SalaireBrut = 1200.35M, TauxCS = 0.20M, DateNaissance = new DateTime(1980, 10, 25) });
            salaries.Add(new Salarie() { Matricule = "02ABC12", Nom = "Bernard", Prenom = "Antoine", TauxCS = 0.20M, DateNaissance = new DateTime(1981, 09, 25) });          
            salaries.Add(new Commercial() { Matricule = "03ABC12", Nom = "Commerce", Prenom = "Philippe", Commission = 0.2M, ChiffreAffaire = 2500, TauxCS = 0.20M, DateNaissance = new DateTime(1979, 09, 25) });
            salaries.Add(new Commercial() { Matricule = "04ABC12", Nom = "CommerceVIP", Prenom = "Jean", Commission = 0.2M, TauxCS = 0.20M, DateNaissance = new DateTime(1990, 09, 25) });
        }


    }


}
