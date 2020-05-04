using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Win32;

namespace Discord_Troll_Gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
        private Point startPoint = new Point(0, 0);
        private bool drag = false;
        private string selected_profile = "";
        private string profiledir = @"C:\Users\Jannes\Documents\.discord tolls\.profile";
        private string presetdir = @"C:\Users\Jannes\Documents\.discord tolls";
        DiscordRpcClient client = new DiscordRpcClient("606102103232086036")
        {
            Logger = new DiscordRPC.Logging.ConsoleLogger()
        };

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Setbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.output.Text += "Verbunden zu Discord Nutzer : " + client.CurrentUser.Username + "\n";
                ListBoxItem lik = (ListBoxItem)largeimkey.SelectedItem;
                ListBoxItem sik = (ListBoxItem)smallimkey.SelectedItem;

                client.SetPresence(new RichPresence()
                {
                    Details = detailsin.Text,
                    State = statein.Text,
                    Assets = new Assets()
                    {
                        SmallImageText = SmallImageTextin.Text,
                        LargeImageKey = lik.Content.ToString(),
                        LargeImageText = LargeImageTextin.Text,
                        SmallImageKey = sik.Content.ToString()
                    }
                });
                this.output.Text += "Einstellungen werden übernommen...\n";
            }
            catch
            {
                this.output.Text += "Einstellungen konnten nicht übernommen werden.\n";
            }
        }
        void Init()
        {
            client.OnReady += (sender, msg) =>
            {                
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.output.Text += "Verbunden zu Discord Nutzer : " + msg.User.Username + "\n";
                }));
            };

            client.OnPresenceUpdate += (sender, msg) =>
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.output.Text += "Einstellungen wurden übernommen.\n";
                }));                
            };
            client.Initialize();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ni.Dispose();
            client.Dispose();
        }
        private void Savefs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog savepath = new SaveFileDialog();

                savepath.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                savepath.InitialDirectory = @"C:\Users\Jannes\Documents\.discord tolls";
                savepath.FilterIndex = 1;
                savepath.RestoreDirectory = true;

                if (savepath.ShowDialog() == true)
                {
                    this.output.Text += savepath.FileName + "\n";
                    ListBoxItem lik = (ListBoxItem)largeimkey.SelectedItem;
                    ListBoxItem sik = (ListBoxItem)smallimkey.SelectedItem;
                    Config saver = new Config(savepath.FileName);
                    saver.FileInit();
                    saver.Addentry("Profile", selected_profile);
                    saver.Addentry("Details", detailsin.Text);
                    saver.Addentry("State", statein.Text);
                    saver.Addentry("SmallImageText", SmallImageTextin.Text);
                    saver.Addentry("LargeImageKey", lik.Content.ToString());
                    saver.Addentry("LargeImageText", LargeImageTextin.Text);
                    saver.Addentry("SmallImageKey", sik.Content.ToString());
                }
            }
            catch
            {
                SaveFileDialog savepath = new SaveFileDialog();

                savepath.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                savepath.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                savepath.FilterIndex = 1;
                savepath.RestoreDirectory = true;

                if (savepath.ShowDialog() == true)
                {
                    this.output.Text += savepath.FileName + "\n";
                    ListBoxItem lik = (ListBoxItem)largeimkey.SelectedItem;
                    ListBoxItem sik = (ListBoxItem)smallimkey.SelectedItem;
                    Config saver = new Config(savepath.FileName);
                    saver.FileInit();
                    saver.Addentry("Details", detailsin.Text);
                    saver.Addentry("State", statein.Text);
                    saver.Addentry("SmallImageText", SmallImageTextin.Text);
                    saver.Addentry("LargeImageKey", lik.Content.ToString());
                    saver.Addentry("LargeImageText", LargeImageTextin.Text);
                    saver.Addentry("SmallImageKey", sik.Content.ToString());
                }
            }

        }
        private void Loadfs_Click(object sender, RoutedEventArgs e)
        {
            load();
        }
        private void load(string path = null)
        {
            try
            {
                if (path == null)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = @"C:\Users\Jannes\Documents\.discord tolls";
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == true)
                    {
                        path = openFileDialog.FileName;
                    }
                }
                Config loader = new Config(path);
                loader.FileInit();
                for (int es = 0; es < profile_selector.Items.Count; es++)
                {
                    string q = loader.Getvalue("Profile");
                    ListBoxItem itemm = (ListBoxItem)profile_selector.Items[es];
                    if ((string)itemm.Content == q)
                    {
                        profile_selector.SelectedIndex = es;
                    }
                }
                detailsin.Text = loader.Getvalue("Details");
                statein.Text = loader.Getvalue("State");
                SmallImageTextin.Text = loader.Getvalue("SmallImageText");
                for (int es = 0; es < largeimkey.Items.Count; es++)
                {
                    string q = loader.Getvalue("LargeImageKey");
                    ListBoxItem itemm = (ListBoxItem)largeimkey.Items[es];
                    if ((string)itemm.Content == q)
                    {
                        largeimkey.SelectedIndex = es;
                    }
                }
                LargeImageTextin.Text = loader.Getvalue("LargeImageText");
                for (int es = 0; es < smallimkey.Items.Count; es++)
                {
                    string q = loader.Getvalue("SmallImageKey");
                    ListBoxItem itemm = (ListBoxItem)smallimkey.Items[es];
                    if ((string)itemm.Content == q)
                    {
                        smallimkey.SelectedIndex = es;
                    }
                }
            }
            catch
            {
            }
        }

        #region tbl
        private void Closebutton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.closebutton.Source = IconUtilities.ToImageSource(Properties.Resources.closepress);
        }
        private void Closebutton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.closebutton.Source = IconUtilities.ToImageSource(Properties.Resources.close);
        }
        private async void Closebutton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await Task.Delay(250);
            if (e.ClickCount == 2)
            {
                this.Close();
            }
            else
            {
                this.Hide();
                
                ni.Icon = Properties.Resources.discordtroll;
                ni.Text = "Discord Troll";
                ni.Visible = true;
                ni.BalloonTipTitle = "Discord Troll";
                ni.BalloonTipText = "Wurde Minimiert und kann mit einem Doppelklick wieder in den Vordergrund gebracht werden.";
                ni.BalloonTipClicked += (s, ep) => this.Show();
                ni.ShowBalloonTip(3);
                ni.DoubleClick +=
                    delegate (object nisender, EventArgs args)
                    {
                        this.Show();
                    };
            }
        }
        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
            if (this.drag)
            {
                Point p1 = e.GetPosition(this);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new System.Windows.Point(p2.X - this.startPoint.X,
                                     p2.Y - this.startPoint.Y);
                this.Top = p3.Y;
                this.Left = p3.X;
            }
        }
        private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.drag = true;
            this.startPoint = e.GetPosition(this);
            this.titlebar.CaptureMouse();
        }
        #endregion

        private void Largeimkey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            try
            {
                ListBoxItem lik = (ListBoxItem)largeimkey.SelectedItem;
                this.largeboxtext.Content = lik.Content;
            }
            catch { }
        }
        private void Smallimkey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxItem sik = (ListBoxItem)smallimkey.SelectedItem;
                this.smallboxtext.Content = sik.Content;
            }
            catch { }
        }
        private void Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            output.ScrollToEnd();
        }
        private void Profile_selector_Loaded(object sender, RoutedEventArgs e)
        {
            string[] filePaths = Directory.GetFiles(profiledir, "*.txt");
            for (int i = 0; i < filePaths.Length; i++)
            {
                ListBoxItem item = new ListBoxItem();
                string filename = filePaths[i].Replace(profiledir, "");
                filename = filename.Replace(@"\", "");
                filename = filename.Replace(@".txt", "");
                item.Content = filename;
                item.DataContext = filePaths[i];
                profile_selector.Items.Add(item);
            }
        }
        private void Profile_selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            largeimkey.SelectedIndex = -1;
            smallimkey.SelectedIndex = -1;
            try
            {
                largeimkey.ItemsSource = null;
                smallimkey.ItemsSource = null;
                ListBoxItem d = (ListBoxItem)profile_selector.SelectedItem;
                Config loader = new Config((string)d.DataContext);
                loader.FileInit();
                selected_profile = loader.Getvalue("Name");
                dispnamelabel.Content = loader.Getvalue("DispName");
                client.Dispose();
                client = new DiscordRpcClient(loader.Getvalue("ClientID"))
                {
                    Logger = new DiscordRPC.Logging.ConsoleLogger()
                };
                Init();
            
                string[] assets = loader.Getvalue("RPAssets").Split(',');
                ListBoxItem[] ad = new ListBoxItem[assets.Length];
                for (int i = 0; i < assets.Length; i++)
                {
                    ad[i] = new ListBoxItem();
                    ad[i].Content = assets[i];
                }

                largeimkey.ItemsSource = ad;
                smallimkey.ItemsSource = ad;
                profileselected.Content = selected_profile;
            }
            catch
            {

            }
        }
        private void Presetselector_Loaded(object sender, RoutedEventArgs e)
        {
            string[] filePaths = Directory.GetFiles(presetdir, "*.txt");
            for (int i = 0; i < filePaths.Length; i++)
            {
                ListBoxItem item = new ListBoxItem();
                string filename = filePaths[i].Replace(presetdir, "");
                filename = filename.Replace(@"\", "");
                filename = filename.Replace(@".txt", "");
                item.Content = filename;
                item.DataContext = filePaths[i];
                presetselector.Items.Add(item);
            }
        }
        private void Presetselector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxItem d = (ListBoxItem)presetselector.SelectedItem;
                load(d.DataContext.ToString());
                presetselected.Content = d.Content;
            }
            catch
            {

            }
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