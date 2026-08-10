using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// Provides internal event mechanism.
    /// </summary>
    internal class TreeViewEventAggregator
    {
        internal IDictionary<string, List<Func<Task, object, Task>>> _eventAsyncList = new Dictionary<string, List<Func<Task, object, Task>>>();

        internal async Task NotifyAsync(string name, object args)
        {
            if (_eventAsyncList.TryGetValue(name, out var handlers))
            {
                var taskToPass = Task.CompletedTask;
                var handlerTasks = handlers.Select(handler => handler(taskToPass, args));
                await Task.WhenAll(handlerTasks).ConfigureAwait(true);
            }
        }
        internal void AddAsync(string name, Func<Task, object, Task> handler)
        {
            if (!_eventAsyncList.TryGetValue(name, out List<Func<Task, object, Task>>? value))
            {
                _eventAsyncList.Add(name, new List<Func<Task, object, Task>>());
            }

            if (!_eventAsyncList[name].Contains(handler))
            {
                _eventAsyncList[name].Add(handler);
            }
        }
    }
}
