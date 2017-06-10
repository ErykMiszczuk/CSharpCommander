﻿using CSharpCommander.DataModels;
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
        //public List<string> PathsToCopy(string path) { }


        //{
        //    get
        //    {
        //        return pathsToCopy;
        //    }

        //    set
        //    {
        //        pathsToCopy.Add(path);
        //    }
        //};

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
        
        private void checkedDiscElementEventHandler()
        {
            //MainWindow win = (MainWindow)Application.Current.MainWindow;
            int counter = 0;
            foreach (UIElement discView in listOfDiscElements.Children)
            {
                if (((DiscElementView)discView).checkedDiscElement.IsChecked.Value)
                {
                    counter++;
                    //var source = discView as DiscElement;
                    pathsToCopy.Add(((DiscElementView)discView).FullPath);
                    //pathsToCopy.Add(((DiscElementView)discView).pathOfDiscElement.Text + ((DiscElementView)discView).typeOfDiscElement.Text);
                    //PathsToCopy(((DiscElementView)discView).pathOfDiscElement.Text);
                }
                else if (counter == 0)
                {
                    pathsToCopy.Clear();
                }
            }
            //label.Content = $"Obiektów zaznaczono {counter}";
        }
        //private DiscElementView parentPath(string path)
        //{
        //    string parentPath = Convert.ToString(Directory.GetParent(path));
        //    DiscElement goUp = new MyDirectory(parentPath);
        //    return goUp;
        //}

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

        private void Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show(Convert.ToString(e.Source.Text));
            var source = e.OriginalSource as TextBox;

            if (source == null)
                return;

            //MessageBox.Show(source.Text);
            try
            {
                createFileList(source.Text);
            }
            catch { }
            
        }
    }
}
