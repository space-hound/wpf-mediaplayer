using System.Collections.Generic;

namespace MediaPlayer.Others
{
    public static class FileChecker
    {
        public static bool Passes(string path)
        {
            if (System.IO.File.Exists(path))
            {
                if ((System.IO.Path.GetExtension(path) == ".mp3"))
                {
                    return true;
                }

                if ((System.IO.Path.GetExtension(path) == ".mp4"))
                {
                    return true;
                }
            }
            return false;
        }
        public static string[] PassedFiles(string[] songs)
        {
            List<string> goodSongs = new List<string>();
            foreach(string song in songs)
            {
                if (Passes(song))
                {
                    goodSongs.Add(song);
                }
            }
            return goodSongs.ToArray();
        }
    }
}
