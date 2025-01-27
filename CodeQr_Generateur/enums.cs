using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeQr_Generateur
{
    /// <summary>
    /// Niveaux de code d'erreur
    /// </summary>
    public enum ECLevel
    {
        L,
        M,
        Q,
        H
    }

    /// <summary>
    /// Modes d'encodage
    /// </summary>
    public enum ChEncoding
    {
        Num,
        AlphaNum,
        Byte,
        Kanji
    }

}
