using System;
using System.Collections.Generic;
using System.Reflection;
using Peryite.Common.Skyrim;

namespace Peryite.Common
{
    public static class Utils
    {
        /// <summary>
        ///     Executes the action for every item in coll
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="f"></param>
        public static void Do<T>(this IEnumerable<T> coll, Action<T> f)
        {
            foreach (var i in coll) f(i);
        }

        public static bool IsIgnorable<T>(this T t)
        {
            if (t == null)
                return false;

            var attribute = (IgnorableAttribute)t.GetType().GetCustomAttribute(typeof(IgnorableAttribute));

            return attribute != null && attribute.Ignorable;
        }
    }
}
