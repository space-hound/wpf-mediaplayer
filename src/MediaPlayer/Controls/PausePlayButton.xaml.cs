using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MediaPlayer.Others;

namespace MediaPlayer.Controls
{

    public partial class PausePlay : UserControl
    {
        //state as in playing or stoped
        private bool state = false;
        //set image acording to state and visual state
        private void setImageState(bool isHover)
        {
            string stateString = this.state ? "pause" : "play";
            if (isHover)
            {
                this.Image.Source = ImageSourcer.GetHover(stateString);
            }
            else
            {
                this.Image.Source = ImageSourcer.GetNormal(stateString);
            }
        }
        //change state and set image acordingly
        private void changeState(bool isHover)
        {
            this.state = !this.state;
            this.setImageState(isHover);
        }
        //constructor
        public PausePlay()
        {
            InitializeComponent();
            this.init();
        }
        //init function
        private void init()
        {
            this.setImageState(false);
        }
        //click handler (callback for ouside interaction)
        public RoutedEventHandler OnClick;
        //click event (changes state and run handler for stop/play music)
        private void _click(object sender, RoutedEventArgs e)
        {
            //return if there is no handler asigned
            if(this.OnClick is null) { return; }
            //change it's state
            this.changeState(true);
            //run handler
            this.OnClick(sender, e);
        }
        //mouse enter channge visual state to hover
        private void _mouseEnter(object sender, MouseEventArgs e)
        {
            this.setImageState(true);
        }
        //mouse leave change visual state to normal
        private void _mouseLeave(object sender, MouseEventArgs e)
        {
            this.setImageState(false);
        }
    }
}
