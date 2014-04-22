using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SubBeatle.Code.Sistema
{
    class Placar
    {
        Vector2 Position;

        public bool Chegou;

        public Placar(Vector2 position)
        {
            Position = position;

            Chegou = false;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 Chegada = new Vector2(600, 422);

            Vector2 Forca = Chegada - Position;

            if (Forca.X < 1 && Forca.Y < 1)
            {
                Chegou = true;
            }

            Forca.Normalize();
            Forca *= 3;

            Position += Forca;            
        }

        public void Draw(SpriteBatch spriteBatch , SpriteFont Font)
        {
            spriteBatch.DrawString(Font, "+ 1", Position, Color.White);
        }
    }
}