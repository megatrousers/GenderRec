using GenderRec01.Model;
using Microsoft.Win32;
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

namespace GenderRec01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Supervisor sup;

        public MainWindow()
        {
            InitializeComponent();
            sup = Supervisor.Instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
             InitializeOpenFileDialog(ofd);
            if(ofd.ShowDialog() == true)
            {
                Supervisor.Instance.LoadImageSet(ofd.FileNames, BatchType.TESTING);
            }

            //display current image in the image frame
           // Image displayImage = new Image();
            

  
            SubjectBatch focusBatch = sup.SubjectBatches.Last().Value;
            Subject focusSubject; 
            sup.FocusSubjects.TryGetValue(focusBatch.Name, out focusSubject);
            //displayImage.Source = focusSubject.BaseImage;
            Image1.Source = focusSubject.BaseImage;
            
            
            foreach (Subject subject in focusBatch.Subjects.Values)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = subject.Name;
                CB1.Items.Add(cbi);
            }
            
        }

        //Method modified from https://msdn.microsoft.com/en-us/library/system.windows.forms.openfiledialog.multiselect(v=vs.110).aspx
        private void InitializeOpenFileDialog(OpenFileDialog ofd)
        {
            // Set the file dialog to filter for graphics files. 
            ofd.Filter =
                "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images. 
            ofd.Multiselect = true;
            ofd.Title = "My Image Browser";
        }

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             ComboBox cb = sender as ComboBox;
             if(cb != null)
             {
                 ComboBoxItem cbi = (ComboBoxItem)cb.SelectedItem;
                 string s = cbi.Content.ToString();
                 sup.ChangeFocusSubject(s);
                 UpdateDisplayImage();
             }
        }

        private void UpdateDisplayImage()
        {
            SubjectBatch focusBatch = sup.SubjectBatches.Last().Value;
            Subject focusSubject;
            sup.FocusSubjects.TryGetValue(focusBatch.Name, out focusSubject);
            //displayImage.Source = focusSubject.BaseImage;
            Image1.Source = focusSubject.BaseImage;
        }
    }
}
