using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using LibGit2Sharp;

namespace GitStache
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SaveConfiguration _configuration;

        public MainWindow()
        {
            _configuration = new SaveConfiguration().ReadFromFile();

            InitializeComponent();

            if (!string.IsNullOrEmpty(_configuration.CurrentRepositoryFilepath))
            {
                this.PopulateGitInfo();
            }
        }

        private void CurrentRepoLabel_DragOver(object sender, DragEventArgs e)
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

        private void CurrentRepoLabel_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as Label;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void CurrentRepoLabel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    // I only want folders
                    // TODO: Deal with stupid crap from users
                    _configuration.CurrentRepositoryFilepath = files[0];
                    
                    _configuration.SaveToFile();
                    this.PopulateGitInfo();
                }
            }

            var listbox = sender as Label;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void Delete_Click(object sender, RoutedEventArgs args)
        {
            using (var repo = new Repository(_configuration.CurrentRepositoryFilepath))
            {
                repo.Stashes.Remove(StachesListBox.SelectedIndex);
            }
           
        }

        private void Apply_Click(object sender, RoutedEventArgs args)
        {
            using (var repo = new Repository(_configuration.CurrentRepositoryFilepath))
            {
                var stash = repo.Stashes[StachesListBox.SelectedIndex];
            }
        }

        private void PopulateGitInfo()
        {
            CurrentRepoLabel.Content = this._configuration.CurrentRepositoryFilepath;
            using (var repo = new Repository(_configuration.CurrentRepositoryFilepath))
            {
                
                foreach (var stash in repo.Stashes)
                {
                    StachesListBox.Items.Add(stash.Message);
                }
            }
        }
    }
}