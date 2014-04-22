using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SubBeatle.Code.Sistema
{
   static class Constantes
    {
       public const float MetalMinimoNivel2 = 100f;
       public const float MetalMinimoNivel3 = 200f;

       public const float tempoQuedaMetal = 1000;
       public const int taxaPerdaMetalLevel1 = 5;
       public const int taxaPerdaMetalLevel2 = 10;
       public const int taxaPerdaMetalLevel3 = 20;

       public const float tempoQuedaPlastico = 1000;
       public const int taxaPerdaPlastico = 1;
       public const int taxaPerdaOrganico = 350;
       public static int Pontuacao;
       public static Rectangle retLimitador = new Rectangle(
            200,
            100,
            400,
            230
           );
    }
}