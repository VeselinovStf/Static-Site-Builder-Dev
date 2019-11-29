namespace Infrastructure.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string StringToBase64(this string str)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);

            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}