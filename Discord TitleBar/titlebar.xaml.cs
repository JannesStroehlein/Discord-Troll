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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Discord_TitleBar
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Closebutton_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Closebutton_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Closebutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window scope = (Window)this.Parent;
            scope.Close();
        }

        private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Titlebar_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Title_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
