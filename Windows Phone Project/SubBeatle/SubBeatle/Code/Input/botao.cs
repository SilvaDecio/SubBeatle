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

namespace SubBeatle.Code.Input
{
    class botao
    {
        Texture2D Imagem;
        Vector2 Posicao;
        Rectangle Caixa;
        Colisaopixel Colisor;
        bool Clicado;
        Texture2D _Imagefundo;

        public botao(Texture2D _imagem, Vector2 _pos)
        {
            this.Imagem = _imagem;
            this.Posicao = _pos;
            this.Caixa = new Rectangle((int)Posicao.X, (int)Posicao.Y, Imagem.Width, Imagem.Height);
            this.Colisor = new Colisaopixel(Imagem, Caixa);
            Clicado = false;
        }

        public void AtualizarBotao()
        {
            this.Caixa = new Rectangle((int)Posicao.X, (int)Posicao.Y, Imagem.Width, Imagem.Height);
            Colisor.AtualizarColisao(Caixa);
        }
        
        public bool VerificarClique(Cursor mouse)
        {
            bool Clicou = false;
            
            if (mouse.Colisor.VerificarColisao(this.Colisor))
            {
                if (mouse.mouse.LeftButton == ButtonState.Pressed)
                {
                    Clicado = true;
                }
            }

            if (Clicado == true && mouse.mouse.LeftButton == ButtonState.Released)
            {
                Clicado = false;
                Clicou = true;
            }

            return Clicou;
        }


        public void colocar(Texture2D _Textura)
        {
            this._Imagefundo = _Textura;
        }

        public void Retirar()
        {
            this._Imagefundo = null;
        }

        public bool VerficarCliqueSimples(Cursor cursor)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //if (Caixa.Intersects(this.Caixa))
                if(this.Caixa.Contains((int)cursor.Pos.X, (int)cursor.Pos.Y))
                {
                    return true;
                }
            }

            return false;
        }

        public void DesenharBotao(SpriteBatch desenhador)
        {
            this.DesnharPorTras(desenhador);
            this.Caixa = new Rectangle((int)Posicao.X, (int)Posicao.Y, Imagem.Width, Imagem.Height);
            desenhador.Draw(Imagem, Posicao, Color.White);
        }

        public void DesnharPorTras(SpriteBatch desenhador)
        {
            if (_Imagefundo != null)
                desenhador.Draw(_Imagefundo, Posicao, Color.White);
        }

        public void DesenharString(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "X", new Vector2(Caixa.Left, Caixa.Top), Color.Red);
            spriteBatch.DrawString(font, "X", new Vector2(Caixa.Left, Caixa.Bottom), Color.Red);
            spriteBatch.DrawString(font, "X", new Vector2(Caixa.Right, Caixa.Top), Color.Red);
            spriteBatch.DrawString(font, "X", new Vector2(Caixa.Right, Caixa.Bottom), Color.Red);
        }
    }
}