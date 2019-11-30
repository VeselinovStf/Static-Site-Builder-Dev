using System;

namespace Infrastructure.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string GetBase64StringForImage(this string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}