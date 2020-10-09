using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SalariesDll
{
    [Serializable()]
    [XmlInclude(typeof(Commercial))]
    public class Salaries : List<Salarie>
    {/// <summary>
    /// Ajouter un salarié ( mine)
    /// </summary>
    /// <param name="sal"></param>
        public new void Add(Salarie sal)
        {
            {
                try 
                { 
                        if (!this.Contains(sal))
                        { base.Add(sal); }
                        else
                        throw new ApplicationException("Le Salarié est déjà present dans la liste des salariés");
                }
                catch(ApplicationException ex)
                {
                    Console.WriteLine(ex);
                }
            }   

        } 
        /// <summary>
        /// Extrait le salarié ayant le numéro de matricule fourni en argument
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public Salarie GetSalarie(string matricule)
        {
            foreach (Salarie elem in this)
            {
                if (matricule == elem.Matricule) return elem;
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sal"></param>
        public new void Remove(Salarie sal)
        {
            foreach (Salarie elem in this)
            {
                if (sal.Equals(elem)) base.Remove(elem);
                return;
            }
        }
        /// <summary>
        /// extrait le salarié ayant le numéro de matricule fourni en argument?
        /// </summary>
        /// <param name="matricule"></param>
        public void Remove(string matricule)
        {
            foreach (Salarie elem in this)
            {
                if (matricule == elem.Matricule) base.Remove(elem);
                return;
            }
        }

        // rajouté a partir du code correction mais probleme avec using ? version 8.0?
        /*public bool SaveXml(string pathRepDocument)
        {
            try
            {
                FileStream fs = new FileStream(string.Format(CultureInfo.InvariantCulture, @"{0}\Salaries.xml", pathRepDocument), FileMode.Create, FileAccess.Write, FileShare.Read);
                XmlSerializer xmls = new XmlSerializer(this.GetType());
                xmls.Serialize(fs, this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool LoadXml(string pathRepDocument)
        {
            try
            {
                FileStream fileStream = new FileStream(string.Format(CultureInfo.InvariantCulture, @"{0}\Salaries.xml", pathRepDocument), FileMode.Open, FileAccess.Read, FileShare.Read);
                XmlSerializer xmls = new XmlSerializer(this.GetType());
                this.AddRange(xmls.Deserialize(fileStream) as Salaries);
                return true; 
                
            }
            
            catch (Exception)
            {
                return false;
            }
        }*/
        public bool SaveTxt(string pathRepDocument)
        {
            try
            {
                using (FileStream fs = new FileStream($@"{pathRepDocument}\Salaries.txt", FileMode.Create, FileAccess.Write, FileShare.Read))
                using (StreamWriter txtwritter = new StreamWriter(fs))
                {

                    foreach (Salarie item in this)
                    {
                        txtwritter.WriteLine(item.ToString());
                    }
                    //txtwritter.Close();
                    //fs.Close();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool LoadText(string pathRepDocument)
        {
            try
            {
                using (FileStream fs = new FileStream($@"{pathRepDocument}\Salaries.txt",
                    FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader sr = new StreamReader(fs))

                    while (sr.Peek() > -1)
                    {
                        string[] infosSalarie = sr.ReadLine().Split(new char[] { ';' }, StringSplitOptions.None);
                        if (infosSalarie.Length == 6)
                        {
                            Salarie salarie = new Salarie()
                            {
                                Matricule = infosSalarie[0],
                                Nom = infosSalarie[1],
                                Prenom = infosSalarie[2],
                                TauxCS = decimal.Parse(infosSalarie[4]),
                            };

                            if (infosSalarie[3].Trim() != string.Empty)
                            { salarie.SalaireBrut = decimal.Parse(infosSalarie[3]); }
                            if (infosSalarie[5].Trim() != string.Empty)
                            {
                                salarie.DateNaissance = DateTime.Parse(infosSalarie[5]);
                            }
                            this.Add(salarie);
                        }
                        if (infosSalarie.Length == 8)
                        {
                            Commercial commercial = new Commercial()
                            {
                                Matricule = infosSalarie[0],
                                Nom = infosSalarie[1],
                                Prenom = infosSalarie[2],
                                TauxCS = decimal.Parse(infosSalarie[4]),

                            };
                            if (infosSalarie[3].Trim() != string.Empty)
                            { commercial.SalaireBrut = decimal.Parse(infosSalarie[3]); }

                            if (infosSalarie[5].Trim() != string.Empty)
                            {
                                commercial.DateNaissance = DateTime.Parse(infosSalarie[5]);
                            }
                            if (infosSalarie[6].Trim() != string.Empty)
                            {
                                commercial.ChiffreAffaire = decimal.Parse(infosSalarie[6]);
                            }
                            if (infosSalarie[7].Trim() != string.Empty)
                            {
                                commercial.Commission = decimal.Parse(infosSalarie[7]);
                            }
                            this.Add(commercial);
                        }

                    }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // good or not? i cant tell
        public bool SaveXml(string pathRepDocument)
        {
            try
            {
                FileStream filestream = new FileStream(string.Format(CultureInfo.InvariantCulture, @"{0}\Salaries.xml", pathRepDocument), FileMode.Create, FileAccess.Write, FileShare.Read);
                XmlTextWriter xmlTW = new XmlTextWriter(filestream, Encoding.UTF8);
                XmlSerializer xmlS = new XmlSerializer(this.GetType());
                xmlS.Serialize(xmlTW, this);
                filestream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool LoadXml(string pathRepDocument)
        {
            FileStream fileStream;
            XmlTextReader xmlTR;
            XmlSerializer xmlS;
            try
            {
                fileStream = new FileStream(string.Format(CultureInfo.InvariantCulture, @"{0}\Salaries.xml", pathRepDocument), FileMode.Open, FileAccess.Read, FileShare.Read);
                xmlTR = new XmlTextReader(fileStream);
                xmlS = new XmlSerializer(this.GetType());
                base.AddRange(xmlS.Deserialize(xmlTR) as Salaries);
                fileStream.Close();              
                return true;
            }
            catch (Exception)
            {
                return false;
            }   
        }
        public bool Savebinarie(string pathRepDocument)
        {
            try
            {
                using (FileStream files = new FileStream($@"{pathRepDocument}\Salaries.dat", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(files, this);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool LoadBinarie(string pathRepDocument)
        {
            try
            {
                using (FileStream files = new FileStream($@"{pathRepDocument}\Salaries.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    this.AddRange((bf.Deserialize(files)as Salaries));
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SaveJson(string pathRepDocument)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            string fluxjson = JsonConvert.SerializeObject(this, settings);
            using FileStream file = new FileStream($@"{pathRepDocument}\Salaries.json", FileMode.Create, FileAccess.Write, FileShare.Read);
            using StreamWriter txtwriter = new StreamWriter(file) ;
            txtwriter.Write(fluxjson);
            return true;           
        }
        public bool LoadJson(string pathRepDocument)
        {
            using FileStream fs = new FileStream($@"{pathRepDocument}\Salaries.json",
                    FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader sr = new StreamReader(fs);
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            this.AddRange(JsonConvert.DeserializeObject<Salaries>(sr.ReadToEnd(), settings));
            return true;
        }
    }
}   
   

