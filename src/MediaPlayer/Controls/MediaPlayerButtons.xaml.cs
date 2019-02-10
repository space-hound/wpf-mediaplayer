using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MediaPlayer.Others;

namespace MediaPlayer.Controls
{
    public partial class MediaPlayerButtons : UserControl
    {
        //type of button (next, prev, etc.)
        public string Type
        {
            get { return this.Tag.ToString(); } 
        }
        //viusal state (hover or not)
        private void setImageState(bool isHover)
        {
            if (isHover)
            {
                this.Image.Source = ImageSourcer.GetHover(this.Type);
            }
            else
            {
                this.Image.Source = ImageSourcer.GetNormal(this.Type);
            }
        }
        //constructor
        public MediaPlayerButtons()
        {
            InitializeComponent();
            this.init();
        }
        //init function
        private void init()
        {
            Loaded += Ready;
        }
        //apparently it tries to load image before reading Tag prop so set image after ready
        //it does't work with dependency property either
        private void Ready(object sender, RoutedEventArgs e)
        {
            this.setImageState(false);
        }
        //click handler (callback for ouside interaction)
        public RoutedEventHandler OnClick;
        //click event (changes state and run handler for stop/play music)
        private void _click(object sender, RoutedEventArgs e)
        {
            //return if there is no handler asigned
            if (this.OnClick is null) { return; }
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
