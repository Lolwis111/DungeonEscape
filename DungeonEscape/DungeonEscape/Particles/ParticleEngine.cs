using Microsoft.Xna.Framework;
using DungeonEscape.Models;
using DungeonEscape.Content;
using System.Collections.Generic;

namespace DungeonEscape.Particles
{
    internal sealed class ParticleEngine
    {
        private const int ParticleLimit = 256;
        private const float StandardSize = 0.5f;
        private const float Lifetime = 1.0f;

        private Vector3 _centerPosition;

        private List<Particle> _particles = new List<Particle>(ParticleLimit);

        public void Restart(Vector3 position)
        {
            _centerPosition = position;

            for (int i = 0; i < ParticleLimit; i++)
                _particles.Add(CreateParticle());
        }

        public void Update()
        {
            foreach(Particle particle in _particles)
            { 
                if (particle.LifeTime < 0.0f)
                {
                    particle.IsAlive = false;

                    continue;
                }

                particle.Size -= 0.0001f;
                particle.LifeTime -= particle.Decay;
            }
        }

        public void Render()
        {
            foreach (Particle p in _particles)
            {
                if (!p.IsAlive) continue;
                
                Effects.MainEffect.Parameters["DiffuseTexture"].SetValue(Textures.Particle);
                VertexModel.ParticleVertexModel.Draw(Matrix.CreateScale(p.Size) * Matrix.CreateTranslation(p.Position));
            }
        }

        private Particle CreateParticle()
        {
            return new Particle
            {
                LifeTime = (Basic.Random.Next(1, 100) + 1) / 100f,
                Decay = 0.05f,
                Position = new Vector3
                {
                    X = (float)(Basic.Random.NextDouble() * 0.9f) - 0.45f,
                    Y = (float)(Basic.Random.NextDouble() * 0.9f) - 0.45f,
                    Z = (float)(Basic.Random.NextDouble() * 0.9f) - 0.45f,
                } + _centerPosition,
                Size = StandardSize,
                IsAlive = true
            };
        }
    }
}
