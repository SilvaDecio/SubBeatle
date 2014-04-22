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

namespace SubBeatle.Code.Cronometro
{
    class Relógio
    {
        TimeSpan Tempo;

        TimeSpan TempoRegressivo;

        TimeSpan TempoLimite;

        float Elapsed;

        ModoCronômetro modo;

        ModoExibir modoexibir;

        EstadoCronômetro estado;

        Vector2 PosRelógio;

        Color CorRelógio;

        int c = 0;

        public Relógio(ModoCronômetro modo,ModoExibir modoView)
        {
            Tempo = new TimeSpan(3, 2, 50);
            Elapsed = 0;
            PosRelógio = new Vector2(0, 0);
            CorRelógio = Color.Red;
            this.modo = modo;
            this.modoexibir = modoView;
            this.estado = EstadoCronômetro.stop;
        }

        public void SetTimerRegressivo(int horas,int minutos, int segundos)
        {
            Tempo = new TimeSpan(horas, minutos, segundos);
            TempoRegressivo = new TimeSpan(horas, minutos, segundos);
        }

        public void SetTempoLimite(int horas, int minutos, int segundos)
        {
            TempoLimite = new TimeSpan(horas, minutos, segundos);
        }

        public TimeSpan GetTimerNow()
        {
            return Tempo;
        }
        public void Inicar()
        {
            estado = EstadoCronômetro.play;
        }

        public bool EsgotouTempo()
        {
            bool Acabou = false;
            if (modo == ModoCronômetro.Regressivo)
            {
                if (Tempo == new TimeSpan(0, 0, 0))
                {
                    estado = EstadoCronômetro.stop;
                    Acabou = true;
                }
            }
            if (modo == ModoCronômetro.Progressivo)
            {
                if (Tempo == TempoLimite)
                {
                    estado = EstadoCronômetro.stop;
                    Acabou = true;
                }
            }
            return Acabou;
        }

        public void Atualizar(GameTime TempoDojogo)
        {
            if (estado == EstadoCronômetro.play)
            {
                if (modo == ModoCronômetro.Progressivo)
                {
                    Elapsed += (float)TempoDojogo.ElapsedGameTime.TotalSeconds;
                    Tempo = TimeSpan.FromSeconds(Elapsed);
                }
                if (modo == ModoCronômetro.Regressivo)
                {
                    Elapsed += (float)TempoDojogo.ElapsedGameTime.TotalSeconds;

                    TempoLimite = TimeSpan.FromSeconds(Elapsed);
                    Tempo = TempoRegressivo.Subtract(TempoLimite);
                    if (Tempo == new TimeSpan(0,0,0))
                    {
                        estado = EstadoCronômetro.stop;
                    }
                }
            }
        }

        public void Parar(GameTime TempoDojogo)
        {
            estado = EstadoCronômetro.stop;
        }

        public void Reiniciar(GameTime TempoDojogo)
        {
            Elapsed = 0;
        }

        public void DesenharTempo(SpriteBatch desenhador, SpriteFont font)
        {
            desenhador.DrawString(font, TempoRegressivo.ToString(), new Vector2(300, 200), Color.Red);
            if (modoexibir == ModoExibir.MinutosSegundos)
            {
                desenhador.DrawString(font, Tempo.Minutes.ToString() + " : " + Tempo.Seconds.ToString(), PosRelógio, CorRelógio);
            }
            if (modoexibir == ModoExibir.Completo)
            {
                desenhador.DrawString(font, Tempo.ToString(), PosRelógio, CorRelógio);
            }
            if (modoexibir == ModoExibir.segundos)
            {
                desenhador.DrawString(font, Tempo.Seconds.ToString(), PosRelógio, CorRelógio);
            }
            if (modoexibir == ModoExibir.minutos)
            {
                desenhador.DrawString(font, Tempo.Seconds.ToString(), PosRelógio, CorRelógio);
            }
        }
    }
}