using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using SubBeatle.BaseClasses;
using SubBeatle.DataBase;

namespace SubBeatle.Management
{
    class GameOver : State
    {
        bool IsRecordBroken;
        
        float PlayerScore;

        List<Player> Players;

        public GameOver(StateManager Father , float Score)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/GameOver");

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Acabou");

                    break;
            }

            # endregion

            PlayerScore = Score;

            IsRecordBroken = false;

            Players = Player.Load().OrderByDescending(x => x.Record).ToList();

            # region Is Record Broken ?

            switch (Players.Count)
            {
                case 0 :

                    IsRecordBroken = true;
                    
                    break;

                case 1 :

                    IsRecordBroken = true;

                    break;

                case 2 :

                    IsRecordBroken = true;

                    break;

                default:

                    for (int i = 0; i < 3; i++)
                    {
                        if (PlayerScore > Players[i].Record)
                        {
                            IsRecordBroken = true;
                            break;
                        }
                    }

                    break;
            }

            # endregion
        }   

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (StateManager.HasAudioControl)
                {
                    MediaPlayer.Stop();
                }

                Manager.GoToMenu();
            }

            # endregion

            if (Manager.Touched)
            {
                if (IsRecordBroken)
                {
                    Manager.GoToSaveRecord(PlayerScore);
                }
                else
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();
                    }

                    Manager.GoToMenu();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage , Vector2.Zero , Color.White);

            Manager.spriteBatch.DrawString(Manager.Font, PlayerScore.ToString() ,
                new Vector2(400 , 330) , Color.White);

            base.Draw(gameTime);
        }
    }
}