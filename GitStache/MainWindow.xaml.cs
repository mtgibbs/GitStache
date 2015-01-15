using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibGit2Sharp;

namespace GitStache
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository _CurrentRepository;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as Label;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as Label;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void DropBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    // I only want folders
                    // TODO: Deal with stupid crap from users
                    CurrentRepoLabel.Content = files[0];
                    using (var repo = new Repository(files[0]))
                    {
                        _CurrentRepository = repo;

                        foreach (var stash in repo.Stashes)
                        {
                            StachesListBox.Items.Add(stash.Message);
                        }
                    }
                }
            }

            var listbox = sender as Label;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }
    }
}