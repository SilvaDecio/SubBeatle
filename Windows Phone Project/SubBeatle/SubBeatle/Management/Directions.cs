using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using SubBeatle.BaseClasses;
using SubBeatle.DataBase;

namespace SubBeatle.Management
{
    class Directions : State
    {
        Vector2 Position;

        public Directions(StateManager Father)
        {
            Manager = Father;

            Position = new Vector2();

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Directions");

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Instrucoes");

                    break;
            }

            # endregion
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.GoToMenu();
            }

            # endregion

            if (Manager.VerticalDrag)
            {
                Position.Y += Manager.CurrentGesture.Delta.Y;
            }
            else
            {
                Position.Y -= 0.5f;
            }

            Position.Y = MathHelper.Clamp(Position.Y, Manager.Game.GraphicsDevice.Viewport.Height - BackGroundImage.Height, 0);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Position, Color.White);

            base.Draw(gameTime);
        }
    }
}