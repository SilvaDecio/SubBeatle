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

using SubBeatle.Code.Input;
using SubBeatle.Code.Model;
using SubBeatle.Code.Sistema;

namespace SubBeatle.Code.Personagem
{
    public class Personagem
    {
        #region atributos

        private float escala;
        public float rotacao;
        private float VelocidadeEscalar;
        private float VelocidadeAngular;
        public static Vector2 velocidade;
        private Vector2 Direcao;
        private Sprite animacaoAtual;
        float timeCombustivel;
        private bool AcabouJogo;
        private botao BtnPontuacao;
        private Hud.EscudoPlastico _Escudo;
        private Hud.HudNiveis _Niveis;

        public Sprite AnimacaoAtual
        {
            get { return animacaoAtual; }
            set 
            {
                animacaoAtual = value;
                animacaoAtual.posicao = position;
            }
        }

        private Sprite Transicao12;
        private Sprite Animacao1;
        private Sprite Animacao2;
        private Sprite Animacao3;
        private Sprite Transicao23;
        private float AnguloDistancia;
        public Rectangle rect;
        private float quantidadeMetal;
        private int quantidadePlastico;
        private int quantidadeOrganico = 10;
        private int quantidadeVidro;
        float elapsedProtection;
        bool protection;
        private bool Transicao1,Transicao2;
        private Vector2 position;
        public EnergyBar BarraMetal;
        private float Aceleracao;
        private float VelocidadeMaxima;

        #endregion

        #region GetsAndSet

        public Vector2 Position
        {
          get { return position; }
          set { position = value; }
        }
        
        public float QuantidadeMetal
        {
            get { return (int)quantidadeMetal; }
            set
            {
                quantidadeMetal = value;
                BarraMetal.Energy = quantidadeMetal;
            }
        }

        public int QuantidadeOrganico
        {
            get { return quantidadeOrganico; }
            set { quantidadeOrganico = value; }
        }
       

        public int QuantidadePlastico
        {
            get { return quantidadePlastico; }
            set { quantidadePlastico = value; }
        }

        public int QuantidadeVidro
        {
            get { return quantidadeVidro; }
            set { quantidadeVidro = value; }
        }

        #endregion

        public float TargetAngle;

        public enum LevelPersonagem
        {
            level1,
            level2,
            level3
        }

        private LevelPersonagem levelAtual;

        public LevelPersonagem LevelAtual
        {
            get { return levelAtual; }
            set 
            { 
                levelAtual = value;

                switch (value)
                {
                    case LevelPersonagem.level1:
                        AnimacaoAtual = Animacao1;
                        break;

                    case LevelPersonagem.level2:
                        AnimacaoAtual = Animacao2;
                        break;

                    case LevelPersonagem.level3:
                        AnimacaoAtual = Animacao3;
                        break;
                   
                    default:
                        break;
                }
            }
        }

        private void IniciarValores()
        {
            this.escala = 1;
            this.rotacao = 0;
            this.VelocidadeAngular = 0.5f;
            this.VelocidadeEscalar = 6f;
            this.quantidadeMetal = 50;
            this.quantidadePlastico = 2;
            this.protection = true;
            this.VelocidadeMaxima = 10;
            this.Aceleracao = 2f;

            TargetAngle = 0f;
        }

        private void CarregarAnimacao(ContentManager content)
        {
            this.Animacao1 = new Sprite(content.Load<Texture2D>("Source/Animacao/fase1"), Vector2.Zero, "Source/Animacao/fase1", "fase1_", 1);
            this.Animacao2 = new Sprite(content.Load<Texture2D>("Source/Animacao/fase2"), Vector2.Zero, "Source/Animacao/fase2", "fase2_", 1);
            this.Animacao3 = new Sprite(content.Load<Texture2D>("Source/Animacao/fase3"), Vector2.Zero, "Source/Animacao/fase3", "Fase3_", 1);
            this.Transicao12 = new Sprite(content.Load<Texture2D>("Source/Animacao/transicao1"), Vector2.Zero, "Source/Animacao/transicao1", "transicao", 1);
            this.Transicao23 = new Sprite(content.Load<Texture2D>("Source/Animacao/transicao2"), Vector2.Zero, "Source/Animacao/transicao2", "transicao", 1);
            this.AnimacaoAtual = Animacao1;
        }

