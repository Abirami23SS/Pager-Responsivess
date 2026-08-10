using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations.Internal
{
    internal class EventAggregator
    {
        private Dictionary<string, Action<ToolbarEventArgs>> eventList = new Dictionary<string, Action<ToolbarEventArgs>>();

        internal IDictionary<string, List<Func<Task, object, Task>>> _eventAsyncList = new Dictionary<string, List<Func<Task, object, Task>>>();

        public void Notify(string name, ToolbarEventArgs args)
        {
            if (eventList.TryGetValue(name, out Action<ToolbarEventArgs> eventName))
            {
                eventName.Invoke(args);
            }
        }

        public void Add(string name, Action<ToolbarEventArgs> handler)
        {
            if (!eventList.TryAdd(name, handler))
            {
                eventList[name] = handler;
            }
        }

        internal void AddAsync(string name, Func<Task, object, Task> handler)
        {
            if (!_eventAsyncList.TryGetValue(name, out List<Func<Task, object, Task>> value))
            {
                _eventAsyncList.Add(name, new List<Func<Task, object, Task>>());
            }

            if (!_eventAsyncList[name].Contains(handler))
            {
                _eventAsyncList[name].Add(handler);
            }
        }

        internal async Task NotifyAsync(string name, object args)
        {
            if (_eventAsyncList.TryGetValue(name, out var handlers))
            {
                var taskToPass = Task.CompletedTask;
                var handlerTasks = handlers.Select(handler => handler(taskToPass, args));
                await Task.WhenAll(handlerTasks).ConfigureAwait(true);
            }
        }
    }
}