using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace TheChicagoProject
{
    /// <summary>
    /// Rotated rectangle class taken from George W. Clingerman
    /// http://www.xnadevelopment.com/tutorials/rotatedrectanglecollisions/rotatedrectanglecollisions.shtml
    /// </summary>
    public class RotatedRectangle
    {
        public FloatRectangle collisionRectangle;
        public float rotation;
        public Vector2 origin;

        public RotatedRectangle(FloatRectangle theRectangle, float theInitialRotation, Vector2? theOrigin = null)
        {
            collisionRectangle = theRectangle;
            rotation = theInitialRotation;

            //Calculate the Rectangles origin. We assume the center of the Rectangle will
            //be the point that we will be rotating around and we use that for the origin
            if (theOrigin == null)
                origin = new Vector2(theRectangle.Width / 2, theRectangle.Height / 2);
            else
                origin = (Vector2)theOrigin;
        }

        /// <summary>
        /// Used for changing the X and Y position of the RotatedRectangle
        /// </summary>
        /// <param name="theXPositionAdjustment"></param>
        /// <param name="theYPositionAdjustment"></param>
        public void ChangePosition(float theXPositionAdjustment, float theYPositionAdjustment)
        {
            collisionRectangle.X += theXPositionAdjustment;
            collisionRectangle.Y += theYPositionAdjustment;
        }

        /// <summary>
        /// This intersects method can be used to check a standard XNA framework Rectangle
        /// object and see if it collides with a Rotated Rectangle object
        /// </summary>
        /// <param name="theRectangle"></param>
        /// <returns></returns>
        public bool Intersects(Rectangle theRectangle)
        {
            return Intersects(new RotatedRectangle(new FloatRectangle(theRectangle), 0.0f));
        }

        /// <summary>
        /// This intersects method can be used to check a standard XNA framework Rectangle
        /// object and see if it collides with a Rotated Rectangle object
        /// </summary>
        /// <param name="theRectangle"></param>
        /// <returns></returns>
        public bool Intersects(FloatRectangle theRectangle)
        {
            return Intersects(new RotatedRectangle(theRectangle, 0.0f));
        }

        /// <summary>
        /// Check to see if two Rotated Rectangls have collided
        /// </summary>
        /// <param name="theRectangle"></param>
        /// <returns></returns>
        public bool Intersects(RotatedRectangle theRectangle)
        {
            //Calculate the Axis we will use to determine if a collision has occurred
            //Since the objects are rectangles, we only have to generate 4 Axis (2 for
            //each rectangle) since we know the other 2 on a rectangle are parallel.
            List<Vector2> aRectangleAxis = new List<Vector2>();
            aRectangleAxis.Add(UpperRightCorner() - UpperLeftCorner());
            aRectangleAxis.Add(UpperRightCorner() - LowerRightCorner());
            aRectangleAxis.Add(theRectangle.UpperLeftCorner() - theRectangle.LowerLeftCorner());
            aRectangleAxis.Add(theRectangle.UpperLeftCorner() - theRectangle.UpperRightCorner());

            //Cycle through all of the Axis we need to check. If a collision does not occur
            //on ALL of the Axis, then a collision is NOT occurring. We can then exit out 
            //immediately and notify the calling function that no collision was detected. If
            //a collision DOES occur on ALL of the Axis, then there is a collision occurring
            //between the rotated rectangles. We know this to be true by the Seperating Axis Theorem
            foreach (Vector2 aAxis in aRectangleAxis)
            {
                if (!IsAxisCollision(theRectangle, aAxis))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if a collision has occurred on an Axis of one of the
        /// planes parallel to the Rectangle
        /// </summary>
        /// <param name="theRectangle"></param>
        /// <param name="aAxis"></param>
        /// <returns></returns>
        private bool IsAxisCollision(RotatedRectangle theRectangle, Vector2 aAxis)
        {
            //Project the corners of the Rectangle we are checking on to the Axis and
            //get a scalar value of that project we can then use for comparison
            List<float> aRectangleAScalars = new List<float>();
            aRectangleAScalars.Add(GenerateScalar(theRectangle.UpperLeftCorner(), aAxis));
            aRectangleAScalars.Add(GenerateScalar(theRectangle.UpperRightCorner(), aAxis));
            aRectangleAScalars.Add(GenerateScalar(theRectangle.LowerLeftCorner(), aAxis));
            aRectangleAScalars.Add(GenerateScalar(theRectangle.LowerRightCorner(), aAxis));

            //Project the corners of the current Rectangle on to the Axis and
            //get a scalar value of that project we can then use for comparison
            List<float> aRectangleBScalars = new List<float>();
            aRectangleBScalars.Add(GenerateScalar(UpperLeftCorner(), aAxis));
            aRectangleBScalars.Add(GenerateScalar(UpperRightCorner(), aAxis));
            aRectangleBScalars.Add(GenerateScalar(LowerLeftCorner(), aAxis));
            aRectangleBScalars.Add(GenerateScalar(LowerRightCorner(), aAxis));

            //Get the Maximum and Minium Scalar values for each of the Rectangles
            float aRectangleAMinimum = aRectangleAScalars.Min();
            float aRectangleAMaximum = aRectangleAScalars.Max();
            float aRectangleBMinimum = aRectangleBScalars.Min();
            float aRectangleBMaximum = aRectangleBScalars.Max();

            //If we have overlaps between the Rectangles (i.e. Min of B is less than Max of A)
            //then we are detecting a collision between the rectangles on this Axis
            if (aRectangleBMinimum <= aRectangleAMaximum && aRectangleBMaximum >= aRectangleAMaximum)
            {
                return true;
            }
            else if (aRectangleAMinimum <= aRectangleBMaximum && aRectangleAMaximum >= aRectangleBMaximum)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generates a scalar value that can be used to compare where corners of 
        /// a rectangle have been projected onto a particular axis. 
        /// </summary>
        /// <param name="theRectangleCorner"></param>
        /// <param name="theAxis"></param>
        /// <returns></returns>
        private float GenerateScalar(Vector2 theRectangleCorner, Vector2 theAxis)
        {
            //Using the formula for Vector projection. Take the corner being passed in
            //and project it onto the given Axis
            float aNumerator = (theRectangleCorner.X * theAxis.X) + (theRectangleCorner.Y * theAxis.Y);
            float aDenominator = (theAxis.X * theAxis.X) + (theAxis.Y * theAxis.Y);
            float aDivisionResult = aNumerator / aDenominator;
            Vector2 aCornerProjected = new Vector2(aDivisionResult * theAxis.X, aDivisionResult * theAxis.Y);

            //Now that we have our projected Vector, calculate a scalar of that projection
            //that can be used to more easily do comparisons
            float aScalar = (theAxis.X * aCornerProjected.X) + (theAxis.Y * aCornerProjected.Y);
            return aScalar;
        }

        /// <summary>
        /// Rotate a point from a given location and adjust using the Origin we
        /// are rotating around
        /// </summary>
        /// <param name="thePoint"></param>
        /// <param name="theOrigin"></param>
        /// <param name="theRotation"></param>
        /// <returns></returns>
        private Vector2 RotatePoint(Vector2 thePoint, Vector2 theOrigin, float theRotation)
        {
            Vector2 aTranslatedPoint = new Vector2();
            aTranslatedPoint.X = (float)(theOrigin.X + (thePoint.X - theOrigin.X) * Math.Cos(theRotation)
                - (thePoint.Y - theOrigin.Y) * Math.Sin(theRotation));
            aTranslatedPoint.Y = (float)(theOrigin.Y + (thePoint.Y - theOrigin.Y) * Math.Cos(theRotation)
                + (thePoint.X - theOrigin.X) * Math.Sin(theRotation));
            return aTranslatedPoint;
        }

        public Vector2 UpperLeftCorner()
        {
            Vector2 aUpperLeft = new Vector2(collisionRectangle.Left, collisionRectangle.Top);
            aUpperLeft = RotatePoint(aUpperLeft, aUpperLeft + origin, rotation);
            return aUpperLeft;
        }

        public Vector2 UpperRightCorner()
        {
            Vector2 aUpperRight = new Vector2(collisionRectangle.Right, collisionRectangle.Top);
            aUpperRight = RotatePoint(aUpperRight, aUpperRight + new Vector2(-origin.X, origin.Y), rotation);
            return aUpperRight;
        }

        public Vector2 LowerLeftCorner()
        {
            Vector2 aLowerLeft = new Vector2(collisionRectangle.Left, collisionRectangle.Bottom);
            aLowerLeft = RotatePoint(aLowerLeft, aLowerLeft + new Vector2(origin.X, -origin.Y), rotation);
            return aLowerLeft;
        }

        public Vector2 LowerRightCorner()
        {
            Vector2 aLowerRight = new Vector2(collisionRectangle.Right, collisionRectangle.Bottom);
            aLowerRight = RotatePoint(aLowerRight, aLowerRight + new Vector2(-origin.X, -origin.Y), rotation);
            return aLowerRight;
        }

        public float X
        {
            get { return collisionRectangle.X; }
        }

        public float Y
        {
            get { return collisionRectangle.Y; }
        }

        public float Width
        {
            get { return collisionRectangle.Width; }
        }

        public float Height
        {
            get { return collisionRectangle.Height; }
        }


        public int IntX
        {
            get { return collisionRectangle.IntX; }
        }

        public int IntY
        {
            get { return collisionRectangle.IntY; }
        }

        public int IntWidth
        {
            get { return collisionRectangle.IntWidth; }
        }

        public int IntHeight
        {
            get { return collisionRectangle.IntHeight; }
        }
    }
}
