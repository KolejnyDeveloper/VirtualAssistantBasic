using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nakladka2Test
{
    class Ikonawzasobniku
    {
        private NotifyIcon notifyIcon;
        private System.Windows.Window okno;

        public Ikonawzasobniku(System.Windows.Window okno)
        {
            this.okno = okno;
            string nazwaIko = "b.ico";
            string nazwaApp = Application.ProductName;
            System.Windows.Resources.StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri(@"/" + nazwaApp + ";component/" + nazwaIko, UriKind.RelativeOrAbsolute));
            Icon ikona = new Icon(sri.Stream);

            ContextMenuStrip menu = makemenu();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = ikona;
            notifyIcon.Text = "Cos";
            notifyIcon.ContextMenuStrip = menu;
            notifyIcon.Visible = true;

            notifyIcon.DoubleClick += (s, e) =>
            {
                notifyIcon.BalloonTipTitle = "Test";
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipText = "Cześć";
                notifyIcon.ShowBalloonTip(2000);
            };

            okno.MouseRightButtonDown += (s, e) =>
            {
                System.Windows.Point p = okno.PointToScreen(e.GetPosition(okno));
                menu.Show((int)p.X, (int)p.Y);
            };
        }

        private ContextMenuStrip makemenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem ukryjtsmi = new ToolStripMenuItem("Schowaj");
            ukryjtsmi.Click += (s, e) => { okno.Hide(); };
            menu.Items.Add(ukryjtsmi);
            ToolStripMenuItem wroctsmi = new ToolStripMenuItem("Wroc"); //do mnie (niech skonczy sie ten dziwny sen)");
            wroctsmi.Click += (s, e) => { okno.Show(); };
            menu.Items.Add(wroctsmi);
            ToolStripMenuItem zamktsmi = new ToolStripMenuItem("Zamknij");
            zamktsmi.Click += (s, e) => { okno.Close(); };
            menu.Items.Add(zamktsmi);

            return menu;
        }

        private bool Widoczna
        {
            get
            {
                return notifyIcon.Visible;
            }
            set
            {
                notifyIcon.Visible = value;
            }
        }

        public void Usun()
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            notifyIcon = null; 
        }
    }
}
