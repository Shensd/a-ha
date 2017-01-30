using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace a_ha
{
    class Program
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        static void Main(string[] args)
        {
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopPtr);

            Random rand = new Random();

            SoundPlayer player = new SoundPlayer(a_ha.Properties.Resources.take_on_me);
            player.PlayLooping();

            int totalWidth = 0;
            int totalHeight = 0;

            foreach(var screen in Screen.AllScreens)
            {
                totalWidth += screen.Bounds.Width;
                if(screen.Bounds.Height > totalHeight)
                {
                    totalHeight = screen.Bounds.Height;
                }
            }

            Thread.Sleep(2500);

            int cycle = 0;

            while (true) {
                for(int i = 0; i < 10; i++) {
                    int randX = rand.Next(1, totalWidth);
                    int randY = rand.Next(1, totalHeight);
                    g.FillRectangle(Brushes.Black, new Rectangle(randX, randY, 85, 15));
                    g.DrawString("TAKE ON ME", new Font(FontFamily.GenericSansSerif.Name, 10), Brushes.White, randX, randY);
                }

                Bitmap bit = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics bit_g = Graphics.FromImage(bit);
                bit_g.CopyFromScreen(0, 0, 0, 0, bit.Size);
                if(cycle % 2 == 0)
                {
                    bit.RotateFlip(RotateFlipType.Rotate180FlipNone);
                } else
                {
                    bit.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                g.DrawImage(bit, 0, 0);

                cycle++;
                Thread.Sleep(1000);
            }
        }
    }
}
