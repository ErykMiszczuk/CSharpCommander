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

namespace CSharpCommander.View
{
    /// <summary>
    /// Logika interakcji dla klasy ListOfFilesPanelView.xaml
    /// </summary>
    public partial class ListOfFilesPanelView : UserControl
    {
        public List<DriveData> DrivesList = new List<DriveData>();
        public List<DiscElement> FolderOneElements = new List<DiscElement>();
        public ListOfFilesPanelView()
        {
            InitializeComponent();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo d in drives)
            {
                DriveData disc = new DriveData(d.VolumeLabel, d.Name);
                DrivesList.Add(disc);
            }

            driveCharSelect1.ItemsSource = DrivesList;
        }

        private List<DiscElement> GetDiscElements(string path)
        {
            MyDirectory myDirectory = new MyDirectory(path);
            List<DiscElement> result = myDirectory.GetSubDiscElements();
            return result;
        }

        private void Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show(Convert.ToString(e.Source.Text));
            var source = e.OriginalSource as TextBox;

            if (source == null)
                return;

            //MessageBox.Show(source.Text);
            try
            {
                FolderOneElements = GetDiscElements(source.Text);
            }
            catch { }
            folderOneElements.Items.Clear();
            foreach (DiscElement ele in FolderOneElements)
            {
                folderOneElements.Items.Add(ele.Name);
            }
        }
    }
}
