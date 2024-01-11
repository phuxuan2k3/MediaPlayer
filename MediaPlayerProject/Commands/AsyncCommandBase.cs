using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool isExecuting;
        private bool IsExecuting
        {
            get { return isExecuting; }
            set
            {
                isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting &&  base.CanExecute(parameter);
        }
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);

            }finally { IsExecuting = false; }
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}
