using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject.GUI.Particles
{
    public abstract class Particle
    {
        // Fields

        // Timing
        private readonly double duration; // in seconds
        private bool isLoopable;
        private double currentTime;

        /// <summary>
        /// Returns the current seek time for this particle.
        /// </summary>
        public double CurrentTime { get { return currentTime; } }

        /// <summary>
        /// Creates a particle with a tint and duration.
        /// </summary>
        /// <param name="duration">A duration in seconds.</param>
        /// <param name="isLoopable">Whether or not this particle will loop forever.</param>
        public Particle(double duration, bool isLoopable)
        {
            this.duration = duration;
            this.isLoopable = isLoopable;
            currentTime = duration;
        }

        /// <summary>
        /// Updates the particles duration, this can be modified if needed.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            currentTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime <= 0)
                if (isLoopable)
                    currentTime = duration;
                else
                    currentTime = 0;
        }

        /// <summary>
        /// Drawing must be created everytime a new particle is made. This is because no particle will have the drawing location or pattern.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