        public Personagem(ContentManager content)
        {
            this.AcabouJogo = false;
            this.position = new Vector2(400 , 240);
            this.CarregarAnimacao(content);
            this.animacaoAtual.posicao = this.position;
            this.IniciarValores();
            rect = animacaoAtual.boundingRect;
            this._Escudo = new Hud.EscudoPlastico(content, new Vector2(500, 410));
            Texture2D box = content.Load<Texture2D>("Source/EnergyBar/box");
            Texture2D energybar = content.Load<Texture2D>("Source/EnergyBar/energybar");
            BarraMetal = new EnergyBar(box,
                energybar,
                new Vector2(100, 480 - box.Height - 10),
                new Vector2(100, 480 - energybar.Height - 10)
                );

            levelAtual = LevelPersonagem.level1;
            BtnPontuacao = new botao(content.Load<Texture2D>("Source//Hud//Ponto"), new Vector2(600, 422));
            this._Niveis = new Hud.HudNiveis(content);
            this.AnimacaoAtual.animationEnded += new Sprite.AnimationEndedHandler(animacaoAtual_animationEnded);
        }

        private void animacaoAtual_animationEnded(object sender)
        {
            if (this.Transicao1)
            {
                LevelAtual = LevelPersonagem.level2;
                Transicao1 = false;
            }
            if (Transicao2)
            {
                LevelAtual = LevelPersonagem.level3;
                Transicao2 = false;
            }
        }

        public void PerderCombustivel(GameTime gameTime)
        {
            timeCombustivel += 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeCombustivel > Constantes.taxaPerdaOrganico)
            {
                QuantidadeOrganico--;
                timeCombustivel = 0;
            }
        }

        public bool GetTransicao1()
        {
            return Transicao1;
        }

        public bool GetTransicao2()
        {
            return Transicao2;
        }

        public void Update(GameTime gameTime)
        {
            this.AnimacaoAtual.Update(gameTime);
            this._Niveis.CheckNivel(levelAtual);
            this._Escudo.checarCondição(quantidadePlastico);
            rect = AnimacaoAtual.boundingRect;
            QuantidadeOrganico = (int)MathHelper.Clamp(QuantidadeOrganico, 0, 30);

            if (quantidadeOrganico <= 0)
            {
                VelocidadeEscalar = 0;
            }
            else
                VelocidadeEscalar = 6f;
            
#if DEBUG
            Console.WriteLine(position);
#endif

            AtualizaMetal(gameTime);
            AtualizaPlastico(gameTime);
            if (AnimacaoAtual.Acabou)
            {
                if (Transicao1)
                {
                    LevelAtual = LevelPersonagem.level2;
                    Transicao1 = false;
                    this.BarraMetal.MaxEnergy = Constantes.MetalMinimoNivel3;
                }
                if (Transicao2)
                {
                    LevelAtual = LevelPersonagem.level3;
                    Transicao2 = false;
                    this.BarraMetal.MaxEnergy = 400;
                }
            }
        }

        public void Desacelerar()
        {
            if (VelocidadeEscalar > 1)
            {
                this.VelocidadeEscalar -= Aceleracao;
            }
        }

        public void Acelerar()
        {
            if (VelocidadeEscalar < VelocidadeMaxima)
            {
                this.VelocidadeEscalar += Aceleracao;
            }
        }

