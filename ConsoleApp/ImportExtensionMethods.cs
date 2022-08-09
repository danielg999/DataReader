using System;

namespace ConsoleApp
{
    /// <summary>
    /// Class to extension methods
    /// </summary>
    public static class ImportExtensionMethods
    {
        /// <summary>
        /// Formatting property imported object
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatPropertyImportedObject(this string str)
        {
            return str.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
    }
}
