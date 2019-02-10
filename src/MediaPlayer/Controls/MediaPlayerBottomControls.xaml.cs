using System.Windows;
using System.Windows.Controls;

namespace MediaPlayer.Controls
{
    public partial class MediaPlayerBottomControls : UserControl
    {
        /*===============================================================================================================*/
        /* CONSTRUCTOR */
        /*===============================================================================================================*/
        public MediaPlayerBottomControls()
        {
            InitializeComponent();
            this.Init();
        }

        /*===============================================================================================================*/
        /* INIT */
        /*===============================================================================================================*/
        private void Init()
        {
            this.Prev.OnClick += _onPrev;
            this.Backward.OnClick += _onBackward;
            this.PausePlay.OnClick += _onPausePlay;
            this.Forward.OnClick += _onForward;
            this.Next.OnClick += _onNext;
        }

        /*===============================================================================================================*/
        /* EVENT HANDLERS */
        /*===============================================================================================================*/
        public RoutedEventHandler OnPrev;
        public RoutedEventHandler OnBackward;
        public RoutedEventHandler OnPausePlay;
        public RoutedEventHandler OnForward;
        public RoutedEventHandler OnNext;

        /*===============================================================================================================*/
        /* EVENTS */
        /*===============================================================================================================*/
        private void _onPrev(object sender, RoutedEventArgs e)
        {
            if (this.OnPrev is null)
                return;
            this.OnPrev(sender, e);
        }
        private void _onBackward(object sender, RoutedEventArgs e)
        {
            if (this.OnBackward is null)
                return;
            this.OnBackward(sender, e);
        }
        private void _onPausePlay(object sender, RoutedEventArgs e)
        { 
            if (this.OnPausePlay is null)
                return;
            this.OnPausePlay(sender, e);
        }
        private void _onForward(object sender, RoutedEventArgs e)
        {
            if (this.OnForward is null)
                return;
            this.OnForward(sender, e);
        }
        private void _onNext(object sender, RoutedEventArgs e)
        {
            if (this.OnNext is null)
                return;
            this.OnNext(sender, e);
        }
    }
}
