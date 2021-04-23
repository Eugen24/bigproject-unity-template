using System;

namespace Template.Plugins.MUnityUtils.ExtensionMethods
{
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// Get random value from enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en">Enum</param>
        /// <returns>Random value</returns>
        public static T GetRandomEnum<T>(this T en) where T : struct, IConvertible
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }
    }
}