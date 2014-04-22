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

using SubBeatle.Management;
using SubBeatle.Code.Sistema;

namespace SubBeatle.Code.Objetos
{
    enum TipoObj
    {
        Lata, porca, Parafuso,
        Maca, Peixe,
        Tomada, Chinelo,copo,Peixe2,
        perfume,Garrafa,GarrafaVidro,
        Janela,Tubarao,banco,porta
    }

    class GerenciadorObjetosColetaveis
    {
        private const int chanceMetal = 350;
        private const int chanceOrganico = 700;
        private const int chancePlastico = 800;
        private const int chanceVidro = 1000;

        private Cronometro.Relógio relogio;
        private Random randomGenerator;
        
        private long tempoCriar;
        private ContentManager Content;
        private List<ObjetoColetavel> meusInimigos;

        private Dictionary<TipoObj, Texture2D> texturasObjetos;

        Personagem.Personagem personagem;

        private SoundEffect Engolir;

        public List<Placar> Pontos;

        public GerenciadorObjetosColetaveis(ContentManager content)
        {
            Content = content;
            randomGenerator = new Random(DateTime.Now.Millisecond);
            relogio = new Cronometro.Relógio(Cronometro.ModoCronômetro.Progressivo, Cronometro.ModoExibir.segundos);
            relogio.Inicar();
            this.meusInimigos = new List<ObjetoColetavel>();
            this.tempoCriar = 700;
            meusInimigos = new List<ObjetoColetavel>();
            texturasObjetos = new Dictionary<TipoObj, Texture2D>();
            CarregarTexturas(content);
            CarregarSoundEffects(content);

            Pontos = new List<Placar>();
        }

        private void CarregarSoundEffects(ContentManager content)
        {
            Engolir = content.Load<SoundEffect>("Audio/SoundEffects/Clique_Engolir");
        }

        public void Limpar()
        {
            meusInimigos.Clear();
        }

        private void CarregarTexturas(ContentManager content)
        {
            this.CarregarObjetosNivel1(content);
            this.CarregarObjetosNivel2(content);
            this.CarregarObjetosNivel3(content);
        }

        private void CarregarObjetosNivel1(ContentManager content)
        {
            texturasObjetos.Add(TipoObj.copo,content.Load<Texture2D>(@"Source//inimigos//Fase1//copo - fase 1 - vidro"));
            texturasObjetos.Add(TipoObj.Maca, content.Load<Texture2D>(@"Source//inimigos//Fase1//maca - fase 1 - organico"));
            texturasObjetos.Add(TipoObj.Peixe, content.Load<Texture2D>(@"Source//inimigos//Fase1//peixe 1 - fase 1 - organico"));
            texturasObjetos.Add(TipoObj.Tomada, content.Load<Texture2D>(@"Source//inimigos//Fase1//tomada - fase 1 - plastico"));
            texturasObjetos.Add(TipoObj.Chinelo, content.Load<Texture2D>(@"Source//inimigos//Fase1//chinelo - fase 1 - plastico"));
            texturasObjetos.Add(TipoObj.porca, content.Load<Texture2D>(@"Source//inimigos//Fase1//forca - fase 1 - metal"));
            texturasObjetos.Add(TipoObj.Parafuso, content.Load<Texture2D>(@"Source//inimigos\Fase1//parafuso - fase 1 - metal")); 
        }

        private void CarregarObjetosNivel2(ContentManager content)
        {
            texturasObjetos.Add(TipoObj.perfume, content.Load<Texture2D>(@"Source//inimigos//Fase2//perfume - fase 2 - vidro"));
            texturasObjetos.Add(TipoObj.GarrafaVidro, content.Load<Texture2D>(@"Source//inimigos//Fase2//garrafa - fase 2 - vidro"));
            texturasObjetos.Add(TipoObj.Peixe2, content.Load<Texture2D>(@"Source//inimigos//Fase2//peixe 2 - fase 2 - organico"));
            texturasObjetos.Add(TipoObj.Garrafa, content.Load<Texture2D>(@"Source//inimigos//Fase2//garrafa - fase 2 - plastico"));
            texturasObjetos.Add(TipoObj.Lata, content.Load<Texture2D>(@"Source//inimigos\Fase2//lata - fase 2 - metal"));
        }

