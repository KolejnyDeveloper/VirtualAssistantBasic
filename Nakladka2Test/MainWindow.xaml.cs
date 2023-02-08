using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nakladka2Test
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ikonawzasobniku ikonawzasobniku;
        public MainWindow()
        {
            InitializeComponent();

            ikonawzasobniku = new Ikonawzasobniku(this);
        }
        #region Przenoszenie okna
        private bool przenos = false;
        private Cursor cursor = null;
        private Point poczatkowy;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == Mouse.LeftButton)
            {
                przenos = true;
                cursor = this.Cursor;
                Cursor = Cursors.Hand;
                poczatkowy = e.GetPosition(this);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (przenos)
            {
                Vector ruch = e.GetPosition(this) - poczatkowy;
                Left += ruch.X;
                Top += ruch.Y;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            //this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Topmost = true;
            //this.Top = 0;
            //this.Left = 0;
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
            this.Activate();
        }
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(przenos)
            {
                Cursor = cursor;
                przenos = false;
            }
        }
        #endregion
        bool zakanim = false;
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
               this.Close();
            }
        }

        
        
        private void okno_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!zakanim)
            {
                Storyboard Znikanie = this.Resources["Znikanie"] as Storyboard;
                Znikanie.Begin();
                e.Cancel = true;
            }
        }
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            zakanim = true;
            ikonawzasobniku.Usun();
            //e.Cancel = false;
            Close();
        }

        private void okno_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var window = (Window)sender;
            window.Topmost = true;
        }

        
       
    }
}
