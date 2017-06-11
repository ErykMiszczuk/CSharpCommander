using CSharpCommander.DataModels;
using CSharpCommander.View;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CSharpCommander
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int pathsCount = 0;
            List<String> ptc = new List<String>();
            foreach (UIElement panel in CSharpComGrid.Children)
            {
                if (panel is ListOfFilesPanelView)
                {

                    List<String> pptc = ((ListOfFilesPanelView)panel).PathsToCopy;
                    foreach (string path in pptc)
                    {
                        ptc.Add(path);
                    }
                    int ptcc = ptc.Count;
                    pathsCount += ptcc;
                }
            }

            etykieta.Content = $"Obiektów zaznaczono {pathsCount}";

            foreach (string path in ptc)
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch { }
                }
            }

            foreach (UIElement panel in CSharpComGrid.Children)
            {
                if (panel is ListOfFilesPanelView)
                {
                    List<string> pptc = ((ListOfFilesPanelView)panel).PathsToCopy;
                    pptc.Clear();
                }
            }
            ptc.Clear();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            int pathsCount = 0;
            List<String> ptc = new List<String>();
            string targetPath = "\\";
            foreach (UIElement panel in CSharpComGrid.Children)
            {
                if (panel is ListOfFilesPanelView)
                {

                    List<String> pptc = ((ListOfFilesPanelView)panel).PathsToCopy;
                    foreach (string path in pptc)
                    {
                        ptc.Add(path);
                    }
                    if ((pptc.Count() <= 0) && (((ListOfFilesPanelView)panel).PanelPath() != null))
                    {
                        targetPath = ((ListOfFilesPanelView)panel).PanelPath();
                    }
                    int ptcc = ptc.Count;
                    pathsCount += ptcc;
                }
            }

            etykieta.Content = $"Obiektów zaznaczono {pathsCount}";

            foreach (string path in ptc)
            {
                try
                {
                    File.Copy(path, targetPath + "\\" + System.IO.Path.GetFileName(path));
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(ioe.Message);
                }
            }

            foreach (UIElement panel in CSharpComGrid.Children)
            {
                if (panel is ListOfFilesPanelView)
                {
                    List<string> pptc = ((ListOfFilesPanelView)panel).PathsToCopy;
                    pptc.Clear();
                }
            }
            ptc.Clear();
        }
    }
}
