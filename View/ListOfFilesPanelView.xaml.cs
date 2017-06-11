using CSharpCommander.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CSharpCommander.View
{
    /// <summary>
    /// Logika interakcji dla klasy ListOfFilesPanelView.xaml
    /// </summary>
    public partial class ListOfFilesPanelView : UserControl
    {
        public List<DriveData> DrivesList = new List<DriveData>();
        public List<DiscElement> FolderOneElements = new List<DiscElement>();
        private List<string> pathsToCopy = new List<string>();
        public ListOfFilesPanelView()
        {
            InitializeComponent();

            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo d in drives)
                {
                    DriveData disc = new DriveData(d.VolumeLabel, d.Name);
                    DrivesList.Add(disc);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            driveCharSelect.ItemsSource = DrivesList;
        }

        public List<String> PathsToCopy
        {
            get
            {
                return  pathsToCopy;
            }
        }

        public string PanelPath()
        {
            return pathForDiscElements.Text;
        }

        private void createFileList(string path)
        {
            string parentPath = path;
            MyDirectory myDirectory = new MyDirectory(parentPath);
            listOfDiscElements.Children.Clear();
            List<DiscElement> allFiles = myDirectory.GetSubDiscElements();
            foreach (DiscElement file in allFiles)
            {
                string prePath = file.Path;
                string postPath = prePath.Replace(parentPath + @"\", "");
                DiscElementView discElementView = new DiscElementView(file);
                listOfDiscElements.Children.Add(discElementView);
                discElementView.openDiscElementClick += createFileListEventHandler;
                discElementView.checkedDiscElementEvent += checkedDiscElementEventHandler;
            }
            FileSystemWatcher filesystemwatcher = new FileSystemWatcher(parentPath);
            filesystemwatcher.Created += FileSystemWatcher_Created;
            filesystemwatcher.Deleted += FileSystemWatcher_Created;
            filesystemwatcher.Renamed += FileSystemWatcher_Created;
            filesystemwatcher.EnableRaisingEvents = true;
        }

        private void sortFileList(object sender, RoutedEventArgs e)
        {
            string parentPath = PanelPath();
            MyDirectory myDirectory = new MyDirectory(parentPath);
            listOfDiscElements.Children.Clear();
            List<DiscElement> allFiles = myDirectory.GetSubDiscElements();
            for (int i = 1; i < allFiles.Count; ++i) allFiles.Sort();
            foreach (DiscElement file in allFiles)
            {
                string prePath = file.Path;
                string postPath = prePath.Replace(parentPath + @"\", "");
                DiscElementView discElementView = new DiscElementView(file);
                listOfDiscElements.Children.Add(discElementView);
                discElementView.openDiscElementClick += createFileListEventHandler;
                discElementView.checkedDiscElementEvent += checkedDiscElementEventHandler;
            }
            FileSystemWatcher filesystemwatcher = new FileSystemWatcher(parentPath);
            filesystemwatcher.Created += FileSystemWatcher_Created;
            filesystemwatcher.Deleted += FileSystemWatcher_Created;
            filesystemwatcher.Renamed += FileSystemWatcher_Created;
            filesystemwatcher.EnableRaisingEvents = true;
        }

        private void checkedDiscElementEventHandler()
        {
            int counter = 0;
            foreach (UIElement discView in listOfDiscElements.Children)
            {
                if (((DiscElementView)discView).checkedDiscElement.IsChecked.Value)
                {
                    counter++;
                    pathsToCopy.Add(((DiscElementView)discView).FullPath);
                }
                else if (counter == 0)
                {
                    pathsToCopy.Clear();
                }
            }
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(() =>
            {
                getDiscElementList();
            }));
        }

        private void getDiscElementList()
        {
            createFileList(pathForDiscElements.Text);
        }

        private void createFileListEventHandler(DiscElement discElement)
        {
            pathForDiscElements.Text = discElement.Path;
            createFileList(pathForDiscElements.Text);
        }

        private void createNewFolder(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(pathForDiscElements.Text + @"\NewFolder");
        }

        private void pathForDiscElements_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                createFileList(pathForDiscElements.Text);
            }
        }

        private void driveCharSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            createFileList(pathForDiscElements.Text);
        }

        private void searchForFile()
        {
            string parentPath = PanelPath();
            string destFile = searchForFiles.Text;
            MyDirectory myDirectory = new MyDirectory(parentPath);
            listOfDiscElements.Children.Clear();
            List<DiscElement> allFiles = myDirectory.GetSubDiscElements();
            var filteredFiles = allFiles.Where(f => f.Name.Contains(destFile));
            foreach (DiscElement file in filteredFiles)
            {
                string prePath = file.Path;
                DiscElementView discElementView = new DiscElementView(file);
                listOfDiscElements.Children.Add(discElementView);
                discElementView.openDiscElementClick += createFileListEventHandler;
                discElementView.checkedDiscElementEvent += checkedDiscElementEventHandler;
                discElementView.imgPopupMouseEnterEvent += imgPopupMouseEnterEventHandler;
                discElementView.imgPopupMouseLeaveEvent += imgPopupMouseLeaveEventHandler;
                discElementView.imgPopupMouseOverEvent += imgPopupMouseOverEventHandler;
            }
            FileSystemWatcher filesystemwatcher = new FileSystemWatcher(parentPath);
            filesystemwatcher.Created += FileSystemWatcher_Created;
            filesystemwatcher.Deleted += FileSystemWatcher_Created;
            filesystemwatcher.Renamed += FileSystemWatcher_Created;
            filesystemwatcher.EnableRaisingEvents = true;
        }

        private void imgPopupMouseEnterEventHandler(BitmapImage source)
        {
            //Popup imgPopup = new Popup(); 
            imgPopup.Height = (source.Height) / 5;
            imgPopup.Width = (source.Width) / 5;
            imgPopup.IsOpen = true;
            imgPopup.PlacementTarget = listOfDiscElements;
            imgPopup.Child = new Image
            {
                Source = source,
                VerticalAlignment = VerticalAlignment.Center
            };  
        }

        private void imgPopupMouseLeaveEventHandler()
        {
            imgPopup.Height = 0;
            imgPopup.Width = 0;
            imgPopup.IsOpen = false;
        }

        private void imgPopupMouseOverEventHandler(MouseEventArgs e)
        {
            imgPopup.HorizontalOffset = e.GetPosition(this).X;
            imgPopup.VerticalOffset = e.GetPosition(this).Y;
            imgPopup.IsOpen = true;
        }

        private void searchForFiles_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchForFile();
        }

        private void goUp_Click(object sender, RoutedEventArgs e)
        {
            string path = pathForDiscElements.Text;
            if (System.IO.Path.GetDirectoryName(path) != null)
            {
                path = System.IO.Path.GetDirectoryName(path);
            }
            pathForDiscElements.Text = path;
            createFileList(path);
        }
    }
}
