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

namespace SubBeatle.Code
{
    class Cursor
    {
       public Texture2D Imagem;
       public Vector2 Pos;
       public Rectangle Caixa;
       public Colisaopixel Colisor;
       public MouseState mouse;

       public Cursor(Texture2D _Imagem, Vector2 _Pos)
       {
           this.Imagem = _Imagem;
           this.Pos = _Pos;
           this.Caixa = new Rectangle((int)Pos.X, (int)Pos.Y, Imagem.Width, Imagem.Height);
           this.Colisor = new Colisaopixel(Imagem, Caixa);
       }

       public void Atualizar()
       {
           mouse = Mouse.GetState();
           this.Pos.X = mouse.X;    
           this.Pos.Y = mouse.Y;
           AtualizarRetangulo();
       }

       public void AtualizarRetangulo()
       {
           this.Caixa = new Rectangle((int)Pos.X,(int)Pos.Y,Imagem.Width,Imagem.Height);
           this.Colisor.AtualizarColisao(Caixa);
       }

       public void Desenhar(SpriteBatch desenhador)
       {
           desenhador.Draw(Imagem, Pos, Color.White);
       }
    }
}