using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SubBeatle.Code.Objetos
{
    public class ObjetoVidro : ObjetoColetavel
    {
        public ObjetoVidro(Texture2D tex, Vector2 pos, Vector2 vel)
            : base(tex, pos, vel)
        {

        }

        public override void Coletado(ref Personagem.Personagem personagem)
        {
            personagem.QuantidadeVidro += 1;
        }
    }
}