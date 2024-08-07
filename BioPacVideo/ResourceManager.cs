using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace BioPacVideo
{
    internal class ResourceManager
    {
        /// <summary>
        /// Extracts an embedded DLL resource and returns it as a byte array
        /// </summary>
        /// <param name="resourceName">The name and path for the embedded resource eg BioPacVideo.Resources.dll.mpdev.dll</param>
        /// <returns>The embedded DLL as a byte array.</returns>
        public static byte[] getEmbededDLL(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource '{resourceName}' not found.");
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}
