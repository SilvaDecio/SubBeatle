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
    class Colisaopixel
    {
        #region Variáeis

        Color[] dados;
        Texture2D textura;
        Rectangle caixa;

        #endregion

        #region Construtores

        public Colisaopixel(Texture2D imagem, Rectangle CaixaDelimitadora)
        {
            this.textura = imagem;
            this.dados = new Color[textura.Width * textura.Height];
            this.textura.GetData(dados);
            this.caixa = CaixaDelimitadora;
        }

        #endregion

        #region Métodos

        public Texture2D Textura
        {
            get { return textura; }
            set { textura = value; }
        }

        public Color[] Dados
        {
            get { return dados; }
            set { dados = value; }
        }

        public Rectangle Caixa
        {
            get { return caixa; }
            set { caixa = value; }
        }

        public void AtualizarColisao(Rectangle Caixa)
        {
            this.caixa = Caixa;
        }

        public bool VerificarColisao(Colisaopixel Colisor)
        {
            if(Colisor.Caixa.Intersects(this.caixa))
            {
            int Cima = Math.Max(Colisor.caixa.Top, this.caixa.Top);
            int Baixo = Math.Min(Colisor.caixa.Bottom, this.caixa.Bottom);
            int Esquerda = Math.Max(Colisor.caixa.Left, this.caixa.Left);
            int Direita = Math.Min(Colisor.caixa.Right, this.caixa.Right);

            for (int y = Cima; y < Baixo; y++)
            {
                for (int x = Esquerda; x < Direita; x++)
                {

                    Color cor1 = dados[(x-this.caixa.Left) + (y-this.caixa.Top) * this.caixa.Width];
                    Color cor2 = Colisor.Dados[(x -Colisor.caixa.Left) + (y-Colisor.caixa.Top) * Colisor.textura.Width];

                    if(cor1.A != 0 && cor2.A != 0)
                    {
                        return true;
                    }
                }
            }
            }

            return false;

        }

        #endregion

    }
}