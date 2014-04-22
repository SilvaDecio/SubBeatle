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
    public abstract class ObjetosJogo
    {
        protected Texture2D imagem;
        protected Vector2 posicao;
        public Rectangle rect;

        public ObjetosJogo(Texture2D imagem, Vector2 posicao)
        {
            this.imagem = imagem;
            this.posicao = posicao;
            if(imagem != null)
            this.rect = new Rectangle((int)posicao.X, (int)posicao.Y, imagem.Width, imagem.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            if(imagem != null)
            this.rect = new Rectangle((int)posicao.X, (int)posicao.Y, imagem.Width, imagem.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(imagem, posicao, Color.White);
        }
    }
}