namespace DAL.Extentions
{
    public static class StringExtension
    {
        public static int ToInt(this string str)
        {
            int length = str.Length;
            int i = 0;
            int result = 0;
            do
            {
                result += str[i];
            }
            while (++i < length);
            return result;
        }
    }
}
