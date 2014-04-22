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

namespace SubBeatle.Model
{
    class GameObject
    {
        #region atributos

        public Texture2D _Image;
        public Vector2 _Posicao;

        #endregion

        public GameObject(Texture2D _Text, Vector2 _Pos)
        {
            this._Image = _Text;
            this._Posicao = _Pos;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch _Sprite)
        {
            _Sprite.Draw(_Image, _Posicao, Color.White);
        }
    }
}