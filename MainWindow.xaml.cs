using Microsoft.Win32;
using System.DirectoryServices;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<File> list_files = new List<File>();
        private int currentIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(Environment.CurrentDirectory + "\\..\\..\\..\\files");
            textBox_fileContent.IsEnabled = false;
        }

        private void button_addFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "*.txt|*.txt";
            
            if (ofd.ShowDialog() == true)
            {
                bool alreadyAdded = false;
                foreach (File file in list_files)
                    if (file.Path == ofd.FileName) alreadyAdded = true;
                if (alreadyAdded)
                    MessageBox.Show("File already added!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    File add = new File(ofd.SafeFileName, ofd.FileName);
                    list_files.Add(add);
                    listBox_files.Items.Add(add);
                }
            }
        }

        private void listBox_files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_files.SelectedIndex == -1) textBox_fileContent.IsEnabled = false;
            else textBox_fileContent.IsEnabled = true;

            
            File ent = (File)listBox_files.SelectedItem;
            string fileText;
            StreamReader strR = new StreamReader(ent.Path);
            fileText = strR.ReadToEnd();
            strR.Close();

            textBlock_fileName.Text = ent.Name + "\r\n" + ent.Path;
            textBox_fileContent.Text = fileText;
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_files.SelectedIndex != -1 && textBox_fileContent.IsEnabled == true)
            {
                File ent = (File)listBox_files.SelectedItem;
                if (MessageBox.Show($"Are you sure you want to save changes to {ent.Name}", "Save file", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    StreamWriter strW = new StreamWriter(ent.Path);

                    strW.Write(textBox_fileContent.Text);
                    MessageBox.Show("File saved!");

                    strW.Close();
                }
            }
        }

        private void textBox_fileContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkBox_autosave.IsChecked == true)
            {
                StreamWriter strW = new StreamWriter(list_files[listBox_files.SelectedIndex].Path);
                strW.Write(textBox_fileContent.Text);
                strW.Close();
            }
        }
    }
}