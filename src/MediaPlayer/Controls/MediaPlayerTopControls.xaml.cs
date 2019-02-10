using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MediaPlayer.Controls
{
    public partial class MediaPlayerTopControls : UserControl
    {
        /*===============================================================================================================*/
        /* CONSTRUCTORS */
        /*===============================================================================================================*/
        public MediaPlayerTopControls()
        {
            InitializeComponent();
            this.Init();
        }

        /*===============================================================================================================*/
        /* INIT */
        /*===============================================================================================================*/
        private void Init()
        {
            this.Shuffle.OnClick += _onShuffle;
            this.Up.OnClick += _onUp;
            this.Down.OnClick += _onDown;
            this.Del.OnClick += _onDel;
            this.Sound.OnClick += _onSound;
        }

        /*===============================================================================================================*/
        /* EVENT HANDLERS */
        /*===============================================================================================================*/
        public RoutedEventHandler OnShuffle;
        public RoutedEventHandler OnUp;
        public RoutedEventHandler OnDown;
        public RoutedEventHandler OnDel;
        public RoutedEventHandler OnSound;

        /*===============================================================================================================*/
        /* EVENTS */
        /*===============================================================================================================*/
        private void _onShuffle(object sender, RoutedEventArgs e)
        {
            if (this.OnShuffle is null)
                return;
            this.OnShuffle(sender, e);
        }
        private void _onUp(object sender, RoutedEventArgs e)
        {
            if (this.OnUp is null)
                return;
            this.OnUp(sender, e);
        }
        private void _onDown(object sender, RoutedEventArgs e)
        {
            if (this.OnDown is null)
                return;
            this.OnDown(sender, e);
        }
        private void _onDel(object sender, RoutedEventArgs e)
        {
            if (this.OnDel is null)
                return;
            this.OnDel(sender, e);
        }
        private void _onSound(object sender, RoutedEventArgs e)
        {
            if (this.OnSound is null)
                return;
            this.OnSound(sender, e);
        }
    }
}
