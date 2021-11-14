namespace MovieCollection.OpenSubtitles
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The Open Subtitles movie file hasher.
    /// </summary>
    /// <remarks>
    /// Open Subtitles is using special hash function to match subtitle files against movie files.
    /// Hash is not dependent on file name of movie file.
    /// <para>Originally implemented by:
    /// https://trac.opensubtitles.org/projects/opensubtitles/wiki/HashSourceCodes.</para>
    /// </remarks>
    public class OpenSubtitlesHasher
    {
        /// <summary>
        /// Computes the hash value for a file by using the default hash algorithm.
        /// </summary>
        /// <param name="filename">The file to compute the hash from.</param>
        /// <returns>A string contaning the computed hash.</returns>
        public static string GetFileHash(string filename)
        {
            var hash = ComputeMovieHash(filename);
            return ToHexadecimal(hash);
        }

        private static byte[] ComputeMovieHash(string filename)
        {
            byte[] result;
            using (var input = File.OpenRead(filename))
            {
                result = ComputeMovieHash(input);
            }

            return result;
        }

        private static byte[] ComputeMovieHash(Stream input)
        {
            long lhash, streamsize;
            streamsize = input.Length;
            lhash = streamsize;

            long i = 0;
            byte[] buffer = new byte[sizeof(long)];
            while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                lhash += BitConverter.ToInt64(buffer, 0);
            }

            input.Position = Math.Max(0, streamsize - 65536);
            i = 0;
            while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                lhash += BitConverter.ToInt64(buffer, 0);
            }

            input.Close();
            byte[] result = BitConverter.GetBytes(lhash);
            Array.Reverse(result);
            return result;
        }

        private static string ToHexadecimal(byte[] bytes)
        {
            var hexBuilder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                hexBuilder.Append(bytes[i].ToString("x2", CultureInfo.InvariantCulture));
            }

            return hexBuilder.ToString();
        }
    }
}
