using System;
using System.Linq;
using MediaPlayer.Models;
using System.Collections.ObjectModel;

namespace MediaPlayer.Others
{
    public static class Shuffler
    {
        private static Random rnd = new Random();
        //shuffle the recived array (Fisher Yates algorithm)
        public static void Shuffle(ObservableCollection<Song> initial)
        {
            int length = initial.Count();
            for (int i = 0; i < length; i++)
            {
                int r = i + rnd.Next(length - i);
                Song temp = initial[r];
                initial[r] = initial[i];
                initial[i] = temp;
            }

        }
    }
}
