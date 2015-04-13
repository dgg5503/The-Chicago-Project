using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheChicagoProject.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace TheChicagoProject.GUI
{
    // Douglas Gliner (usually stuff is taken from stackoverflow / other projects of mine and others)
    static class Utils
    {
        public static Random rand = new Random();

        public static Texture2D GenRandColorTexture(GraphicsDevice g, int width, int height)
        {
            Texture2D texture = new Texture2D(g, width, height);
            Color[] colors = new Color[texture.Height * texture.Width];

            Color randomColor = new Color(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256), 100);

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = randomColor;
            }
            texture.SetData<Color>(colors);

            return texture;
        }

        public static Texture2D GenColorTexture(GraphicsDevice g, int width, int height, Color color)
        {

            Texture2D texture = new Texture2D(g, width, height);
            Color[] colors = new Color[texture.Height * texture.Width];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }
            texture.SetData<Color>(colors);

            return texture;
        }

        //http://stackoverflow.com/questions/13893959/how-to-draw-the-border-of-a-square
        // EXTENSION METHODS :OOOO!
        public static void CreateBorder(this Texture2D texture, int borderWidth, Color borderColor)
        {
            Color[] colors = new Color[texture.Width * texture.Height];

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    bool colored = false;
                    for (int i = 0; i <= borderWidth; i++)
                    {
                        if (x == i || y == i || x == texture.Width - 1 - i || y == texture.Height - 1 - i)
                        {
                            colors[x + y * texture.Width] = borderColor;
                            colored = true;
                            break;
                        }
                    }

                    if (colored == false)
                        colors[x + y * texture.Width] = Color.Transparent;
                }
            }

            texture.SetData(colors);
        }


        /// <summary>
        /// Project vector A onto vecotr B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 Project(Vector2 a, Vector2 b)
        {
            return Vector2.Multiply(b, Vector2.Dot(a, b) / b.LengthSquared());
        }
    }
}
