using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Douglas Gliner
namespace TheChicagoProject.GUI
{
    /*
     * FOR ENTTIY COLLISION, GET STUFF IN TILE AND CHECK BETWEEN WHATS IN THAT TILE WITH THE PLAYER....
     */


    /// <summary>
    /// Holds information from sprites sheets and handles their animations.
    /// </summary>
    public class Sprite
    {
        // Animation information
        // The SpriteSheet
        private Texture2D texture;

        // Sprite tint
        private Color color;

        // Current frame of the animation
        // frame is also an offset for the sprite sheet
        private int frame;

        // Amount of time that has passed.
        private double timeCounter;

        // FPS of the animation
        private double fps;

        // Amount of time per frame
        private double timePerFrame;

        // Offsets and heights.
        // Number of frames in the animation.
        private int frameCount;

        // How far down to move for the images.
        private int yOffset;

        // Height of a single frame
        private int height;

        // Width of a single frame.
        private int width;

        // File name with extension (blah.png)
        private string fileName;

        // Directory for Sprites.
        public static readonly string Directory = "./Content/Sprites/";


        // Properties
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public int YOffset { get { return yOffset; } }

        public int Height { get { return height; } }

        public int Width { get { return width; } }

        public int Frame { get { return frame; } set { frame = value; } }

        public string FileName { get { return fileName; } }

        public double FPS { get { return fps; } set { fps = value; } }

        public double TimePerFrame { get { return timePerFrame; } }

        public Color Color { get { return color; } set { color = value; } }

        /// <summary>
        /// Basic constructor that draws a static sprite from frame 0
        /// </summary>
        /// <param name="height">Height of the sprite in the sprite sheet.</param>
        /// <param name="width">Width of the sprite in the sprite sheet.</param>
        /// <param name="yOffset">Offset from the top of the sheet.</param>
        /// <param name="fileName">File name of the sprite sheet.</param>
        public Sprite(int height, int width, int yOffset, Color color, string fileName)
        {
            this.height = height;
            this.width = width;
            this.yOffset = yOffset;
            this.fileName = fileName;
            this.color = color;
            frame = 0;
            fps = 0;
            timePerFrame = 0;
            frameCount = 0;
        }

        /// <summary>
        /// Basic constructor that draws a static sprite from a given frame.
        /// </summary>
        /// <param name="height">Height of the sprite in the sprite sheet.</param>
        /// <param name="width">Width of the sprite in the sprite sheet.</param>
        /// <param name="yOffset">Offset from the top of the sheet.</param>
        /// <param name="frame">Frame to present.</param>
        public Sprite(int height, int width, int yOffset, Color color, string fileName, int frame)
        {
            this.height = height;
            this.width = width;
            this.yOffset = yOffset;
            this.frame = frame;
            this.fileName = fileName;
            this.color = color;
            fps = 0;
            timePerFrame = 0;
            frameCount = 0;
        }

        /// <summary>
        /// Basic constructor that starts a sprite animation from a given frame at a given FPS.
        /// </summary>
        /// <param name="height">Height of the sprite in the sprite sheet.</param>
        /// <param name="width">Width of the sprite in the sprite sheet.</param>
        /// <param name="yOffset">Offset from the top of the sheet.</param>
        /// <param name="frame">Frame to start at.</param>
        /// <param name="fps">Frames per second for the animation.</param>
        public Sprite(int height, int width, int yOffset, string fileName, int frame, int fps)
        {
            this.height = height;
            this.width = width;
            this.yOffset = yOffset;
            this.frame = frame;
            this.fps = fps;
            this.fileName = fileName;
            timePerFrame = 1.0 / fps;
            frameCount = 0;
        }


        /// <summary>
        /// Basic frame handling for animations.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Dont run any animation if FPS is 0!
            if (fps == 0)
                return;

            // Handle animation timing
            // - Add to the time counter
            // - Check if we have enough "time" to advance the frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter >= timePerFrame)
            {
                frame += 1;						// Adjust the frame

                if (frame > frameCount)	// Check the bounds
                    frame = 1;					// Back to 1 (since 0 is the "standing" frame)

                timeCounter -= timePerFrame;	// Remove the time we "used"
            }
        }

        /// <summary>
        /// Draw the entire texture as a "sprite".
        /// </summary>
        /// <param name="sb">Game1 SpriteBatch</param>
        /// <param name="x">X location.</param>
        /// <param name="y">Y location.</param>
        public void Draw(SpriteBatch sb, int x, int y)
        {
            sb.Draw(
                texture,					    // - The texture to draw
                new Rectangle(x, y, texture.Width, texture.Height),		// - The location to draw on the screen
                color					// - The color
            );								// - Layer depth (unused)
        }


