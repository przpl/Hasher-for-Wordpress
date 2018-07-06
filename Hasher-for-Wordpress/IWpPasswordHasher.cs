namespace WordpressPasswordHasher
{
    public interface IWpPasswordHasher
    {
        /// <summary>
        /// Verify if hash matches password.
        /// </summary>
        /// <param name="hash">Password hash from 'user_pass' column (default configuration). Hash contains salt and usually starts with '$P$B' sequence.</param>
        /// <param name="password">User's password.</param>
        bool Verify(string hash, string password);

        /// <summary>
        /// Calculates hash from password and salt.
        /// </summary>
        /// <param name="password">User's password.</param>
        /// <param name="salt">Salt starting with prefix, usually '$P$B'. Must be length of 12 characters.</param>
        /// <returns>Concatenation of salt and password hash.</returns>
        string Hash(string password, string salt);
    }
}
