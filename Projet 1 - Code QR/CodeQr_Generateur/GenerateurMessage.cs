
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    public class GenerateurMessage
    {

        private string[] _chaineOctets;
        private ECLevel _niveauCorrection;
        private int _version;
        Dictionary<int, int> dictio;


        public GenerateurMessage(string[] chaineOctetcs, ECLevel niveauCorrection, int version)
        {
            _chaineOctets = chaineOctetcs;
            _niveauCorrection = niveauCorrection;
            _version = version;
            dictio = new Dictionary<int, int>
                {
                    {1, 0}, {2, 7}, {3, 7}, {4, 7}, {5, 7}, {6, 7},
                    {7, 0}, {8, 0}, {9, 0},{10, 0}, {11, 0}, {12, 0}, {13, 0},
                    {14, 3}, {15, 3}, {16, 3}, {17, 3}, {18, 3}, {19, 3}, {20, 3},
                    {21, 4}, {22, 4}, {23, 4}, {24, 4} , {25, 4} , {26, 4}, {27, 4},
                    {28, 3}, {29, 3}, {30, 3}, {31, 3}, {32, 3}, {33, 3}, {34, 3},
                    {35, 0}, {36, 0}, {37, 0}, {38, 0}, {39, 0}, {40, 0}

                };
        }

        private GroupBlockCodewordHelper GetGroupBlockCodewordHelper()
        {
            return GroupBlockCodewordSplit.getVersionGroupBlockCodewordInfo(_niveauCorrection, _version);
        }

        private List<Groupe> FormerGroupes(string[] chaineOctects)
        {
            GroupBlockCodewordHelper infoGroupesBlocs = GetGroupBlockCodewordHelper();

            List<Groupe> listGroupes = new List<Groupe>();

            //If 2 Groupes
            //Creer 2 Groupes
            //else
            //Creer 1 seul groupe

            //TODO: Finaliser la division de chaineOctets pour fournir l'information aux constructeurs de groupe1 et groupe2, selon le cas
            int nbOctectG1 = infoGroupesBlocs.NbBlocksInGroup1 * infoGroupesBlocs.NbCodeWordsInGroup1Blocks;
            string[] chaineOctetsG1 = new ArraySegment<string>(chaineOctects, 0, nbOctectG1).ToArray();

            Groupe groupe1 = new Groupe(chaineOctetsG1, infoGroupesBlocs.NbCodeWordsInGroup1Blocks,
                                  infoGroupesBlocs.NbBlocksInGroup1, infoGroupesBlocs.HowManyCorrectionCodewords);

            listGroupes.Add(groupe1);

            if (infoGroupesBlocs.NbBlocksInGroup2 != 0)
            {
                int nbOctectG2 = infoGroupesBlocs.NbBlocksInGroup2 * infoGroupesBlocs.NbCodeWordsInGroup2Blocks;
                string[] chaineOctetsG2 = new ArraySegment<string>(chaineOctects, nbOctectG1, nbOctectG2).ToArray();
                Groupe groupe2 = new Groupe(chaineOctetsG2, infoGroupesBlocs.NbCodeWordsInGroup2Blocks,
                                       infoGroupesBlocs.NbBlocksInGroup2, infoGroupesBlocs.HowManyCorrectionCodewords);

                listGroupes.Add(groupe2);
            }
            return listGroupes;
        }


        /// <summary>
        /// Entrelacement des données codeword avec 2groupes de 2 bloc spécifiquement
        /// </summary>
        /// <returns>Données entrelacé</returns>
        private int[] EntrelacerCW()
        {
            List<Groupe> listGroupes = FormerGroupes(_chaineOctets);
            GroupBlockCodewordHelper groupBlockCodewordHelper = GetGroupBlockCodewordHelper();

            //CWEntrelaces[0] = Convert.ToInt32(listGroupes[0].GetBlocs()[0].GetEncodedCodeWords()[0], 2);
            //CWEntrelaces[1] = Convert.ToInt32(listGroupes[0].GetBlocs()[1].GetEncodedCodeWords()[0], 2);
            //CWEntrelaces[2] = Convert.ToInt32(listGroupes[1].GetBlocs()[0].GetEncodedCodeWords()[0], 2);
            //CWEntrelaces[3] = Convert.ToInt32(listGroupes[1].GetBlocs()[1].GetEncodedCodeWords()[0], 2);

            //CWEntrelaces[3] = Convert.ToInt32(listGroupes[0].GetBlocs()[0].GetEncodedCodeWords()[1], 2);
            //CWEntrelaces[4] = Convert.ToInt32(listGroupes[0].GetBlocs()[1].GetEncodedCodeWords()[1], 2);
            //CWEntrelaces[5] = Convert.ToInt32(listGroupes[1].GetBlocs()[0].GetEncodedCodeWords()[1], 2);
            //CWEntrelaces[6] = Convert.ToInt32(listGroupes[1].GetBlocs()[1].GetEncodedCodeWords()[1], 2);

            //CWEntrelaces[7] = Convert.ToInt32(listGroupes[0].GetBlocs()[0].GetEncodedCodeWords()[2], 2);
            //CWEntrelaces[8] = Convert.ToInt32(listGroupes[0].GetBlocs()[1].GetEncodedCodeWords()[2], 2);
            //CWEntrelaces[9] = Convert.ToInt32(listGroupes[1].GetBlocs()[0].GetEncodedCodeWords()[2], 2);
            //CWEntrelaces[10] = Convert.ToInt32(listGroupes[1].GetBlocs()[1].GetEncodedCodeWords()[2], 2);

            int[] CWEntrelaces = null;
            int nbTotalBlocs = groupBlockCodewordHelper.NbBlocksInGroup1 + groupBlockCodewordHelper.NbBlocksInGroup2;

            if (listGroupes.Count > 1)   //S'il ya plus d'un groupe = 2 groupes
                CWEntrelaces = new int[groupBlockCodewordHelper.NbCodeWordsInGroup2Blocks * nbTotalBlocs];   //16*4
            else
                CWEntrelaces = new int[groupBlockCodewordHelper.NbCodeWordsInGroup1Blocks * nbTotalBlocs];   //13*1

            int i = 0;
            int noCW = 0;
            int noGroupe = 0;
            int noBloc = 0;

            while (i < CWEntrelaces.Length)
            {
                foreach (Groupe groupe in listGroupes)
                {
                    do
                    {
                        if (groupe == listGroupes[0])
                        {
                            //Si on a dépassé les limites des blocs du premier groupe
                            if (noCW >= groupBlockCodewordHelper.NbCodeWordsInGroup1Blocks)
                                CWEntrelaces[i] = 0;
                            else
                            {
                                Bloc blocCourrent = groupe.GetBlocs()[noBloc];
                                string CWCourant = blocCourrent.GetEncodedCodeWords()[noCW];
                                CWEntrelaces[i] = Convert.ToInt32(CWCourant, 2);
                            }
                        }
                        else
                        {
                            Bloc blocCourrent = groupe.GetBlocs()[noBloc];
                            string CWCourant = blocCourrent.GetEncodedCodeWords()[noCW];
                            CWEntrelaces[i] = Convert.ToInt32(CWCourant, 2);
                        }

                        i++;
                        noBloc++;

                    } while (noBloc < groupe.GetBlocs().Count);
                    noBloc = 0; //On remet noBloc à zéro pour pouvoir parcourir tous les blocs du 2nd groupe
                }
                noCW++; //Àprès avoir parcourue tous les blocs de tous les groupes
            }

            return CWEntrelaces;
        }


        private int[] EntrelacerECCW()
        {
            List<Groupe> listGroupes = FormerGroupes(_chaineOctets);
            GroupBlockCodewordHelper groupBlockCodewordHelper = GetGroupBlockCodewordHelper();

            //ECCWEntrelaces[0] = Convert.ToInt32(listGroupes[0].GetBlocs()[0].GetEncodedCodeWords()[15], 2);
            //ECCWEntrelaces[1] = Convert.ToInt32(listGroupes[0].GetBlocs()[1].GetEncodedCodeWords()[15], 2);
            //ECCWEntrelaces[2] = Convert.ToInt32(listGroupes[1].GetBlocs()[0].GetEncodedCodeWords()[15], 2);
            //ECCWEntrelaces[3] = Convert.ToInt32(listGroupes[1].GetBlocs()[1].GetEncodedCodeWords()[15], 2);

            //ECCWEntrelaces[3] = Convert.ToInt32(listGroupes[0].GetBlocs()[0].GetEncodedCodeWords()[16], 2);
            //ECCWEntrelaces[4] = Convert.ToInt32(listGroupes[0].GetBlocs()[1].GetEncodedCodeWords()[16], 2);
            //ECCWEntrelaces[5] = Convert.ToInt32(listGroupes[1].GetBlocs()[0].GetEncodedCodeWords()[16], 2);
            //ECCWEntrelaces[6] = Convert.ToInt32(listGroupes[1].GetBlocs()[1].GetEncodedCodeWords()[16], 2);

            //ECCWEntrelaces[7] = Convert.ToInt32(listGroupes[0].GetBlocs()[0].GetEncodedCodeWords()[17], 2);
            //ECCWEntrelaces[8] = Convert.ToInt32(listGroupes[0].GetBlocs()[1].GetEncodedCodeWords()[17], 2);
            //ECCWEntrelaces[9] = Convert.ToInt32(listGroupes[1].GetBlocs()[0].GetEncodedCodeWords()[17], 2);
            //ECCWEntrelaces[10] = Convert.ToInt32(listGroupes[1].GetBlocs()[1].GetEncodedCodeWords()[17], 2);

            int nbTotalBlocs = groupBlockCodewordHelper.NbBlocksInGroup1 + groupBlockCodewordHelper.NbBlocksInGroup2;
            int[] ECCWEntrelaces = new int[groupBlockCodewordHelper.HowManyCorrectionCodewords * nbTotalBlocs];   //18*4

            int i = 0;
            int noCW = groupBlockCodewordHelper.NbCodeWordsInGroup1Blocks;  //On commence directement au 1er ECCW
            int noGroupe = 0;
            int noBloc = 0;

            while (i < ECCWEntrelaces.Length)
            {
                foreach (Groupe groupe in listGroupes)
                {
                    do
                    {
                        if (groupe != listGroupes[0] && noBloc == 0)
                            noCW += 1;

                        Bloc blocCourrent = groupe.GetBlocs()[noBloc];
                        string CWCourant = blocCourrent.GetEncodedCodeWords()[noCW];
                        ECCWEntrelaces[i] = Convert.ToInt32(CWCourant, 2);

                        i++;
                        noBloc++;

                    } while (noBloc < groupe.GetBlocs().Count);
                    noBloc = 0; //On remet noBloc à zéro pour pouvoir parcourir tous les blocs du 2nd groupe
                    if (groupe != listGroupes[0])
                        noCW -= 1;  //Pour pouvoir remettre le cureseur au niveau des blocs du groupe 1
                }
                noCW++; //Àprès avoir parcourue tous les blocs de tous les groupes
            }

            return ECCWEntrelaces;
        }


        public string GenererMessageFinal()
        {
            string messageFinal = "";
            int[] CWEntrelaces = EntrelacerCW();
            int[] ECCWEntrelaces = EntrelacerECCW();

            int[] formeDecimale = new int[CWEntrelaces.Length + ECCWEntrelaces.Length];
            Array.Copy(CWEntrelaces, formeDecimale, CWEntrelaces.Length);
            Array.Copy(ECCWEntrelaces, 0, formeDecimale, CWEntrelaces.Length, ECCWEntrelaces.Length);

            foreach (int entier in formeDecimale)
            {
                if (entier == null)
                    continue;
                else
                {
                    string elementBinaire = Convert.ToString(entier, 2);
                    int remainder = elementBinaire.Length % 8;
                    if (elementBinaire.Length % 8 != 0)
                    {
                        messageFinal += new string('0', 8 - remainder);

                    }
                    messageFinal += elementBinaire;
                }
            }

            //On ajoute les bits restants nécessaires


            int noBitsRenainder = dictio[_version];    //S'il =0 la meme chaine est retournée
            messageFinal = messageFinal.PadRight(noBitsRenainder + messageFinal.Length, '0');

            return messageFinal;
        }

    }
}
