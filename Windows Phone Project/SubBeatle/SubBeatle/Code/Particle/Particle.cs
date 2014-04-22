using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SubBeatle.Code.Particle
{
    public class Particle
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;
        public Rectangle Source;
        public float Angle;
        public float AngleVelocity;
        public float Size;
        public int TDV;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angleVelocity, float size, int tdv) 
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Angle = angle;
            this.AngleVelocity = angleVelocity;
            this.Size = size;
            this.TDV = tdv;
        }

        public void Update()
        {
            Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            TDV--;
            Position += Velocity;
            Angle += AngleVelocity;
        }
    }
}