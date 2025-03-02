using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace ls
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentBox.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Выберите папку";
                fbd.ShowDialog();
                try
                {
                    ContentBox.Text = fbd.SelectedPath;
                }
                catch
                {

                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string path = ContentBox.Text;
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    List<string> files = new List<string>();
                    if (Fi.IsChecked == true)
                    {
                        string[] pathFiles = Directory.GetFiles(path);
                        foreach (string file in pathFiles)
                        {
                            if (Ex.IsChecked == false)
                            {
                                if (Path.GetFileNameWithoutExtension(file) == "")
                                {
                                    files.Add(Path.GetFileName(file));
                                }
                                else
                                {
                                    files.Add(Path.GetFileNameWithoutExtension(file));
                                }
                            }
                            else
                            {
                                files.Add(Path.GetFileName(file));
                            }
                        }
                        files.Remove("desktop");
                        files.Remove("desktop.ini");
                    }
                    if (Fo.IsChecked == true)
                    {
                        string[] pathFiles = Directory.GetDirectories(path);
                        foreach (string file in pathFiles)
                        {
                            files.Add("./" + Path.GetFileName(file));
                        }
                    }
                    files.Sort();
                    string output = string.IsNullOrEmpty(Pa.Text) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "list.md") : Pa.Text;
                    using (StreamWriter sw = new StreamWriter(output))
                    {
                        foreach (string file in files)
                        {
                            sw.WriteLine("- " + file);
                        }
                        sw.WriteLine();
                    }
                    System.Windows.MessageBox.Show("Готово!");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Путь не выбран");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Pa.Text = dialog.FileName;
            }
        }
    }
}