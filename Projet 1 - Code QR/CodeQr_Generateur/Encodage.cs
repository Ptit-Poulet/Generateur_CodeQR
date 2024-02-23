using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public static class Encodage
    {

        /// <summary>
        /// Encoder la chaine de debut en mode numerique
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        public static string CodageNumerique(string chaineDebut)
        {
            string chaineEncodee = "";
            List<string> lesGroupesDeTrois = new List<string>();

            for (int i = 0; i < chaineDebut.Length; i += 3)
            {
                if (i < chaineDebut.Length - 2)
                    lesGroupesDeTrois.Add(chaineDebut.Substring(i, 3));
                else if (i == chaineDebut.Length - 2)
                    lesGroupesDeTrois.Add(chaineDebut.Substring(i, 2));
                else
                    lesGroupesDeTrois.Add(chaineDebut.Substring(i, 1));
            }

            //Convertir chaque groupe en binaire
            foreach (string groupe in lesGroupesDeTrois)
            {
                if (groupe.Length == 3)
                {
                    if (groupe[0] == '0')
                    {
                        if (groupe[1] == '0')
                        {
                            //il doit etre interprété comme un nombre à 1 chiffre
                            chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
                        }
                        else    //il doit etre interprêté comme un nombre à deux chiffres
                            chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(7, '0');
                    }
                    else    //Si effectivement il s'agit d'un avec le premier chifrre différent de zero
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(10, '0');

                }
                else if (groupe.Length == 2)
                {
                    if (groupe[0] == '0')
                    {
                        //Dans ce cas il doit etre interprêté comme un nombre à 1 chiffre
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
                    }
                    else    //il doit effectivement etre interprêté comme un nombre à deux chiffres
                        chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(7, '0');
                }
                else    //si le groupe final ne comprend qu'un seul chiffre
                    chaineEncodee += Convert.ToString(int.Parse(groupe), 2).PadLeft(4, '0');
            }


            return chaineEncodee;
        }
        /// <summary>
        /// Encoder la chaine de debut en mode alphanum
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        public static string CodageAlphaNum(string chaineDebut)
        {
            string CaractereEncode = "";
            string binaire = "";
            string indicateurCaractere = "";
            int valeurNumerique = 0;

            List<string> lettre = new List<string> {"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E",
                            "F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"," ","$","%","*","+","-",".","/",":"};

            List<string> chiffre = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
            "20","21","22","23","24","25","26","27","28","29","30","31","32","33","34","35","36","37","38","39","40","41","42","43","44"};

            List<List<string>> alphaNumValue = new List<List<string>>();
            List<string> caractereEnBinaire = new List<string>();

            //Créer une liste avec les valeurs alphanumerique
            for (int i = 0; i < 44; i++)
            {
                List<string> value = new List<string>();
                value.Add(lettre[i]);
                value.Add(chiffre[i]);
                alphaNumValue.Add(value);
            }

            // Conversion en binaire
            foreach (char c in chaineDebut)
            {
                // Recherche de la correspondance dans la liste alphaNumValue
                var matchingItem = alphaNumValue.FirstOrDefault(item => item.Contains(c.ToString()));

                // Si une correspondance est trouvée, ajoutez la représentation binaire
                if (matchingItem != null)
                {
                    caractereEnBinaire.Add(matchingItem[1]);
                }

            }


            //Formule pour mettre en binaire
            for (int i = 0; i < caractereEnBinaire.Count; i += 2)
            {
                //Si le caractère est seul
                if (i == caractereEnBinaire.Count - 1)
                {
                    valeurNumerique = int.Parse(caractereEnBinaire[i]);
                }
                else
                {
                    valeurNumerique = int.Parse(caractereEnBinaire[i]) * 45 + int.Parse(caractereEnBinaire[i + 1]);
                }

                //Regarder si fini sur un caractère seul
                int longueurPadding = (i == caractereEnBinaire.Count - 1) ? 6 : 11;

                binaire = Convert.ToString(valeurNumerique, 2).PadLeft(longueurPadding, '0');

                CaractereEncode = CaractereEncode + " " + binaire;

                //Break la boucle si c'Est le dernier caractère
                if (i == caractereEnBinaire.Count - 1)
                {
                    break;
                }
            }
            return CaractereEncode;
        }
        /// <summary>
        /// Encoder la chaine de debut en mode Byte
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        public static string CodageByte(string chaineDebut)
        {
            //TODO
            string chaineEncodee = "";
            List<string> caractereEnHexa = new List<string>();

            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(chaineDebut);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            string msg = iso.GetString(isoBytes);

            foreach (char c in chaineDebut)
            {
                chaineEncodee += Convert.ToString(c, 2).PadLeft(8, '0');
            }


            return chaineEncodee;
            //throw new Exception();
        }
        /// <summary>
        /// Encoder la chaine de debut en mode kanji
        /// </summary>
        /// <param name="chaineDebut"></param>
        /// <returns></returns>
        public static string CodageKanji(string chaineDebut)
        {
            //TODO

            throw new Exception();
        }
    }
}
