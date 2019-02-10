using System;

using NAudio.Wave;

using System.Windows;
using MediaPlayer.Models;
using MediaPlayer.Others;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace MediaPlayer.Controls
{
    public partial class MediaPlayerThePlayer : UserControl
    {
        /*===============================================================================================================*/
        /* TIMER && SLIDER */
        /*===============================================================================================================*/
        //timer to control the seekbar
        private DispatcherTimer Timer;
        //timer update interval (at every _timeInterval)
        private static TimeSpan _timerInterval = TimeSpan.FromMilliseconds(100);
        //init timer
        private void _initTimer()
        {
            //instantiate timer
            this.Timer = new DispatcherTimer();
            //set timer interval
            this.Timer.Interval = _timerInterval;
            //asign callback for timer to be called at every _timerInterval
            Timer.Tick += _timerTick;
            //start timer;
            Timer.Start();
        }
        //timer callback
        private void _timerTick(object sender, EventArgs e)
        {
            //if there are songs then update seeker value
            if (this.SongList.Songs.Count > 0)
            {
                if (!this._isOnSeeker)
                {
                    this._updateSliederPosition();
                }
            }
        }
        //update slider all
        private void _updateSliederAll()
        {
            this.SeekBar.Maximum = this._getCurrentLength() * .99;
            this.SeekBar.Value = this._getCurrentPosition();
        }
        //update slider position
        private void _updateSliederPosition()
        {
            this.SeekBar.Value = this._getCurrentPosition();
        }
        //seekbar value changed
        private bool _isOnSeeker = false;
        private void _seekBarMouseUp(object sender, MouseButtonEventArgs e)
        {
            this._isOnSeeker = false;
            if (this.SongList.Songs.Count > 0)
            {
                this._setCurrentPosition(this.SeekBar.Value);
            }
        }
        private void _seekBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            this._isOnSeeker = true;
        }
        //moved the code from here to _seekBarMouseUp as it was buggy this way
        //and if you moved it to the end netx song start from about 90%
        private void _seekBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        /*===============================================================================================================*/
        /* AUDIO HANDLERS */
        /*===============================================================================================================*/
        //audio files and sound device handler
        private WaveOutEvent OutputDevice;
        private AudioFileReader AudioFile;
        //check if output dveice plays sound
        private bool _isPlaying()
        {
            if(this.OutputDevice is null) { return false; }
            return this.OutputDevice.PlaybackState == PlaybackState.Playing;
        }
        //check if output dveice is not playing sound
        private bool _isStopped()
        {
            if (this.OutputDevice is null) { return false; }
            return this.OutputDevice.PlaybackState == PlaybackState.Stopped;
        }
        //get the current song length
        private double _getCurrentLength()
        {
            if(this.SongList.Songs.Count > 0)
            {
                return (double)this.AudioFile.Length;
            }

            return 0;
        }
        //get the current song position
        private double _getCurrentPosition()
        {
            if (this.SongList.Songs.Count > 0)
            {
                if(this.AudioFile != null)
                {
                    return (double)this.AudioFile.Position;
                }
            }

            return 0;
        }
        //set the current song position
        private void _setCurrentPosition(double val)
        {
            if (this.SongList.Songs.Count > 0)
            {
                this.AudioFile.Position = (long)val;
            }

            return;
        }
        //dispose current song
        //cand vrei sa schmbi o melodie trebe sa executi asta
        private void _disposeCurrentSong()
        {
            if(this.OutputDevice == null)
            {
                return;
            }
            OutputDevice.Dispose();
            OutputDevice = null;
            if(this.AudioFile == null)
            {
                return;
            }
            AudioFile.Dispose();
            AudioFile = null;
        }
        //set current song
        private void _setCurrentSong(Song song)
        {
            OutputDevice = new WaveOutEvent();
            OutputDevice.PlaybackStopped += _onPlayBackStopped;
            AudioFile = new AudioFileReader(song.Path);
            OutputDevice.Init(AudioFile);
            this._updateSliederAll();
            this.SongListView.SelectedIndex = this.SongList.CurrentIndex;
        }
        //set current song
        private void _setCurrentSong()
        {
            OutputDevice = new WaveOutEvent();
            OutputDevice.PlaybackStopped += _onPlayBackStopped;
            AudioFile = new AudioFileReader(this.SongList.CurrentSong.Path);
            OutputDevice.Init(AudioFile);

            this._updateSliederAll();
            this.SongListView.SelectedIndex = this.SongList.CurrentIndex;
        }
        //callback for when player stop playing
        private void _onPlayBackStopped(object sender, StoppedEventArgs args)
        {
            if (this._isStopped())
            {
                if(this._getCurrentPosition() >= (this._getCurrentLength()))
                {
                    this._disposeCurrentSong();
                    this.Next();
                }
            }

        }
        //get current index playing
        public int CurrentIndexPlaying
        {
            get
            {
                return this.SongList.CurrentIndex;
            }
        }
        /*===============================================================================================================*/
        /* LOAD FILES HANDLER */
        /*===============================================================================================================*/
        //hendle upload files on drop them upon this
        private void _onFilesDrop(object sender, DragEventArgs e)
        {
            //only if there are files beeing dropped
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //get an array of paths for all files
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //add the songs to the list
                this.SongList.AddSongs(FileChecker.PassedFiles(files));
                if(this.AudioFile == null || this.OutputDevice == null)
                {
                    this._setCurrentSong(this.SongList.CurrentSong);
                }
            }
        }

        /*===============================================================================================================*/
        /* INTERACTION */
        /*===============================================================================================================*/
        //the song list
        private SongList SongList;
        //state of the player (playing sound or not)
        private bool playerState = false;
        //play if state is true
        private void _playIf()
        {
            if (this.playerState)
            {
                this.OutputDevice.Play();
            }
        }
        //toggle between true and false
        private void _toggleState()
        {
            this.playerState = !this.playerState;
        }
        //toggle play or pause
        public void PausePlay()
        {
            //no songs then can't play anything
            if (this.SongList.IsEmpty())
            {
                return;
            }

            if (this.playerState)
            {
                this.OutputDevice.Stop();
            }
            else
            {
                this.OutputDevice.Play();
            }

            this._toggleState();
        }
        //next song
        public void Next()
        {
            this._disposeCurrentSong();
            this.SongList.Next();
            this._setCurrentSong();
            this._playIf();
        }
        //prev song
        public void Prev()
        {
            this._disposeCurrentSong();
            this.SongList.Prev();
            this._setCurrentSong();
            this._playIf();
        }
        //skip interval
        private long _skipInterval()
        {
            return (long)(this.AudioFile.Length / 15);
        }
        //fast forward
        public void Forward()
        {
            this.AudioFile.Position += this._skipInterval();
        }
        //fast backward
        public void Backward()
        {
            this.AudioFile.Position -= this._skipInterval();
        }

        /*===============================================================================================================*/
        /* BOTTOM CONTROLSSSS */
        /*===============================================================================================================*/
        private void _bottomEvents()
        {
            this.BottomControls.OnBackward += e_Backward;
            this.BottomControls.OnPrev += e_Prev;
            this.BottomControls.OnPausePlay += e_PausePlay;
            this.BottomControls.OnNext += e_Next;
            this.BottomControls.OnForward += e_Forward;
        }
        private void e_Backward(object sender, RoutedEventArgs e)
        {
            this.Backward();
        }
        private void e_Prev(object sender, RoutedEventArgs e)
        {
            this.Prev();
        }
        private void e_PausePlay(object sender, RoutedEventArgs e)
        {
            this.PausePlay();
        }
        private void e_Next(object sender, RoutedEventArgs e)
        {
            this.Next();
        }
        private void e_Forward(object sender, RoutedEventArgs e)
        {
            this.Forward();
        }

        /*===============================================================================================================*/
        /* TOP CONTROLS */
        /*===============================================================================================================*/
        private void _topEvents()
        {
            this.TopControls.OnShuffle += e_Shuffle;
            this.TopControls.OnUp += e_Up;
            this.TopControls.OnDown += e_Down;
            this.TopControls.OnDel += e_Del;
            this.TopControls.OnSound += e_Sound;
        }
        private void e_Shuffle(object sender, RoutedEventArgs e)
        {
            this.SongList.Shuffle();
        }
        private void e_Up(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.SongListView.SelectedIndex;

            if(selectedIndex != -1)
            {
                this.SongList.MoveUpAt(selectedIndex);
                if (selectedIndex == this.SongList.CurrentIndex)
                {
                    this.SongList.CurrentIndex = selectedIndex;
                }

                if (selectedIndex - 1 < 0)
                {
                    this.SongListView.SelectedIndex = 0;
                    if (selectedIndex == this.SongList.CurrentIndex)
                    {
                        this.SongList.CurrentIndex = 0;
                    }
                }
                else
                {
                    this.SongListView.SelectedIndex = selectedIndex - 1;
                    if (selectedIndex == this.SongList.CurrentIndex)
                    {
                        this.SongList.CurrentIndex = selectedIndex - 1;
                    }
                }

            }
        }
        private void e_Down(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.SongListView.SelectedIndex;

            if (selectedIndex != -1)
            {
                this.SongList.MoveDownAt(selectedIndex);
                if (selectedIndex == this.SongList.CurrentIndex)
                {
                    this.SongList.CurrentIndex = selectedIndex;
                }

                if (selectedIndex + 1 >= this.SongListView.Items.Count)
                {
                    this.SongListView.SelectedIndex = this.SongListView.Items.Count - 1;
                    if (selectedIndex == this.SongList.CurrentIndex)
                    {
                        this.SongList.CurrentIndex = this.SongListView.Items.Count - 1;
                    }
                }
                else
                {
                    this.SongListView.SelectedIndex = selectedIndex + 1;
                    if (selectedIndex == this.SongList.CurrentIndex)
                    {
                        this.SongList.CurrentIndex = selectedIndex + 1;
                    }
                }
            }
        }
        private void e_Del(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.SongListView.SelectedIndex;

            if (selectedIndex != -1)
            {
                
                this.SongList.DeleteAt(selectedIndex);
                this._disposeCurrentSong();

                if (selectedIndex == 0 && this.SongListView.Items.Count > 1)
                {
                    this.SongListView.SelectedIndex = selectedIndex;
                }
                else if (selectedIndex >= this.SongListView.Items.Count - 1 && this.SongListView.Items.Count > 1)
                {
                    this.SongListView.SelectedIndex = selectedIndex - 1;
                }
                else if (this.SongListView.Items.Count == 1)
                {
                    this.SongListView.SelectedIndex = 0;
                }
                else
                {
                    this.SongListView.SelectedIndex = selectedIndex;
                }

                if(selectedIndex == this.SongList.CurrentIndex && this.SongListView.SelectedIndex != -1)
                {
                    this.SongList.CurrentIndex = this.SongListView.SelectedIndex;
                    this._setCurrentSong();
                    if (this.playerState)
                    {
                        this.OutputDevice.Play();
                    }
                }
            }
        }
        //check sound state and mute or unmute
        private bool _sound = false;
        private void e_Sound(object sender, RoutedEventArgs e)
        {
            if(this.OutputDevice != null)
            {
                if (this._sound)
                {
                    this.OutputDevice.Volume = 1;
                }
                else
                {
                    this.OutputDevice.Volume = 0;
                }

                this._sound = !this._sound;
            }
        }

        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        //AllocConsole();
        //Console.WriteLine();

        private void _onSongDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            Song song = ((Song)item.Content);
            int index = this.SongList.Songs.IndexOf(song);
            this.SongList.CurrentIndex = index;
            this._disposeCurrentSong();
            this._setCurrentSong();
            if (this.playerState)
            {
                this.OutputDevice.Play();
            }
        }
        /*===============================================================================================================*/
        /* OTHERS */
        /*===============================================================================================================*/
        //init function
        private void _init()
        {
            //create an empty song list
            this.SongList = new SongList();
            //asign a surce to the listview
            this.SongListView.ItemsSource = this.SongList.Songs;
            //init the timer
            this._initTimer();
            //events related to the bottom buttons
            this._bottomEvents();
            //events related to the top buttons
            this._topEvents();
        }

        //constructor
        public MediaPlayerThePlayer()
        {
            InitializeComponent();
            this._init();
        }
    }
}
