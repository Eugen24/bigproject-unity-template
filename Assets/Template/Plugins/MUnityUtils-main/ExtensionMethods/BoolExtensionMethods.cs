namespace MUnityUtils.ExtensionMethods
{
    public static class BoolExtensionMethods
    {
        /// <summary>
        /// Inverts the bool
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Invert(this bool b)
        {
            return !b;
        }

        /// <summary>
        /// Converts the bool to an int
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }
    }
}
