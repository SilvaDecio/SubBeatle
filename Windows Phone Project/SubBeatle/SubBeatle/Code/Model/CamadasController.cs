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

namespace SubBeatle.Code.Model
{
    class CamadasController
    {
        Camada[,] CenarioGrade;
        Texture2D[,] CenarioOnda, CenarioAgua;
        public Vector2 Velocidade;

        public CamadasController(Game game)
        {
            this.CenarioGrade = new Camada[2, 2];
            this.CenarioGrade[0, 0] = new Camada(game.Content.Load<Texture2D>(@"Source//Cenario//pedras_4"), new Vector2(0, 0), new Vector2(3, 3));
            this.CenarioGrade[0, 1] = new Camada(game.Content.Load<Texture2D>(@"Source//Cenario//pedras_4"), new Vector2(800, 0), new Vector2(3, 3));
            this.CenarioGrade[1, 0] = new Camada(game.Content.Load<Texture2D>(@"Source//Cenario//pedras_4"), new Vector2(0, 480), new Vector2(3, 3));
            this.CenarioGrade[1, 1] = new Camada(game.Content.Load<Texture2D>(@"Source//Cenario//pedras_4"), new Vector2(800, 480), new Vector2(3, 3));

            CenarioOnda = new Texture2D[2, 2];
            this.CenarioOnda[0, 0] = game.Content.Load<Texture2D>(@"Source//Cenario//Ondas2");
            this.CenarioOnda[0, 1] = game.Content.Load<Texture2D>(@"Source//Cenario//Ondas2");
            this.CenarioOnda[1, 0] = game.Content.Load<Texture2D>(@"Source//Cenario//Ondas2");
            this.CenarioOnda[1, 1] = game.Content.Load<Texture2D>(@"Source//Cenario//Ondas2");

            CenarioAgua = new Texture2D[2, 2];
            this.CenarioAgua[0, 0] = game.Content.Load<Texture2D>(@"Source//Cenario//Agua_2");
            this.CenarioAgua[0, 1] = game.Content.Load<Texture2D>(@"Source//Cenario//Agua_2");
            this.CenarioAgua[1, 0] = game.Content.Load<Texture2D>(@"Source//Cenario//Agua_2");
            this.CenarioAgua[1, 1] = game.Content.Load<Texture2D>(@"Source//Cenario//Agua_2");

            Velocidade = new Vector2();
        }
        
        public void SlideVertical()
        {
            for (int c = 0; c < 2; c++)
            {
                for (int j = 0; j < 2; j++)
                {
                    this.CenarioGrade[c, j]._Position.Y += Velocidade.Y;
                    
                    if (CenarioGrade[0, 0]._Position.Y < 0)
                    {
                        CenarioGrade[1, 0]._Position.Y = CenarioGrade[0, 0]._Position.Y + CenarioGrade[0, 0]._Imagem.Height -1;
                        CenarioGrade[1, 1]._Position.Y = CenarioGrade[0, 0]._Position.Y + CenarioGrade[0, 0]._Imagem.Height -1;
                        
                        if (CenarioGrade[1, 0]._Position.Y < 0)
                        {
                            CenarioGrade[0, 0]._Position.Y = CenarioGrade[1, 0]._Position.Y + CenarioGrade[0, 0]._Imagem.Height - 2;
                            CenarioGrade[0, 1]._Position.Y = CenarioGrade[1, 0]._Position.Y + CenarioGrade[0, 0]._Imagem.Height - 2;
                        }
                    }

                    if (CenarioGrade[0, 0]._Position.Y > 0)
                    {
                        CenarioGrade[1, 0]._Position.Y = CenarioGrade[0, 0]._Position.Y - CenarioGrade[0, 0]._Imagem.Height + 1;
                        CenarioGrade[1, 1]._Position.Y = CenarioGrade[0, 0]._Position.Y - CenarioGrade[0, 0]._Imagem.Height + 1;
                        
                        if (CenarioGrade[1, 0]._Position.Y > 0)
                        {
                            CenarioGrade[0, 0]._Position.Y = CenarioGrade[0, 1]._Position.Y - CenarioGrade[0, 0]._Imagem.Height +2;
                            CenarioGrade[0, 1]._Position.Y = CenarioGrade[0, 1]._Position.Y - CenarioGrade[0, 0]._Imagem.Height +2;
                        }
                    }
                }
            }
        }

        public void SlideHotizontal()
        {
            for (int c = 0; c < 2; c++)
            {
                for (int j = 0; j < 2; j++)
                {
                    this.CenarioGrade[c, j]._Position.X += Velocidade.X;
                    
                    if (CenarioGrade[0, 0]._Position.X < 0)
                    {
                        CenarioGrade[0, 1]._Position.X = CenarioGrade[0, 0]._Position.X + CenarioGrade[0, 0]._Imagem.Width -1;
                        CenarioGrade[1, 1]._Position.X = CenarioGrade[0, 0]._Position.X + CenarioGrade[0, 0]._Imagem.Width-2;

                        if (CenarioGrade[0, 1]._Position.X < 0)
                        {
                            CenarioGrade[0, 0]._Position.X = CenarioGrade[0, 1]._Position.X + CenarioGrade[0, 0]._Imagem.Width -3 ;
                            CenarioGrade[1, 0]._Position.X = CenarioGrade[0, 1]._Position.X + CenarioGrade[0, 0]._Imagem.Width -2 ;
                        }
                    }

                    if (CenarioGrade[0, 0]._Position.X > 0)
                    {
                        CenarioGrade[0, 1]._Position.X = CenarioGrade[0, 0]._Position.X - CenarioGrade[0, 0]._Imagem.Width +1;
                        CenarioGrade[1, 1]._Position.X = CenarioGrade[0, 0]._Position.X - CenarioGrade[0, 0]._Imagem.Width +2;
                        
                        if (CenarioGrade[0, 0]._Position.X > 800)
                        {
                            CenarioGrade[0, 0]._Position.X = CenarioGrade[0, 1]._Position.X - CenarioGrade[0, 0]._Imagem.Width + 3;
                            CenarioGrade[1, 0]._Position.X = CenarioGrade[0, 1]._Position.X - CenarioGrade[0, 0]._Imagem.Width + 2;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch desenhador)
        {
            for (int c = 0; c < 2; c++)
            {
                for (int j = 0; j < 2; j++)
                {
                    //desenhador.Draw(CenarioFundo[c, j], CenarioGrade[c, j]._Position, Color.White);
                    this.CenarioGrade[c, j].Draw(desenhador);
                   
                    desenhador.Draw(CenarioAgua[c, j], CenarioGrade[c, j]._Position, Color.White);
                    desenhador.Draw(CenarioOnda[c, j], CenarioGrade[c, j]._Position, Color.White);                
                }
            }
        }


        public void DrawInfo(SpriteBatch desenhador, SpriteFont font)
        {
#if DEBUG
            desenhador.DrawString(font, "Posicao 0:0 " + CenarioGrade[0, 0]._Position.ToString(),new Vector2(100,100), Color.Red);
            desenhador.DrawString(font, "Posicao 0:1 " +  CenarioGrade[0, 1]._Position.ToString(), new Vector2(100, 200), Color.Red);
            desenhador.DrawString(font, "Posicao 1:0 " +  CenarioGrade[1, 0]._Position.ToString(), new Vector2(100, 300), Color.Red);
            desenhador.DrawString(font, "Posicao 1:1 " + CenarioGrade[1, 1]._Position.ToString(), new Vector2(100, 400), Color.Red);
#endif
        }
    }
}