        private void AtualizaPlastico(GameTime gameTime)
        {
            if (protection)
            {
                elapsedProtection += (float)gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedProtection >= Constantes.tempoQuedaPlastico)
                {
                    QuantidadePlastico -= Constantes.taxaPerdaPlastico;
                    elapsedProtection = 0;

                    if (QuantidadePlastico <= 0)
                        protection = false;
                }
            }
            else
            {
                if (QuantidadePlastico > 0)
                    protection = true;
            }
        }

        private void AtualizaMetal(GameTime gameTime)
        {
            if (!protection)
            {
                elapsedProtection += (float)gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedProtection >= Constantes.tempoQuedaMetal)
                {
                    switch (LevelAtual)
                    {
                        case LevelPersonagem.level1:
                            QuantidadeMetal -= Constantes.taxaPerdaMetalLevel1;
                            break;
                        case LevelPersonagem.level2:
                            QuantidadeMetal -= Constantes.taxaPerdaMetalLevel2;
                            break;
                        case LevelPersonagem.level3:
                            QuantidadeMetal -= Constantes.taxaPerdaMetalLevel3;
                            break;
                        default:
                            break;
                    }

                    elapsedProtection = 0;

                    if (QuantidadeMetal <= 0)
                    {
                        this.AcabouJogo = true;
                        return;
                    }
                }
            }

            AtualizarLevelAtual();
        }


        public bool FimJogo()
        {
            Constantes.Pontuacao = quantidadeVidro;
            return this.AcabouJogo;
        }

        private void AtualizarLevelAtual()
        {
            if (QuantidadeMetal <= Constantes.MetalMinimoNivel2)
            {
                //LevelAtual = LevelPersonagem.level1;
            }
            else if (QuantidadeMetal <= Constantes.MetalMinimoNivel3)
            {
                if (levelAtual == LevelPersonagem.level1)
                {
                    if (!Transicao1)
                    {
                        Transicao1 = true;
                        this.Transicao12.posicao = this.animacaoAtual.posicao;
                        this.AnimacaoAtual = this.Transicao12;
                    }
                }
            }
            else
            {
                if (levelAtual == LevelPersonagem.level2)
                {
                    if (!Transicao2)
                    {
                        //LevelAtual = LevelPersonagem.level3;
                        Transicao2 = true;
                        this.Transicao23.posicao = this.animacaoAtual.posicao;
                        this.AnimacaoAtual = this.Transicao23;
                    }
                }
            }
        }

        public bool intersects(Rectangle rect)
        {
            if (new RotatedRectangle(this.rect, AnguloDistancia).Intersects(rect))
                return true;

            return false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            this.AnimacaoAtual.Draw(spritebatch, this.position, false, this.rotacao);
            this.AnimacaoAtual.posicao = this.position;

#if DEBUG
            //spritebatch.DrawString(Game1.fonte, "Level Atual: " + LevelAtual,
            //    new Vector2(100, 100), Color.DarkRed);
            //spritebatch.DrawString(Game1.fonte, "Metal: " + QuantidadeMetal,
            //    new Vector2(100, 140), Color.DarkRed);
            //spritebatch.DrawString(Game1.fonte, "Plastico: " + QuantidadePlastico,
            //    new Vector2(100, 180), Color.DarkRed);
            //spritebatch.DrawString(Game1.fonte, "Organico: " + QuantidadeOrganico,
            ////    new Vector2(100, 220), Color.DarkRed);
            ////spritebatch.DrawString(Game1.fonte, "Vidro: " + QuantidadeVidro,
            ////    new Vector2(100, 260), Color.DarkRed);
#endif
        }

        public void DesenharTextos(SpriteBatch spritebatch, SpriteFont font)
        {
            spritebatch.DrawString(font, quantidadeVidro.ToString(), new Vector2(655, 432),Color.White);
        }

        public void DesenharBarra(SpriteBatch spritebatch)
        {
            BarraMetal.Draw(spritebatch);
            BtnPontuacao.DesenharBotao(spritebatch);
            this._Escudo.DesenharBtn(spritebatch);
            this._Niveis.DesenharNivel(spritebatch);
        }

        public void IncrementarEscala(float valor)
        {
            this.escala += valor;
        }

        public void DecrementarEscala(float valor)
        {
            this.escala -= valor;
        }

        public void GetAngleToMove()
        {
            float Distance = rotacao - TargetAngle;

            if (Distance > Math.PI)
            {
                rotacao -= MathHelper.TwoPi;
            }
            if (Distance < -Math.PI)
            {
                rotacao += MathHelper.TwoPi;
            }
            if (rotacao < TargetAngle)
            {
                rotacao += 0.125f;
            }

            if (rotacao > TargetAngle)
            {
                rotacao -= 0.125f;
            }
        }

        public void Apontar(Vector2 PosicaoAlvo)
        {
            float angX = (float)Math.Cos(rotacao);
            float angY = (float)Math.Sin(rotacao);
            this.Direcao = new Vector2(angX, angY);

            this.position.X += (Direcao.X * VelocidadeEscalar);
            this.position.Y += (Direcao.Y * VelocidadeEscalar);

            position.X = MathHelper.Clamp(
                position.X,
                Constantes.retLimitador.X - 2,
                Constantes.retLimitador.Right - rect.Width + 2);

            position.Y = MathHelper.Clamp(
                position.Y,
                Constantes.retLimitador.Y - 2,
                Constantes.retLimitador.Bottom - rect.Height + 2);

            velocidade = new Vector2(-Direcao.X * VelocidadeEscalar, -Direcao.Y * VelocidadeEscalar);

            //if (Calculardistancia(PosicaoAlvo,100))
            //{
            //    float angulo = 0;
            //    angulo = (float)Math.Atan2(
            //        PosicaoAlvo.Y - (this.position.Y + this.AnimacaoAtual.GetSourceRectangle().Height / 2),
            //        PosicaoAlvo.X - (position.X + this.AnimacaoAtual.GetSourceRectangle().Width / 2)
            //        );

            //    float angX = (float)Math.Cos(angulo);
            //    float angY = (float)Math.Sin(angulo);
            //    this.Direcao = new Vector2(angX, angY);
   
            //    this.position.X += (Direcao.X * VelocidadeEscalar);
            //    this.position.Y += (Direcao.Y * VelocidadeEscalar);

            //    position.X = MathHelper.Clamp(
            //        position.X,
            //        Constantes.retLimitador.X - 2,
            //        Constantes.retLimitador.Right - rect.Width + 2);

            //    position.Y = MathHelper.Clamp(
            //        position.Y,
            //        Constantes.retLimitador.Y - 2,
            //        Constantes.retLimitador.Bottom - rect.Height + 2);

            //    velocidade = new Vector2(-Direcao.X * VelocidadeEscalar, -Direcao.Y * VelocidadeEscalar);
            //}
            //else
            //{
            //    velocidade = Vector2.Zero;
            //}
        }

        private bool Calculardistancia(Vector2 alvo,float limite)
        {
            float distance = Vector2.Distance(this.position, alvo);
            bool Resultado = false;
            if (distance > limite)
            {
                Resultado = true;
            }
            else
            {
                Resultado = false;
            }
            return Resultado;
        }

        public void CalcularAngulo(Vector2 PosicaoAlvo)
        {
                float angulo = 0f;

                angulo = (float)Math.Atan2(
                    PosicaoAlvo.Y - (this.position.Y + this.AnimacaoAtual.GetSourceRectangle().Height / 2),
                    PosicaoAlvo.X - (position.X + this.AnimacaoAtual.GetSourceRectangle().Width / 2)
                    );

                float anguloEmGraus = MathHelper.ToDegrees(angulo);

                if (angulo <= MathHelper.ToRadians(-180f))
                {
                    anguloEmGraus += 180;
                    angulo = MathHelper.ToRadians(anguloEmGraus);
                }

                if (this.rotacao > angulo)
                {
                    this.rotacao -= this.VelocidadeAngular;
                }
                if (this.rotacao < angulo)
                {
                    this.rotacao += this.VelocidadeAngular;
                }
                this.AnguloDistancia = angulo;

                if (Calculardistancia(PosicaoAlvo,20))
                {
                    this.rotacao = angulo;
                }
                else
                {
                    //this.rotacao = 0;
                }
                this.rotacao = angulo;            
        }
    }
}