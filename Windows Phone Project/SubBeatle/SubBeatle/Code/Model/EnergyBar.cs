using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SubBeatle.Code.Model
{
    // Classe para barras de energia, saúde, vida etc... porém somente horizontal
    public class EnergyBar
    {
        // Imagem da caixa por trás da barra de energia
        private Texture2D boxTexture = null;

        // Imagem da barra de energia
        private Texture2D energyTexture = null;

        // Posição da caixa
        private Vector2 boxPosition;

        // Posição da barra de energia
        private Vector2 energyPosition;

        // Nível máximo de energia
        public float MaxEnergy
        {
            get { return this.maxEnergy; }
            set { this.maxEnergy = value; }
        }
        private float maxEnergy = 100.0f;

        // Energia
        public float Energy
        {
            get { return this.energy; }
            set
            {
                this.energy = MathHelper.Clamp(value, 0, maxEnergy);
            }
        }
        public float energy = 100.0f;

        // Cor da barra de energia quando esta vazia
        private Color emptyEnergyColor = Color.White;

        // Cor da barra de energia quando esta cheia
        private Color fullEnergyColor = Color.White;

        // Indica se deseja reduzir a energia para o outro lado (padrão = esquerda, flip = direita)
        private bool flipEnergyReduce = false;

       //Básico
        public EnergyBar(Texture2D boxTexture, Texture2D energyTexture, Vector2 boxPosition, Vector2 energyPosition)
        {
            this.boxTexture = boxTexture;
            this.energyTexture = energyTexture;
            this.boxPosition = boxPosition;
            this.energyPosition = energyPosition;
        }
        //com cor
        public EnergyBar(Texture2D boxTexture, Texture2D energyTexture, Vector2 boxPosition, Vector2 energyPosition, Color color)
        {
            this.boxTexture = boxTexture;
            this.energyTexture = energyTexture;
            this.boxPosition = boxPosition;
            this.energyPosition = energyPosition;
            this.emptyEnergyColor = color;
            this.fullEnergyColor = color;
        }
        //cores diferentes para cheio e vazio
        public EnergyBar(Texture2D boxTexture, Texture2D energyTexture, Vector2 boxPosition, Vector2 energyPosition, Color emptyEnergyColor, Color fullEnergyColor)
        {
            this.boxTexture = boxTexture;
            this.energyTexture = energyTexture;
            this.boxPosition = boxPosition;
            this.energyPosition = energyPosition;
            this.emptyEnergyColor = emptyEnergyColor;
            this.fullEnergyColor = fullEnergyColor;
        }
        //Barra invertida
        public EnergyBar(Texture2D boxTexture, Texture2D energyTexture, Vector2 boxPosition, Vector2 energyPosition, Color emptyEnergyColor, Color fullEnergyColor, bool flipEnergyReduce)
        {
            this.boxTexture = boxTexture;
            this.energyTexture = energyTexture;
            this.boxPosition = boxPosition;
            this.energyPosition = energyPosition;
            this.emptyEnergyColor = emptyEnergyColor;
            this.fullEnergyColor = fullEnergyColor;
            this.flipEnergyReduce = flipEnergyReduce;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenha a caixa que envolve a energia
            spriteBatch.Draw(boxTexture,
                boxPosition,
                new Rectangle(0, 0, boxTexture.Width, boxTexture.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f);

            // Desenha a barra de energia
            spriteBatch.Draw(energyTexture,
                energyPosition,
                new Rectangle(0, 0, (int)(energy * energyTexture.Width / maxEnergy), (int)energyTexture.Height),
                Color.Lerp(emptyEnergyColor, fullEnergyColor, energy / maxEnergy),
                flipEnergyReduce ? MathHelper.ToRadians(180) : 0.0f,
                flipEnergyReduce? new Vector2(energyTexture.Width, energyTexture.Height) : Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f);
        }

        public void MoveTo(Vector2 newPosition)
        {
            Vector2 diff = energyPosition - boxPosition;
            boxPosition = newPosition;
            energyPosition = boxPosition + diff;
        }

    }
}