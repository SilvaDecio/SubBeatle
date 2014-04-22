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

using SubBeatle;
using SubBeatle.Code.Objetos;

namespace SubBeatle.Code.Model
{
    class Cenario : ObjetosJogo
    {
        Texture2D fundo;
        Texture2D barra;
        public Vector2 velocidade = Vector2.Zero;
        List<Texture2D> objetos = new List<Texture2D>();
        List<Texture2D> objetosX = new List<Texture2D>();
        List<Vector2> positions = new List<Vector2>();
        List<Vector2> positionsX = new List<Vector2>();

        public Cenario(ContentManager content) : base(null, Vector2.Zero)
        {
            objetos.Add(content.Load<Texture2D>(@"Cenario/Pedras_4"));
            objetos.Add(content.Load<Texture2D>(@"Cenario/Agua_2"));
            objetos.Add(content.Load<Texture2D>(@"Cenario/Ondas_3"));

            objetos.Add(content.Load<Texture2D>(@"Cenario/Pedras_4"));
            objetos.Add(content.Load<Texture2D>(@"Cenario/Agua_2"));
            objetos.Add(content.Load<Texture2D>(@"Cenario/Ondas_3"));

            objetosX.Add(content.Load<Texture2D>(@"Cenario/Pedras_4"));
            objetosX.Add(content.Load<Texture2D>(@"Cenario/Agua_2"));
            objetosX.Add(content.Load<Texture2D>(@"Cenario/Ondas_3"));
           
            objetosX.Add(content.Load<Texture2D>(@"Cenario/Pedras_4"));
            objetosX.Add(content.Load<Texture2D>(@"Cenario/Agua_2"));
            objetosX.Add(content.Load<Texture2D>(@"Cenario/Ondas_3"));

            barra = content.Load<Texture2D>(@"Cenario/BARRA");
            fundo = content.Load<Texture2D>(@"Cenario/Fundo_5");

            for (int i = 0; i < 3; i++)
            {
                positions.Add(Vector2.Zero);
                positionsX.Add(new Vector2(-Game1.TamanhoTela.X,0));
            }
            for (int i = 0; i < 3; i++)
            {
                positions.Add(new Vector2(0, Game1.TamanhoTela.Y ));
                positionsX.Add(new Vector2(-Game1.TamanhoTela.X, Game1.TamanhoTela.Y));
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] += Personagem.Personagem.velocidade;
                positionsX[i] += Personagem.Personagem.velocidade;
            }       
            UpdateCenario();
        }
        public void UpdateCenario()
        {
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].Y > Game1.TamanhoTela.Y)
                    positions[i] = new Vector2(positions[i].X, -Game1.TamanhoTela.Y);
                
                else if (positions[i].Y < -Game1.TamanhoTela.Y )
                    positions[i] = new Vector2(positions[i].X, Game1.TamanhoTela.Y-1);

                positionsX[i] = new Vector2(positionsX[i].X, positions[i].Y);

                if (positions[i].X > Game1.TamanhoTela.X)
                {
                    positions[i] = new Vector2(-Game1.TamanhoTela.X + 1, positions[i].Y);
                    
                }
                else if (positions[i].X < -Game1.TamanhoTela.X)
                {
                    positions[i] = new Vector2(Game1.TamanhoTela.X -1, positions[i].Y);
                }
                if (positionsX[i].X > Game1.TamanhoTela.X)
                {
                    positionsX[i] = new Vector2(-Game1.TamanhoTela.X +1, positionsX[i].Y);
                }
                else if (positionsX[i].X < -Game1.TamanhoTela.X)
                {
                    positionsX[i] = new Vector2(Game1.TamanhoTela.X -1, positionsX[i].Y);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fundo, Vector2.Zero, Color.Aqua);

            for (int i = 0; i < objetos.Count; i++)
            {
                spriteBatch.Draw(objetos[i], new Rectangle((int)positions[i].X, (int)positions[i].Y,(int) Game1.TamanhoTela.X,(int)Game1.TamanhoTela.Y), Color.White);
                spriteBatch.Draw(objetosX[i], new Rectangle((int)positionsX[i].X, (int)positionsX[i].Y, (int)Game1.TamanhoTela.X, (int)Game1.TamanhoTela.Y), Color.White);
            }
        }
        public void DrawBarra(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barra, new Vector2(0, 720 - barra.Height), Color.White);
        }
    } 
}