        private void CarregarObjetosNivel3(ContentManager content)
        {
            texturasObjetos.Add(TipoObj.Janela, content.Load<Texture2D>(@"Source//inimigos//Fase3//janela - fase 3 - vidro"));
            texturasObjetos.Add(TipoObj.Tubarao, content.Load<Texture2D>(@"Source//inimigos//Fase3//espinha de tubarao - fase 3 - organico"));
            texturasObjetos.Add(TipoObj.banco, content.Load<Texture2D>(@"Source//inimigos//Fase3//banco - fase 3 - plastico"));
            texturasObjetos.Add(TipoObj.porta, content.Load<Texture2D>(@"Source//inimigos//Fase3//porta - fase 3 - metal"));
        }
        
        public void Update(GameTime gameTime)
        {
            relogio.Atualizar(gameTime);

            if (relogio.GetTimerNow().Milliseconds >= tempoCriar)
            {
                 meusInimigos.Add(CriarObjeto());
                 relogio.Reiniciar(gameTime);
            }

            for (int i = 0; i < meusInimigos.Count; i++)
            {
                meusInimigos[i].Update(gameTime);
            }

            for (int i = 0; i < Pontos.Count; i++)
            {
                Pontos[i].Update(gameTime);

                if (Pontos[i].Chegou)
                {
                    Pontos.RemoveAt(i);
                }
            }
        }

        public void UpdatePersonagem(Vector2 velPersonagem)
        {
            for (int i = 0; i < meusInimigos.Count; i++)
            {
                meusInimigos[i].UpdatePersonagem(velPersonagem);
            }
        }

        public void Draw(SpriteBatch spritebatch , SpriteFont Font)
        {
            for (int i = 0; i < meusInimigos.Count; i++)
            {
                meusInimigos[i].Draw(spritebatch);
            }            
        }

        public void ChecarPersonagem(ref Personagem.Personagem player)
        {
            this.personagem = player;

            for (int c = 0; c < meusInimigos.Count; c++)
            {
                if (player.intersects(meusInimigos[c].rect))
                {
                    meusInimigos[c].Coletado(ref player);

                    if (meusInimigos[c].GetType() == typeof(ObjetoVidro))
                    {
                        Pontos.Add(new Placar(player.Position));
                    }

                    meusInimigos.RemoveAt(c);
                    
                    if (StateManager.HasAudioControl)
                    {
                        Engolir.Play();
                    }
                }
            }
        }

