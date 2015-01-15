/**
 * This is pretty much a copy of Phillip Sumi's CloseWindowCommand.  Just in my namespace.
 * http://www.hardcodet.net/wpf-notifyicon
 * Do not attribute this work to me.
 */

using System.Windows.Input;

namespace GitStache.Commands
{
    class CloseWindowCommand : CommandBase<CloseWindowCommand>
    {
        public override void Execute(object parameter)
        {
            GetTaskbarWindow(parameter).Close();
            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            var win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}
