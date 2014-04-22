using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.IO;

namespace SubBeatle.Code.Personagem
{
    public class Sprite
    {
        protected Texture2D textura;
        public Texture2D Textura
        {
            get { return textura; }
        }
        public float rotation;
        protected Rectangle _boundingRect = new Rectangle();
        protected Vector2 _posicao;
        protected Rectangle sourceRect = new Rectangle();

        #region Animacao

        protected string sheetPath;
        protected string sheetPrefix;
        //Dicionario com os SourceRectangles de todos os frames
        protected Dictionary<string, Rectangle> spriteSourceRectangles = new Dictionary<string, Rectangle>();
        //Contador do tempo para animação
        protected long elapsedTime = 0;
        //Tempo que cada frame fica na tela
        protected long frameTime;
        //Frame atual a ser desenhado
        protected int actualFrame = 0;

        #endregion

        //Evento sinalizando fim de animacao
        public delegate void AnimationEndedHandler(object sender);
        public event AnimationEndedHandler animationEnded;

        /// <summary>
        /// BoundingRectangle para colisão do Sprite
        /// </summary>
        public Rectangle boundingRect
        {
            get { return _boundingRect; }
            private set { _boundingRect = value; }
        }
        public bool Acabou;
        /// <summary>
        /// Posição do Sprite.
        /// Ao atualizar a posição, o BoundingRectangle também será atualizado automaticamente.
        /// </summary>
        public Vector2 posicao
        {
            get { return _posicao; }
            set
            {
                _posicao = value;
                //Atualiza a posição do BoundingRectangle
                _boundingRect.X = (int)_posicao.X;
                _boundingRect.Y = (int)_posicao.Y;
                ////Se o sprite não for animado, define largura e altura iguais à da textura
                //if (frameTime == 0)
                //{
                //    _boundingRect.Width = textura.Width;
                //    _boundingRect.Height = textura.Height;
                //}
            }
        }

        public Sprite(Texture2D textura, Vector2 posicao)
            : this(textura, posicao, null, null, null)
        {
        }

        public Sprite(Texture2D textura, Vector2 posicao, string Path, string Prefix, long? timeLapse)
        {
            this.textura = textura;
            this.posicao = posicao;

            this.sheetPath = Path;
            this.sheetPrefix = Prefix;

            //Passa o diretório para o metodo carregar o txt
            if (Path != null)
                LoadTexture();

            this.frameTime = timeLapse ?? 0;

            if (timeLapse != null)
            {
                //this._boundingRect.Width = spriteSourceRectangles["Frame_0"].Width;
                //this._boundingRect.Height = spriteSourceRectangles["Frame_0"].Height;
                //this.sourceRect = spriteSourceRectangles["Frame_0"];
            }
            else
            {
                this._boundingRect.Width = this.textura.Width;
                this._boundingRect.Height = this.textura.Height;
                sourceRect = textura.Bounds;
            }
        }

        public Color[] GetColorData()
        {
            Color[] colorArray = new Color[boundingRect.Width * boundingRect.Height];

            textura.GetData(0, sourceRect, colorArray, 0, sourceRect.Height * sourceRect.Width);

            return colorArray;
        }

        public Rectangle GetSourceRectangle()
        {
            return this.sourceRect;
        }

        public virtual void Update(GameTime gameTime)
        {
            //Sprite animado
            if (frameTime > 0)
            {
                //Se está na hora de trocar o frame
                if (elapsedTime >= frameTime)
                {                    
                    //Avança para o próximo frame
                    actualFrame++;

                    //Se não houverem mais frames para avaçar, retorna ao início e
                    //dispara um evento avisando que um clico de animação terminou
                    if (actualFrame >= spriteSourceRectangles.Count)
                    {
                        actualFrame = 0;
                        if (animationEnded != null)
                        {
                            animationEnded(this);
                        }

                            this.Acabou = true;
                    }

                    //Reinicia o contador de tempo
                    elapsedTime = 0;

                    //Modifica o ponteiro sourceRect para o frame atual
                    sourceRect = spriteSourceRectangles[sheetPrefix + actualFrame];
                    //Atualiza o boundingRectangle
                    _boundingRect.Width = sourceRect.Width;
                    _boundingRect.Height = sourceRect.Height;
                }

                //Soma o ElapsedGameTime ao contador
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            }
        }

        /// <summary>
        /// Método responsável por desenhar o sprite
        /// </summary>
        /// <param name="gameTime">GameTime do XNA</param>
        /// <param name="spriteBatch">SpriteBatch onde o sprite deverá ser desenhado</param>
        /// <param name="posicao">Posição em que o sprite deve ser desenhado</param>
        /// <param name="flip">Definição se o sprite deve ser espelhado horizontalmente ou não</param>
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 posicao, bool flip, float rotation)
        {
            if (posicao != this.posicao)
                this.posicao = posicao;

            this.rotation = rotation;

            SpriteEffects effects = SpriteEffects.None;
            if (flip)
                effects = SpriteEffects.FlipHorizontally;

            Vector2 origem = new Vector2(sourceRect.Width/2,sourceRect.Height/2);
            spriteBatch.Draw(textura, adjustedRectangle(), sourceRect, Color.White, rotation + MathHelper.PiOver2, origem, effects, 0);
        }
        public Rectangle adjustedRectangle()
        {
           RotatedRectangle rect = new RotatedRectangle(boundingRect, rotation);
           return new Rectangle(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), rect.Width, rect.Height);
        }

        /// <summary>
        /// Método responsável por desenhar o sprite
        /// </summary>
        /// <param name="gameTime">GameTime do XNA</param>
        /// <param name="spriteBatch">SpriteBatch onde o sprite deverá ser desenhado</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Draw(spriteBatch, this.posicao, false, 0);
        }

        /// <summary>
        /// Método responsável por desenhar o sprite
        /// </summary>
        /// <param name="gameTime">GameTime do XNA</param>
        /// <param name="spriteBatch">SpriteBatch onde o sprite deverá ser desenhado</param>
        /// <param name="flip">Definição se o sprite deve ser espelhado horizontalmente ou não</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool flip)
        {
            this.Draw(spriteBatch, this.posicao, flip, 0);
        }
        public void LoadTexture()
        {
            // open a StreamReader to read the index

            using (StreamReader reader = new StreamReader(TitleContainer.OpenStream(@"Content\" + sheetPath + ".txt")))
            {
                // while we're not done reading...
                while (!reader.EndOfStream)
                {
                    // get a line
                    string line = reader.ReadLine();

                    // split at the equals sign
                    string[] sides = line.Split('=');

                    // trim the right side and split based on spaces
                    string[] rectParts = sides[1].Trim().Split(' ');

                    // create a rectangle from those parts
                    Rectangle r = new Rectangle(
                       int.Parse(rectParts[0]),
                       int.Parse(rectParts[1]),
                       int.Parse(rectParts[2]),
                       int.Parse(rectParts[3]));

                    // add the name and rectangle to the dictionary
                    spriteSourceRectangles.Add(sides[0].Trim(), r);
                }
            }
        }
    }
}
