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
        //public List<DriveData> DrivesList = new List<DriveData>();
        //public List<DiscElement> FolderTwoElements = new List<DiscElement>();

        

        public MainWindow()
        {
            InitializeComponent();

            //DriveInfo[] drives = DriveInfo.GetDrives();
            //foreach (DriveInfo d in drives)
            //{
            //    DriveData disc = new DriveData(d.VolumeLabel, d.Name);
            //    DrivesList.Add(disc);
            //}

            ////driveCharSelect1.ItemsSource = DrivesList;
            //driveCharSelect2.ItemsSource = DrivesList;

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
                    if ((pptc.Count() <= 0 ) && (((ListOfFilesPanelView)panel).PanelPath() != null))
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
                File.Copy(path, targetPath+"\\"+ System.IO.Path.GetFileName(path));
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

        //private void Path_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //MessageBox.Show(Convert.ToString(e.Source.Text));
        //    var source = e.OriginalSource as TextBox;

        //    if (source == null)
        //        return;

        //    //MessageBox.Show(source.Text);
        //    FolderOneElements = GetDiscElements(source.Text);
        //    folderOneElements.Items.Clear();
        //    foreach (DiscElement ele in FolderOneElements)
        //    {
        //        folderOneElements.Items.Add(ele.Name);
        //    }
        //}

        //private void Path_TextChanged2(object sender, TextChangedEventArgs e)
        //{
        //    //MessageBox.Show(Convert.ToString(e.Source.Text));
        //    var source = e.OriginalSource as TextBox;

        //    if (source == null)
        //        return;

        //    //MessageBox.Show(source.Text);
        //    FolderTwoElements = GetDiscElements(source.Text);
        //    folderTwoElements.Items.Clear();
        //    foreach (DiscElement ele in FolderTwoElements)
        //    {
        //        folderTwoElements.Items.Add(ele.Name);
        //    }
        //}

        //private List<DiscElement> GetDiscElements(string path)
        //{
        //    MyDirectory myDirectory = new MyDirectory(path);
        //    List<DiscElement> result = myDirectory.GetSubDiscElements();
        //    return result;
        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource driveDataViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("driveDataViewSource")));
        //    // Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
        //    // driveDataViewSource.Źródło = [ogólne źródło danych]
        //}
    }
}
