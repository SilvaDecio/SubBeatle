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

namespace SubBeatle.Code.Model
{
    public class ParallaxLayer
    {
        private Texture2D imagem;
        private Vector2 posicao;
        private Vector2 velocidade;
        private EstadoParalax EstadoParalax;
        public Vector2 Posicao 
        {
            get { return this.posicao; }
            set { this.posicao = value; }
        }

        public ParallaxLayer(float velX, float velY,EstadoParalax estado)
        {
            this.posicao = new Vector2(0, 0);
            this.velocidade = new Vector2(velX, velY);
            this.EstadoParalax = estado;
        }

        public void LoadContent(ContentManager content, string filename)
        {
            this.imagem = content.Load<Texture2D>(filename);
        }

        public void UpdatePosition(Vector2 TargetPosition)
        {

        }
      
        public void Update(GameTime gameTime)
        {
            KeyboardState teclado = Keyboard.GetState();

            if (EstadoParalax == EstadoParalax.Horizontal)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.posicao.X -= Personagem.Personagem.velocidade.X;
            }
            else
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.posicao.Y -= Personagem.Personagem.velocidade.Y;
            }
            
            this.posicao.X = this.posicao.X % this.imagem.Width;
            this.posicao.Y = this.posicao.Y % this.imagem.Height;
        }

        public void Draw(SpriteBatch batch)
        {
            if (EstadoParalax == EstadoParalax.Horizontal)
            {
                batch.Draw(this.imagem, this.posicao, Color.White);
                batch.Draw(this.imagem, new Vector2(this.posicao.X + this.imagem.Width, posicao.Y), Color.White);
            }
            else
            {
                batch.Draw(this.imagem, this.posicao, Color.White);
                batch.Draw(this.imagem, new Vector2(posicao.X,this.posicao.Y + this.imagem.Height), Color.White);
            }
            //batch.Draw(this.imagem, new Vector2(this.posicao.X + this.imagem.Width,0 ), Color.White);
        }
    }
}