using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SubBeatle.Code.Input;

namespace SubBeatle.Code.Hud
{
    public  class EscudoPlastico
    {
        botao BtnEscudo;
        botao Completo;
        botao Vazio;
        botao Metade;

        public EscudoPlastico(ContentManager content,Vector2 Posicao)
        {
            this.Completo = new botao(content.Load<Texture2D>("Source//Hud//defesa_cheia"), Posicao);
            this.Vazio = new botao(content.Load<Texture2D>("Source//Hud//defesa_vazia"), Posicao);
            this.Metade = new botao(content.Load<Texture2D>("Source//Hud//defesa_meio"), Posicao);
            this.BtnEscudo = Vazio;
        }

        public void checarCondição(int QuantidadePlástico)
        {
            if (QuantidadePlástico > 0)
            {
                this.BtnEscudo = Completo;
            }
            else
            {
                this.BtnEscudo = Vazio;
            }
        }

        public void DesenharBtn(SpriteBatch desenhador)
        {
            BtnEscudo.DesenharBotao(desenhador);
        }
    }
}