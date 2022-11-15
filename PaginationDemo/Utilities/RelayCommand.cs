using MVVMEssentials.Commands;

namespace PaginationDemo.Utilities
{
    public class RelayCommand : CommandBase
    {
        private readonly Action _callback;

        public RelayCommand(Action callback)
        {
            _callback = callback;
        }

        public override void Execute(object parameter)
        {
            _callback?.Invoke();
        }
    }
}
