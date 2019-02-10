using MediaPlayer.Others;
using System.Windows;
using System.Windows.Controls;

namespace MediaPlayer.Controls
{
    public partial class SoundButton : UserControl
    {
        //constructor
        public SoundButton()
        {
            InitializeComponent();
            this.Image.Source = ImageSourcer.GetSound(this.state);
        }
        //state
        private bool state = false;
        private void _changeState()
        {
            this.state = !this.state;
            this.Image.Source = ImageSourcer.GetSound(this.state);
        }
        //click handler (callback for ouside interaction)
        public RoutedEventHandler OnClick;
        //click event (changes state and run handler for stop/play music)
        private void _click(object sender, RoutedEventArgs e)
        {
            //return if there is no handler asigned
            if (this.OnClick is null) { return; }
            //change it's state
            this._changeState();
            //run handler
            this.OnClick(sender, e);
        }
    }
}
