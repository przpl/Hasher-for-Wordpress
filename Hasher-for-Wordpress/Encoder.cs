namespace WordpressPasswordHasher
{
    internal class Encoder
    {
        private readonly string _itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public string Encode64(byte[] bytes, int count)
        {
            int characters = _itoa64.Length;
            string result = string.Empty;
            int i = 0;
            while (i < count)
            {
                int value = bytes[i];
                i++;
                result += _itoa64[value % characters];
                if (i < count)
                    value |= bytes[i] << 8;
                result += _itoa64[(value >> 6) % characters];
                if (i >= count)
                    break;
                i++;
                if (i < count)
                    value |= bytes[i] << 16;
                result += _itoa64[(value >> 12) % characters];
                if (i >= count)
                    break;
                i++;
                result += _itoa64[(value >> 18) % characters];
            }

            return result;
        }
    }
}
