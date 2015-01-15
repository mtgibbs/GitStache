using System.Windows.Input;

namespace GitStache.Commands
{
    public class ToggleWindowVisibilityCommand : CommandBase<ToggleWindowVisibilityCommand>
    {
        public override void Execute(object parameter)
        {
            var win = GetTaskbarWindow(parameter);

            if (win.IsVisible)
            {
                win.Hide();
                
            }
            else
            {
                win.Show();
                
            }
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
