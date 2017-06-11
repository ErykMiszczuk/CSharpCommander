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

namespace CSharpCommander.View
{
    /// <summary>
    /// Logika interakcji dla klasy DiscElementView.xaml
    /// </summary>
    public partial class DiscElementView : UserControl
    {
        DiscElement discElement;
        private string fullPath;

        public string FullPath { get => fullPath; }

        public DiscElementView(DiscElement discElement)
        {
            this.discElement = discElement;

            fullPath = discElement.Path;

            InitializeComponent();

            pathOfDiscElement.Text = discElement.Name;

            pathOfDiscElement.Text = System.IO.Path.GetFileNameWithoutExtension(discElement.Path);

            try
            {

                if (discElement is MyDirectory)
                {
                    typeOfDiscElement.Text = "Directory " + Directory.GetDirectories(discElement.Path).Length + "  ";
                }
                else
                {
                    typeOfDiscElement.Text = System.IO.Path.GetExtension(discElement.Path);
                }

            }
            catch (Exception) { }
            finally
            {
                creationTimeOfDiscElement.Text = discElement.CreationTime.ToString();
            }
        }

        public delegate void OpenDiscElementClick(DiscElement discElement);
        public event OpenDiscElementClick openDiscElementClick;
        private void OpenDiscElement(object sender, MouseButtonEventArgs e)
        {
            if (discElement is MyDirectory)
            {
                if (openDiscElementClick != null)
                {
                    openDiscElementClick.Invoke(discElement);
                }
            }
            else
            {
                System.Diagnostics.Process.Start(discElement.Path);
            }
        }
        public delegate void checkedDiscElementClick();
        public event checkedDiscElementClick checkedDiscElementEvent;
        private void checkedDiscElement_Click(object sender, RoutedEventArgs e)
        {
            if(checkedDiscElementEvent != null)
            {
                checkedDiscElementEvent.Invoke();
            }
        }

        public delegate void imgPopupMouseEnter(BitmapImage source);
        public event imgPopupMouseEnter imgPopupMouseEnterEvent;

        private void pathOfDiscElement_MouseEnter(object sender, MouseEventArgs e)
        {
            if (typeOfDiscElement.Text == ".jpg")
            {
                BitmapImage source = new BitmapImage(new Uri(FullPath));
                if (imgPopupMouseEnterEvent != null)
                {
                    imgPopupMouseEnterEvent.Invoke(source);
                }
            }
        }

        public delegate void imgPopupMouseLeave();
        public event imgPopupMouseLeave imgPopupMouseLeaveEvent;

        private void pathOfDiscElement_MouseLeave(object sender, MouseEventArgs e)
        {
            if (typeOfDiscElement.Text == ".jpg")
            {
                if (imgPopupMouseLeaveEvent != null)
                {
                    imgPopupMouseLeaveEvent.Invoke();
                }
            }
        }

        public delegate void imgPopupMouseOver(MouseEventArgs e);
        public event imgPopupMouseOver imgPopupMouseOverEvent;

        private void pathOfDiscElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (typeOfDiscElement.Text == ".jpg")
            {
                if (imgPopupMouseOverEvent != null)
                {
                    imgPopupMouseOverEvent.Invoke(e);
                }
            }
        }
    }
}
