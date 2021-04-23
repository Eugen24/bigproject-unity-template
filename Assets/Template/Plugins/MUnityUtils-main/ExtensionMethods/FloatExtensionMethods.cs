using UnityEngine;

namespace Template.Plugins.MUnityUtils.ExtensionMethods
{
    public static class FloatExtensionMethods
    {
        /// <summary>
        /// Round to int
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int RoundToInt(this float f)
        {
            return Mathf.RoundToInt(f);
        }

        /// <summary>
        /// Clamp this float within a min and max value
        /// </summary>
        /// <param name="f"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(this float f, float min, float max)
        {
            return Mathf.Clamp(f, min, max);
        }

        /// <summary>
        /// Clamp this float within 0 and 1
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float Clamp01(this float f)
        {
            return Mathf.Clamp01(f);
        }

        /// <summary>
        /// Get the absolute value of this float
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float Abs(this float f)
        {
            return Mathf.Abs(f);
        }

        /// <summary>
        /// Is floatA equal to zero? Takes floating point inaccuracy into account, by using Epsilon.
        /// </summary>
        /// <param name="floatA"></param>
        /// <returns></returns>
        public static bool IsEqualToZero(this float floatA)
        {
            return Mathf.Abs(floatA) < Mathf.Epsilon;
        }

        /// <summary>
        /// Is floatA not equal to zero? Takes floating point inaccuracy into account, by using Epsilon.
        /// </summary>
        /// <param name="floatA"></param>
        /// <returns></returns>
        public static bool NotEqualToZero(this float floatA)
        {
            return Mathf.Abs(floatA) > Mathf.Epsilon;
        }

        /// <summary>
        /// Wraps a float between -180 and 180.
        /// </summary>
        /// <param name="toWrap">The float to wrap.</param>
        /// <returns>A value between -180 and 180.</returns>
        public static float Wrap180(this float toWrap)
        {
            toWrap %= 360.0f;
            if (toWrap < -180.0f)
            {
                toWrap += 360.0f;
            }
            else if (toWrap > 180.0f)
            {
                toWrap -= 360.0f;
            }

            return toWrap;
        }

        /// <summary>
        /// Wraps a float between 0 and 1.
        /// </summary>
        /// <param name="toWrap">The float to wrap.</param>
        /// <returns>A value between 0 and 1.</returns>
        public static float Wrap1(this float toWrap)
        {
            toWrap %= 1.0f;
            if (toWrap < 0.0f)
            {
                toWrap += 1.0f;
            }

            return toWrap;
        }

        /// <summary>
        /// Gets the fraction portion of a float.
        /// </summary>
        /// <param name="number">The float.</param>
        /// <returns>The fraction portion of a float.</returns>
        public static float GetFraction(this float number)
        {
            return number - Mathf.Floor(number);
        }
    }
}
