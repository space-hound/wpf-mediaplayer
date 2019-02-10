using MediaPlayer.Others;
using System.Collections.ObjectModel;

namespace MediaPlayer.Models
{
    public class SongList
    {
        private ObservableCollection<Song> songs;
        public ObservableCollection<Song> Songs
        {
            get { return this.songs; }
            set { this.songs = value; }
        }
        private int currentIndex = 0;
        public int CurrentIndex
        {
            get
            {
                return this.currentIndex;
            }
            set
            {
                if (value == this.currentIndex)
                {
                    return;
                }
                if (value >= this.songs.Count)
                {
                    return;
                }
                if (value < 0)
                {
                    return;
                }

                this.currentIndex = value;
            }
        }
        public Song CurrentSong
        {
            get
            {
                if (this.songs.Count < 0)
                {
                    return null;
                }
                return this.songs[this.currentIndex];
            }
        }
        public bool IsEmpty()
        {
            return this.songs.Count == 0;
        }
        //add a song to list
        public void AddSong(Song s)
        {
            this.songs.Add(s);
        }
        //add songs to the list
        public void AddSongs(string[] songs)
        {
            foreach (string song in songs)
            {
                this.songs.Add(new Song(song));
            }
        }
        //remove at index
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.songs.Count)
            {
                return;
            }
            this.songs.RemoveAt(index);
        }
        //remove current
        public void RemoveCurrent()
        {
            if (this.currentIndex < 0 || this.currentIndex >= this.songs.Count)
            {
                return;
            }
            this.songs.RemoveAt(this.currentIndex);
        }
        //move the index with one
        public void Next()
        {
            if (this.currentIndex + 1 >= this.songs.Count)
            {
                this.currentIndex = 0;
            }
            else
            {
                this.currentIndex++;
            }
        }
        //move the index with one
        public void Prev()
        {
            if (this.currentIndex - 1 < 0)
            {
                this.currentIndex = this.songs.Count - 1;
            }
            else
            {
                this.currentIndex--;
            }
        }
        //shuffle
        public void Shuffle()
        {
            Shuffler.Shuffle(this.songs);
        }
        //move one up in the que
        public void MoveUp()
        {
            if (this.currentIndex - 1 < 0)
            {
                return;
            }

            Song temp = this.songs[this.currentIndex - 1];
            this.songs[this.currentIndex - 1] = this.songs[this.currentIndex];
            this.songs[this.currentIndex] = temp;

            this.Prev();
        }
        public void MoveUpAt(int index)
        {
            if (index - 1 < 0)
            {
                return;
            }

            Song temp = this.songs[index - 1];
            this.songs[index - 1] = this.songs[index];
            this.songs[index] = temp;
        }
        //move one down in the que
        public void MoveDown()
        {
            if (this.currentIndex + 1 >= this.songs.Count)
            {
                return;
            }

            Song temp = this.songs[this.currentIndex + 1];
            this.songs[this.currentIndex + 1] = this.songs[this.currentIndex];
            this.songs[this.currentIndex] = temp;

            this.Next();
        }
        public void MoveDownAt(int index)
        {
            if (index + 1 >= this.songs.Count)
            {
                return;
            }

            Song temp = this.songs[index + 1];
            this.songs[index + 1] = this.songs[index];
            this.songs[index] = temp;
        }
        public void DeleteAt(int index)
        {
            if(index > 0 || index < this.songs.Count)
            {
                this.songs.RemoveAt(index);
            }
        }
        //constructor default
        public SongList()
        {
            this.songs = new ObservableCollection<Song>();
        }
        //constructor from array of files
        public SongList(string[] songs)
        {
            this.songs = new ObservableCollection<Song>();
            foreach (string song in songs)
            {
                this.songs.Add(new Song(song));
            }
        }
    }
}
