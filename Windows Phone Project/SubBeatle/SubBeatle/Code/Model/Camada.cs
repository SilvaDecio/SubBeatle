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

namespace SubBeatle.Code.Model
{
    public class Camada
    {
        public Texture2D _Imagem;
        public Vector2 _Position;
        private Vector2 _Velocidade;

        public Camada(Texture2D _Img, Vector2 _Pos, Vector2 _Velo)
        {
            this._Imagem = _Img;
            this._Position = _Pos;
            this._Velocidade = _Velo;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_Imagem, _Position, Color.White);
        }
    }
}