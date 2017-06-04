using CSharpCommander.DataModels;
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