        public ObjetoColetavel CriarObjeto()
        {
            int sorteio;
            int sorteioPosicaoX, sorteioPosicaoY;

            sorteio = randomGenerator.Next(0, 1000);
            sorteioPosicaoX = randomGenerator.Next(15, 1260);
            sorteioPosicaoY = randomGenerator.Next(15, 700);

            Vector2 posSorteada = new Vector2(sorteioPosicaoX, sorteioPosicaoY);

            int flipHorizontal = randomGenerator.Next(0, 1);
            int flipVertical = randomGenerator.Next(0, 1);

            Vector2 velSorteada = new Vector2(
                (float)randomGenerator.NextDouble(),
                (float)randomGenerator.NextDouble());

            if (flipHorizontal == 1)
                velSorteada.X *= -1;

            if (flipVertical == 1)
                velSorteada.Y *= -1;

            ObjetoColetavel obj = null;

            int tipoObj;

		    if (sorteio <= chanceMetal)
            {
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level1)
                {
                    tipoObj = randomGenerator.Next(2);
                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoMetalico(texturasObjetos[TipoObj.porca], posSorteada, velSorteada, 10);
                            break;

                        case 1:
                            obj = new ObjetoMetalico(texturasObjetos[TipoObj.Parafuso], posSorteada, velSorteada, 10);
                            break;
                    }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level2)
                    {
                        tipoObj = randomGenerator.Next(1);
                        switch (tipoObj)
                        {
                            case 0:
                                obj = new ObjetoMetalico(texturasObjetos[TipoObj.Lata], posSorteada, velSorteada, 15);
                                break;
                         }
                    }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level3)
                    {
                        tipoObj = randomGenerator.Next(1);
                        switch (tipoObj)
                        {
                            case 0:
                                obj = new ObjetoMetalico(texturasObjetos[TipoObj.porta], posSorteada, velSorteada, 15);
                                break;
                         }
                    }
            } 

            else if (sorteio <= chanceOrganico)
            {
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level1)
                {
                    tipoObj = randomGenerator.Next(1,3);

                    switch (tipoObj)
                    {
                        //case 0:
                        //    obj = new ObjetoOrganico(texturasObjetos[TipoObj.Coco], posSorteada, velSorteada);
                        //    break;

                        case 1:
                            obj = new ObjetoOrganico(texturasObjetos[TipoObj.Maca], posSorteada, velSorteada);
                            break;

                        case 2:
                            obj = new ObjetoOrganico(texturasObjetos[TipoObj.Peixe], posSorteada, velSorteada);
                            break;
                    }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level2)
                {
                    tipoObj = randomGenerator.Next(1);

                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoOrganico(texturasObjetos[TipoObj.Peixe2], posSorteada, velSorteada);
                            break;
                    }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level3)
                {
                    tipoObj = randomGenerator.Next(1);

                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoOrganico(texturasObjetos[TipoObj.Tubarao], posSorteada, velSorteada);
                            break;                            
                    }
                }
            }

            else if (sorteio <= chancePlastico)
            {
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level1)
                {
                tipoObj = randomGenerator.Next(2);
                switch (tipoObj)
                {
                    case 0:
                        obj = new ObjetoPlastico(texturasObjetos[TipoObj.Tomada], posSorteada, velSorteada, 5);
                        break;

                    case 1:
                        obj = new ObjetoPlastico(texturasObjetos[TipoObj.Chinelo], posSorteada, velSorteada, 5);
                        break;                  
                }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level2)
                {
                    tipoObj = randomGenerator.Next(1);
                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoPlastico(texturasObjetos[TipoObj.Garrafa], posSorteada, velSorteada, 5);
                            break;
                    }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level3)
                {
                     tipoObj = randomGenerator.Next(1);
                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoPlastico(texturasObjetos[TipoObj.banco], posSorteada, velSorteada, 5);
                            break;
                    }
                }
            }

            else if (sorteio <= chanceVidro)
            {

                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level1)
                {
                    tipoObj = randomGenerator.Next(1);
                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoVidro(texturasObjetos[TipoObj.copo], posSorteada, velSorteada);
                            break;                        
                    }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level2)
                {
                    tipoObj = randomGenerator.Next(2);
                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoVidro(texturasObjetos[TipoObj.perfume], posSorteada, velSorteada);
                            break;
                        case 1:
                            obj = new ObjetoVidro(texturasObjetos[TipoObj.GarrafaVidro], posSorteada, velSorteada);
                            break;
                    }
                }
                if (this.personagem.LevelAtual == Personagem.Personagem.LevelPersonagem.level3)
                {
                    tipoObj = randomGenerator.Next(1);
                    switch (tipoObj)
                    {
                        case 0:
                            obj = new ObjetoVidro(texturasObjetos[TipoObj.Janela], posSorteada, velSorteada);
                            break;
                    }
                }
            }
            if (obj == null)
            {
                String x = "uou";
            }
            return obj;
        }
    }
}