        /// <summary>
        /// Draw the entire texture as a "sprite".
        /// </summary>
        /// <param name="sb">Game1 SpriteBatch</param>
        /// <param name="x">X location.</param>
        /// <param name="y">Y location.</param>
        /// <param name="dir">Direction of the sprite to face.</param>
        public void Draw(SpriteBatch sb, int x, int y, Direction dir)
        {
            
            sb.Draw(
                 texture,					    // - The texture to draw
                 new Rectangle(x + (width / 2), y + (height / 2), width, height),		// - The location to draw on the screen
                 new Rectangle(					// - The "source" rectangle
                     frame * width,	                    //   - This rectangle specifies
                     yOffset,		            //	   where "inside" the texture
                     width,			            //     to get pixels (We don't want to
                     height),		        	//     draw the whole thing)
                 color,					// - The color
                 DirectionToRadians(dir),								// - Rotation (none currently)
                 new Vector2(width / 2, height / 2),					// - Origin inside the image (top left)
                 SpriteEffects.None,				        // - Can be used to flip the image
                 0);								// - Layer depth (unused)
          
            /*
            #region debug
            
            sb.Draw(
                texture,					    // - The texture to draw
                new Rectangle(x, y, texture.Width, texture.Height),		// - The location to draw on the screen
                Color.White					// - The color
            );
             
            #endregion
            */
           
        }

        public void Draw(SpriteBatch sb, int x, int y, float dir, Color color)
        {

            sb.Draw(
                 texture,					    // - The texture to draw
                 new Rectangle(x + (width / 2), y + (height / 2), width, height),		// - The location to draw on the screen
                 new Rectangle(					// - The "source" rectangle
                     frame * width,	                    //   - This rectangle specifies
                     yOffset,		            //	   where "inside" the texture
                     width,			            //     to get pixels (We don't want to
                     height),		        	//     draw the whole thing)
                 color,					// - The color
                 dir,								// - Rotation (none currently)
                 new Vector2(width / 2, height / 2),					// - Origin inside the image (top left)
                 SpriteEffects.None,				        // - Can be used to flip the image
                 0);								// - Layer depth (unused)

            /*
            #region debug
            
            sb.Draw(
                texture,					    // - The texture to draw
                new Rectangle(x, y, texture.Width, texture.Height),		// - The location to draw on the screen
                Color.White					// - The color
            );
             
            #endregion
            */

        }

        /// <summary>
        /// Draw a sprite without rotation.
        /// </summary>
        /// <param name="sb">Game1 SpriteBatch</param>
        /// <param name="x">X location.</param>
        /// <param name="y">Y location.</param>
        /// <param name="w">Desired width.</param>
        /// <param name="h">Desired height.</param>
        public void Draw(SpriteBatch sb, int x, int y, int w, int h)
        {
            sb.Draw(
                texture,					    // - The texture to draw
                new Rectangle(x, y, w, h),		// - The location to draw on the screen
                new Rectangle(					// - The "source" rectangle
                    frame * width,	                    //   - This rectangle specifies
                    yOffset,		            //	   where "inside" the texture
                    width,			            //     to get pixels (We don't want to
                    height),		        	//     draw the whole thing)
                color,					// - The color
                0,								// - Rotation (none currently)
                Vector2.Zero,					// - Origin inside the image (top left)
                SpriteEffects.None,				        // - Can be used to flip the image
                0);								// - Layer depth (unused)
        }

        /// <summary>
        /// Draw a sprite with rotation.
        /// </summary>
        /// <param name="sb">Game1 SpriteBatch</param>
        /// <param name="x">X location.</param>
        /// <param name="y">Y location.</param>
        /// <param name="w">Desired width.</param>
        /// <param name="h">Desired height.</param>
        /// <param name="dir">A direction from the direction enum.</param>
        public void Draw(SpriteBatch sb, int x, int y, int w, int h, Direction dir)
        {
            sb.Draw(
                texture,					    // - The texture to draw
                new Rectangle(x, y, w, h),		// - The location to draw on the screen
                new Rectangle(					// - The "source" rectangle
                    frame * width,	                    //   - This rectangle specifies
                    yOffset,		            //	   where "inside" the texture
                    width,			            //     to get pixels (We don't want to
                    height),		        	//     draw the whole thing)
                color,					// - The color
                DirectionToRadians(dir),								// - Rotation (none currently)
                Vector2.Zero,					// - Origin inside the image (top left)
                SpriteEffects.None,				        // - Can be used to flip the image
                0);								// - Layer depth (unused)

           
        }

        /// <summary>
        /// Converts given direction into radians for ease of use
        /// by the draw function.
        /// </summary>
        /// <param name="d">A direction.</param>
        /// <returns></returns>
        public float DirectionToRadians(Direction d)
        {
            switch(d)
            {
                case Direction.Up:
                    return (float)System.Math.PI / 2;
                case Direction.UpRight:
                    return (float)System.Math.PI / 4;
                case Direction.Right:
                    return 0;
                case Direction.DownRight:
                    return (float)(7 * System.Math.PI) / 4;
                case Direction.Down:
                    return (float)(3 * System.Math.PI) / 2;
                case Direction.DownLeft:
                    return (float)(5 * System.Math.PI) / 4;
                case Direction.Left:
                    return (float)System.Math.PI;
                case Direction.UpLeft:
                    return (float)(3 * System.Math.PI) / 4;
            }

            // Greater than 2 pi, shows that there was a major error!!
            return 10;
        }
    }
}
