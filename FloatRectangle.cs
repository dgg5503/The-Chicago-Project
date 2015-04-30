using System;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheChicagoProject
{
    /// <summary>
    /// Describes a 2D-FloatRectangle. 
    /// </summary>
    public struct FloatRectangle : IEquatable<FloatRectangle>
    {
        #region Private Fields

        private static FloatRectangle emptyFloatRectangle = new FloatRectangle();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of the top-left corner of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float X;

        /// <summary>
        /// The y coordinate of the top-left corner of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Y;

        /// <summary>
        /// The x coordinate of the top-left corner of this <see cref="FloatRectangle"/>.
        /// </summary>
        public int IntX { get { return (int)X; } }

        /// <summary>
        /// The y coordinate of the top-left corner of this <see cref="FloatRectangle"/>.
        /// </summary>
        public int IntY { get { return (int)Y; } }

        /// <summary>
        /// The width of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Width;

        /// <summary>
        /// The height of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Height;

        /// <summary>
        /// The width of this <see cref="FloatRectangle"/>.
        /// </summary>
        public int IntWidth { get { return (int)Width; } }

        /// <summary>
        /// The height of this <see cref="FloatRectangle"/>.
        /// </summary>
        public int IntHeight { get { return (int)Height; } }


        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="FloatRectangle"/> with X=0, Y=0, Width=0, Height=0.
        /// </summary>
        public static FloatRectangle Empty
        {
            get { return emptyFloatRectangle; }
        }

        /// <summary>
        /// Returns the x coordinate of the left edge of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Left
        {
            get { return this.X; }
        }

        /// <summary>
        /// Returns the x coordinate of the right edge of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Right
        {
            get { return (this.X + this.Width); }
        }

        /// <summary>
        /// Returns the y coordinate of the top edge of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Top
        {
            get { return this.Y; }
        }

        /// <summary>
        /// Returns the y coordinate of the bottom edge of this <see cref="FloatRectangle"/>.
        /// </summary>
        public float Bottom
        {
            get { return (this.Y + this.Height); }
        }

        /// <summary>
        /// Whether or not this <see cref="FloatRectangle"/> has a <see cref="Width"/> and
        /// <see cref="Height"/> of 0, and a <see cref="Location"/> of (0, 0).
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return ((((this.Width == 0) && (this.Height == 0)) && (this.X == 0)) && (this.Y == 0));
            }
        }

        /// <summary>
        /// The top-left coordinates of this <see cref="FloatRectangle"/>.
        /// </summary>
        public Vector2 Location
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The width-height coordinates of this <see cref="FloatRectangle"/>.
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(this.Width, this.Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// A <see cref="Point"/> located in the center of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="Width"/> or <see cref="Height"/> is an odd number,
        /// the center point will be rounded down.
        /// </remarks>
        public Vector2 Center
        {
            get
            {
                return new Vector2(this.X + (this.Width / 2), this.Y + (this.Height / 2));
            }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X, "  ",
                    this.Y, "  ",
                    this.Width, "  ",
                    this.Height
                    );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="FloatRectangle"/> struct, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="FloatRectangle"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="FloatRectangle"/>.</param>
        /// <param name="width">The width of the created <see cref="FloatRectangle"/>.</param>
        /// <param name="height">The height of the created <see cref="FloatRectangle"/>.</param>
        public FloatRectangle(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Creates a new instance of <see cref="FloatRectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="location">The x and y coordinates of the top-left corner of the created <see cref="FloatRectangle"/>.</param>
        /// <param name="size">The width and height of the created <see cref="FloatRectangle"/>.</param>
        public FloatRectangle(Vector2 location, Vector2 size)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Width = size.X;
            this.Height = size.Y;
        }

        /// <summary>
        /// Creates a new instance of <see cref="FloatRectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="rectangle">The integer rectangle to convert to float rectangle.<see cref="FloatRectangle"/>.</param>
        public FloatRectangle(Rectangle rectangle)
        {
            this.X = rectangle.X;
            this.Y = rectangle.Y;
            this.Width = rectangle.X;
            this.Height = rectangle.Y;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares whether two <see cref="FloatRectangle"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="FloatRectangle"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="FloatRectangle"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(FloatRectangle a, FloatRectangle b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        /// <summary>
        /// Compares whether two <see cref="FloatRectangle"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="FloatRectangle"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="FloatRectangle"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(FloatRectangle a, FloatRectangle b)
        {
            return !(a == b);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(int x, int y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided coordinates lie within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the point to check for containment.</param>
        /// <param name="y">The y coordinate of the point to check for containment.</param>
        /// <returns><c>true</c> if the provided coordinates lie inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(float x, float y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FloatRectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Point value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Point"/> lies within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FloatRectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Point"/> lies inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref Point value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FloatRectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(Vector2 value)
        {
            return ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="Vector2"/> lies within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">The coordinates to check for inclusion in this <see cref="FloatRectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="Vector2"/> lies inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref Vector2 value, out bool result)
        {
            result = ((((this.X <= value.X) && (value.X < (this.X + this.Width))) && (this.Y <= value.Y)) && (value.Y < (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FloatRectangle"/> lies within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">The <see cref="FloatRectangle"/> to check for inclusion in this <see cref="FloatRectangle"/>.</param>
        /// <returns><c>true</c> if the provided <see cref="FloatRectangle"/>'s bounds lie entirely inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Contains(FloatRectangle value)
        {
            return ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Gets whether or not the provided <see cref="FloatRectangle"/> lies within the bounds of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">The <see cref="FloatRectangle"/> to check for inclusion in this <see cref="FloatRectangle"/>.</param>
        /// <param name="result"><c>true</c> if the provided <see cref="FloatRectangle"/>'s bounds lie entirely inside this <see cref="FloatRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Contains(ref FloatRectangle value, out bool result)
        {
            result = ((((this.X <= value.X) && ((value.X + value.Width) <= (this.X + this.Width))) && (this.Y <= value.Y)) && ((value.Y + value.Height) <= (this.Y + this.Height)));
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is FloatRectangle) && this == ((FloatRectangle)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="other">The <see cref="FloatRectangle"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(FloatRectangle other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="FloatRectangle"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        /// <summary>
        /// Adjusts the edges of this <see cref="FloatRectangle"/> by specified horizontal and vertical amounts. 
        /// </summary>
        /// <param name="horizontalAmount">Value to adjust the left and right edges.</param>
        /// <param name="verticalAmount">Value to adjust the top and bottom edges.</param>
        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            X -= (int)horizontalAmount;
            Y -= (int)verticalAmount;
            Width += (int)horizontalAmount * 2;
            Height += (int)verticalAmount * 2;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="FloatRectangle"/> intersects with this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">Other <see cref="FloatRectangle"/>.</param>
        /// <returns><c>true</c> if other <see cref="FloatRectangle"/> intersects with this <see cref="FloatRectangle"/>; <c>false</c> otherwise.</returns>
        public bool Intersects(FloatRectangle value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }


        /// <summary>
        /// Gets whether or not a specified <see cref="FloatRectangle"/> intersects with this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="value">Other <see cref="FloatRectangle"/>.</param>
        /// <param name="result"><c>true</c> if other <see cref="FloatRectangle"/> intersects with this <see cref="FloatRectangle"/>; <c>false</c> otherwise. As an output parameter.</param>
        public void Intersects(ref FloatRectangle value, out bool result)
        {
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Creates a new <see cref="FloatRectangle"/> that contains overlapping region of two other FloatRectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FloatRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FloatRectangle"/>.</param>
        /// <returns>Overlapping region of the two FloatRectangles.</returns>
        public static FloatRectangle Intersect(FloatRectangle value1, FloatRectangle value2)
        {
            FloatRectangle FloatRectangle;
            Intersect(ref value1, ref value2, out FloatRectangle);
            return FloatRectangle;
        }

        /// <summary>
        /// Creates a new <see cref="FloatRectangle"/> that contains overlapping region of two other FloatRectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FloatRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FloatRectangle"/>.</param>
        /// <param name="result">Overlapping region of the two FloatRectangles as an output parameter.</param>
        public static void Intersect(ref FloatRectangle value1, ref FloatRectangle value2, out FloatRectangle result)
        {
            if (value1.Intersects(value2))
            {
                float right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
                float left_side = Math.Max(value1.X, value2.X);
                float top_side = Math.Max(value1.Y, value2.Y);
                float bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
                result = new FloatRectangle(left_side, top_side, right_side - left_side, bottom_side - top_side);
            }
            else
            {
                result = new FloatRectangle(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="FloatRectangle"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="FloatRectangle"/>.</param>
        public void Offset(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="offsetX">The x coordinate to add to this <see cref="FloatRectangle"/>.</param>
        /// <param name="offsetY">The y coordinate to add to this <see cref="FloatRectangle"/>.</param>
        public void Offset(float offsetX, float offsetY)
        {
            X += (int)offsetX;
            Y += (int)offsetY;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="FloatRectangle"/>.</param>
        public void Offset(Point amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        /// <summary>
        /// Changes the <see cref="Location"/> of this <see cref="FloatRectangle"/>.
        /// </summary>
        /// <param name="amount">The x and y components to add to this <see cref="FloatRectangle"/>.</param>
        public void Offset(Vector2 amount)
        {
            X += (int)amount.X;
            Y += (int)amount.Y;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="FloatRectangle"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Width:[<see cref="Width"/>] Height:[<see cref="Height"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="FloatRectangle"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Width:" + Width + " Height:" + Height + "}";
        }

        /// <summary>
        /// Creates a new <see cref="FloatRectangle"/> that completely contains two other FloatRectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FloatRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FloatRectangle"/>.</param>
        /// <returns>The union of the two FloatRectangles.</returns>
        public static FloatRectangle Union(FloatRectangle value1, FloatRectangle value2)
        {
            float x = Math.Min(value1.X, value2.X);
            float y = Math.Min(value1.Y, value2.Y);
            return new FloatRectangle(x, y,
                                 Math.Max(value1.Right, value2.Right) - x,
                                     Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="FloatRectangle"/> that completely contains two other FloatRectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="FloatRectangle"/>.</param>
        /// <param name="value2">The second <see cref="FloatRectangle"/>.</param>
        /// <param name="result">The union of the two FloatRectangles as an output parameter.</param>
        /// 
        /*
        public static void Union(ref FloatRectangle value1, ref FloatRectangle value2, out FloatRectangle result)
        {
            result.X = Math.Min(value1.X, value2.X);
            result.Y = Math.Min(value1.Y, value2.Y);
            result.Width = Math.Max(value1.Right, value2.Right) - result.X;
            result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
        }
         * */

        public static implicit operator Rectangle(FloatRectangle floatRec)
        {
            return new Rectangle((int)floatRec.X, (int)floatRec.Y, (int)floatRec.Width, (int)floatRec.Height);
        }

        #endregion
    }

}
