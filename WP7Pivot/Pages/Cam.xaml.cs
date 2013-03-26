using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace WP7Pivot.Pages
{
    public partial class Cam : PivotItem
    {
        CameraCaptureTask cameraCaptureTask;
        public Cam()
        {
            InitializeComponent();
            cameraCaptureTask = new CameraCaptureTask();
            //cameraCaptureTask.Completed += cameraCaptureTask_Completed;
            cameraCaptureTask.Completed += delegate(object sender, PhotoResult e)
            {

                if (e.TaskResult == TaskResult.OK)
                {
                    //MessageBox.Show(e.ChosenPhoto.Length.ToString());

                    //Code to display the photo on the page in an image control named myImage.
                    System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                    bmp.SetSource(e.ChosenPhoto);
                    myImage.Source = bmp;
                }
            };
        }

        private void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show(e.ChosenPhoto.Length.ToString());

                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                myImage.Source = bmp;
            }

        }

        private void takePicte_Click(object sender, RoutedEventArgs e)
        {
            cameraCaptureTask.Show();
        }
    }
}
