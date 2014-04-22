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
    public abstract class ObjetoColetavel 
    {
       private Texture2D imagem;
       private Vector2 posicao;
       
       public Vector2 Posicao
        {
            get { return posicao; }
            set
            { 
                posicao = value;
                rect.X = (int)posicao.X;
                rect.Y = (int)posicao.Y;
            }
        }

        public Rectangle rect;
        private float angulo;
        private bool Lado;
        private Vector2 velocidade;

        public ObjetoColetavel(Texture2D tex, Vector2 pos, Vector2 velocidade)
        {
            this.imagem = tex;
            this.posicao = pos;
            this.rect = new Rectangle((int)pos.X, (int)pos.Y, imagem.Width, imagem.Height);
            this.velocidade = velocidade;
            //Random aletorio = new Random();
            //angulo = (float)aletorio.NextDouble();
            //int lado = aletorio.Next(0, 2);
            //if (lado == 0)
            //{
            //    Lado = true;
            //}
            //else
            //{
            //    Lado = false;
            //}
        }

        public virtual void Coletado(ref Personagem.Personagem personagem)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            Posicao += velocidade;
           // posicao += Personagem.velocidade;
            this.rect = new Rectangle((int)Posicao.X, (int)Posicao.Y, imagem.Width, imagem.Height);
            //if (Lado)
            //{
            //    Posicao.X -= (float)Math.Cos(angulo);
            //    Posicao.Y -= (float)Math.Sin(angulo);
            //}
            //else
            //{
            //    Posicao.X += (float)Math.Cos(angulo);
            //    Posicao.Y += (float)Math.Sin(angulo);
            //}

        }

        public virtual void UpdatePersonagem(Vector2 velPersonagem)
        {
            posicao += velPersonagem;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.imagem, Posicao, Color.White);
        }
    }
}