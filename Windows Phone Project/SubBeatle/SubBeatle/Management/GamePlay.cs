using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SubBeatle.BaseClasses;
using SubBeatle.DataBase;

using SubBeatle.Code;
using SubBeatle.Code.Particle;
using SubBeatle.Code.Personagem;
using SubBeatle.Code.Objetos;
using SubBeatle.Code.Model;
using SubBeatle.Code.Sistema;

namespace SubBeatle.Management
{
    class GamePlay : State
    {
        ParticleEngine particulas;
        
        Personagem Jogador;

        GerenciadorObjetosColetaveis gerenciadorObjetos;

        CamadasController cenario;

        Texture2D BarTexture , CriticalImage;

        bool IsCritical , IsMoment;

        float CriticalTime , MetalMinimo;
        
        public GamePlay(StateManager Father)
        {
            Manager = Father;

            BarTexture = Manager.Game.Content.Load<Texture2D>(@"Source/Cenario/Panel");
            CriticalImage = Manager.Game.Content.Load<Texture2D>(@"Source/EnergyBar/CriticalEnergyBar");

            Restart();
        }

        public override void Restart()
        {
            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Play(Manager.GamePlaySong);    
            }

            cenario = new CamadasController(Manager.Game);

            this.Jogador = new Personagem(Manager.Game.Content);

            gerenciadorObjetos = new GerenciadorObjetosColetaveis(Manager.Game.Content);

            particulas = new ParticleEngine(Manager.Game.Content.Load<Texture2D>("Source/Bolha"), Vector2.Zero);

            IsCritical = false;
            IsMoment = false;

            CriticalTime = 0f;

            base.Restart();
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.GoToPause();
            }

            # endregion

            # region Won

            if (Jogador.LevelAtual == Personagem.LevelPersonagem.level3 &&
                Jogador.BarraMetal.Energy >= 400)
            {
               if (StateManager.HasVibrationControl)
                {
                    Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 500));
                }

               Manager.GoToWon(Jogador.QuantidadeVidro);
            }

            # endregion

            # region Lost

            if (Jogador.FimJogo())
            {
                if (StateManager.HasVibrationControl)
                {
                    Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 500));
                }

                Manager.GoToLost();
            }

            # endregion

            Vector2 Local = new Vector2(Manager.TouchedPlace.X, Manager.TouchedPlace.Y);

            Jogador.PerderCombustivel(gameTime);
            
            Jogador.Update(gameTime);

            Vector2 Distance = new Vector2(Manager.TouchedPlace.X, Manager.TouchedPlace.Y) - Jogador.Position;

            Jogador.TargetAngle = (float)Math.Atan2(Distance.Y, Distance.X);
            Jogador.GetAngleToMove();

            Jogador.Apontar(Local);
            
            //Jogador.CalcularAngulo(Local);

            if (Constantes.retLimitador.Contains(Jogador.rect) == false)
            {
                cenario.Velocidade = Personagem.velocidade;

                cenario.SlideHotizontal();
                cenario.SlideVertical();

                gerenciadorObjetos.UpdatePersonagem(Personagem.velocidade);
            }

            gerenciadorObjetos.ChecarPersonagem(ref Jogador);
            gerenciadorObjetos.Update(gameTime);
            
            Rectangle retanguloAtual = Jogador.AnimacaoAtual.GetSourceRectangle();
            
            particulas.Update(new Vector2(Jogador.Position.X + retanguloAtual.Width / 2 ,
                Jogador.Position.Y + retanguloAtual.Height / 2));

            # region Troca de Nível

            if (Jogador.GetTransicao1())
            {
                gerenciadorObjetos.Limpar();
            }

            if (Jogador.GetTransicao2())
            {
                gerenciadorObjetos.Limpar();
            }

            # endregion

            # region MetalMínimo

            switch (Jogador.LevelAtual)
            {
                case Personagem.LevelPersonagem.level1:

                    MetalMinimo = 40f;

                    break;

                case Personagem.LevelPersonagem.level2:

                    MetalMinimo = 85f;

                    break;

                case Personagem.LevelPersonagem.level3:

                    MetalMinimo = 130f;

                    break;
            }

            # endregion

            # region Metal Crítico

            if (IsCritical)
            {
                CriticalTime += gameTime.ElapsedGameTime.Milliseconds;

                if (CriticalTime >= 1000)
                {
                    CriticalTime = 0f;

                    IsMoment = !IsMoment;
                }

                if (Jogador.BarraMetal.Energy > MetalMinimo)
                {
                    IsCritical = false;
                    IsMoment = false;

                    CriticalTime = 0f;
                }
            }
            else
            {

                if (Jogador.BarraMetal.Energy <= MetalMinimo)
                {
                    IsCritical = true;
                    IsMoment = true;

                    CriticalTime = 0f;
                }
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            cenario.Draw(Manager.spriteBatch);

            gerenciadorObjetos.Draw(Manager.spriteBatch , Manager.Font);

            Manager.spriteBatch.Draw(BarTexture, new Vector2(0, 480 - BarTexture.Height), Color.White);

            for (int i = 0; i < gerenciadorObjetos.Pontos.Count; i++)
            {
                gerenciadorObjetos.Pontos[i].Draw(Manager.spriteBatch, Manager.Font);
            }

            particulas.Draw(Manager.spriteBatch);

            Jogador.Draw(Manager.spriteBatch);
            Jogador.DesenharBarra(Manager.spriteBatch);
            Jogador.DesenharTextos(Manager.spriteBatch, Manager.Font);

            if (IsMoment)
            {
                Manager.spriteBatch.Draw(CriticalImage, new Vector2(100, 480 - CriticalImage.Height - 10),
                    Color.White);
            }

            base.Draw(gameTime);
        }
    }
}