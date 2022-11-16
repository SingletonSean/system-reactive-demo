using MVVMEssentials.Commands;

namespace PaginationDemo.Utilities
{
    public class RelayCommand<T> : CommandBase
    {
        private readonly Action<T> _callback;

        public RelayCommand(Action<T> callback)
        {
            _callback = callback;
        }

        public override void Execute(object parameter)
        {
            _callback?.Invoke((T) parameter);
        }
    }
}
