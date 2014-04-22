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

using SubBeatle.Code.Input;

namespace SubBeatle.Code.Hud
{
    class HudNiveis
    {
        botao Nivel1;
        botao Nivel2;
        botao Nivel3;
        Texture2D Ligado;
        Texture2D Desligado;

        public HudNiveis(ContentManager content)
        {
            this.Ligado = content.Load<Texture2D>("Source//Hud//lampada_cheia");
            this.Desligado = content.Load<Texture2D>("Source//Hud//lampada_vazia");
            this.Nivel1 = new botao(content.Load<Texture2D>("Source//Hud//lampada_vazia"), new Vector2(321 , 403));
            this.Nivel2 = new botao(content.Load<Texture2D>("Source//Hud//lampada_vazia"), new Vector2(357 , 393));
            this.Nivel3 = new botao(content.Load<Texture2D>("Source//Hud//lampada_vazia"), new Vector2(390 , 400));
        }

        public void CheckNivel(Personagem.Personagem.LevelPersonagem nivel)
        {
            if (nivel == Personagem.Personagem.LevelPersonagem.level1)
            {
                this.Nivel1.colocar(Ligado);
                this.Nivel2.Retirar();
                this.Nivel3.Retirar();
            }

            if (nivel == Personagem.Personagem.LevelPersonagem.level2)
            {
                this.Nivel2.colocar(Ligado);
                this.Nivel3.Retirar();
            }

            if (nivel == Personagem.Personagem.LevelPersonagem.level3)
            {
                this.Nivel3.colocar(Ligado);
            }
        }

        public void DesenharNivel(SpriteBatch desenhador)
        {
            this.Nivel1.DesenharBotao(desenhador);
            this.Nivel2.DesenharBotao(desenhador);
            this.Nivel3.DesenharBotao(desenhador);
        }
    }   
}