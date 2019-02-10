using NAudio.Wave;

namespace MediaPlayer.Models
{
    public class Song
    {
        private string path;
        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        //duration of audio file in string format minutes:seconds
        private string duration;
        public string Duration
        {
            get { return this.duration; }
        }
        //name of the audio file
        private string name;
        public string Name
        {
            get { return this.name; }
        }
        //get the name from the path and read its duration 
        private void _scrapeForNameAndDuration()
        {
            this.name = System.IO.Path.GetFileName(this.path);
            this.duration = (new AudioFileReader(this.path)).TotalTime.ToString(@"mm\:ss");
        }
        //constructor
        public Song(string path)
        {
            this.path = path;
            this._scrapeForNameAndDuration();
        }
    }
}
