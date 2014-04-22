using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SubBeatle.Code.Particle
{
    public class ParticleEngine
    {

        private Random random;
        private Texture2D texture;
        public List<Particle> particles;
        public Vector2 EmitterLocation;

        public ParticleEngine(Texture2D texture, Vector2 location)
        {
            this.EmitterLocation = location;
            this.texture = texture;
            this.particles = new List<Particle>();
            this.random = new Random();
        }

        public void Update(Vector2 emitter)
        {
            EmitterLocation = new Vector2(emitter.X, emitter.Y);
            int total = 5;
            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateParticle());
            }
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].TDV <= 0)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        private Particle GenerateParticle()
        {
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                (1f * (float)(random.NextDouble() * 2 - 1)) * 1.5f,
                (1f * (float)(random.NextDouble() * 2 - 1)*1.5f));
            float angle = 0;
            float angleVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble();
            int tdv = 50;

            return new Particle(texture, EmitterLocation, velocity, angle, angleVelocity, size, tdv);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                spriteBatch.Draw(particles[i].Texture,particles[i].Position, particles[i].Source, Color.White, particles[i].Angle, particles[i].Origin, particles[i].Size, SpriteEffects.None, 0f);
            }
        }
    }
}