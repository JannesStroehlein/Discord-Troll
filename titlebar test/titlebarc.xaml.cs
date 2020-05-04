using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace titlebar_test
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class titlebarc : UserControl
    {
        private Point startPoint = new Point(0, 0);
        private bool drag = false;

        public titlebarc()
        {
            InitializeComponent();          
        }

        private void Closebutton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.closebutton.Source = IconUtilities.ToImageSource(Properties.Resources.closepress);
        }
        private void Closebutton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.closebutton.Source = IconUtilities.ToImageSource(Properties.Resources.close);
        }
        private void Closebutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }
        public static Point GetMousePositionWindowsForms()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new Point(point.X, point.Y);
        }
        private void Title_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.drag = false;
            this.titlebar.ReleaseMouseCapture();
        }
        private void Titlebar_MouseMove(object sender, MouseEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (this.drag)
            {
                Point p1 = e.GetPosition(this);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new System.Windows.Point(p2.X - this.startPoint.X,
                                     p2.Y - this.startPoint.Y);
                parentWindow.Top = p3.Y;
                parentWindow.Left = p3.X;
            }
        }
        private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.drag = true;
            this.startPoint = e.GetPosition(this);
            this.titlebar.CaptureMouse();
        }
    }
    internal static class IconUtilities
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        [STAThread]
        public static ImageSource ToImageSource(this System.Drawing.Bitmap input)
        {
            System.Drawing.Bitmap bitmap = input;
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }
            return wpfBitmap;
        }
    }
}
