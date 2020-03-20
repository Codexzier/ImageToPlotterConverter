using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace Components.Ui.EventBus
{
    /// <summary>
    /// Eventbus singleton. can only one instance exist for the application
    /// </summary>
    public class EventbusManager
    {
        public static EventbusManager eventbus;

        private readonly IDictionary<Type, IList<IMessageEventHost>> _messageContainers = new Dictionary<Type, IList<IMessageEventHost>>();

        private EventbusManager() { }

        public static EventbusManager GetEventbus()
        {
            if (eventbus == null)
            {
                eventbus = new EventbusManager();
            }

            return eventbus;
        }

        public int RegisteredCount => this._messageContainers.Count;

        public int RegisteredCountAll => this._messageContainers.Sum(c => c.Value.Count);

        public int RegisteredCountByView<TView>() where TView : DependencyObject
        {
            if(this._messageContainers.Count == 0)
            {
                return 0;
            }

            return this._messageContainers[typeof(TView)].Count;
        }

        /// <summary>
        /// create new internal instance host for message event. 
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="receiverMethod"></param>
        public void Register<TView, TMessage>(Func<IMessageContainer, bool> receiverMethod) 
            where TView : DependencyObject
            where TMessage : IMessageContainer
        {

            var host = new MessageEventHost<TView, TMessage>();
            host.Subscribe(receiverMethod);

            if (!this._messageContainers.ContainsKey(host.ViewType))
            {
                this._messageContainers.Add(host.ViewType, new List<IMessageEventHost>());
            }

            if (this._messageContainers[host.ViewType].Any(a => a.MessageType == host.MessageType))
            {
                throw new EventbusException($"can not register one moretime to the viewType {host.ViewType.Name} about message type: {host.MessageType.Name}");
            }

            this._messageContainers[host.ViewType].Add(host);
        }

        /// <summary>
        /// Deregister closing content. Every view need an dispose interface to cleanup events and unused references.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        public void Deregister<TView>()
        {
            if (this._messageContainers.ContainsKey(typeof(TView)))
            {
                foreach (var item in this._messageContainers[typeof(TView)])
                {
                    item.RemoveEventMethod();
                }
                this._messageContainers.Remove(typeof(TView));
            }
        }

        /// <summary>
        /// Open new instance of a view. The view must setup the viewModel to the DataContext.
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        public void OpenView<TView>(IMessageContainer message = null)
        {
            var t = typeof(TView);
            if (!this._messageContainers.ContainsKey(t))
            {
                this.OpenViewEvent?.Invoke((TView)Activator.CreateInstance(typeof(TView)));
            }

            if(message != null)
            {
                this.Send<TView, IMessageContainer>(message);
            }
        }

        public delegate void OpenViewEventHandler(object obj);
        public event OpenViewEventHandler OpenViewEvent;

        public bool Send<TView, TMessageType>(TMessageType message, bool openView = false) where TMessageType : IMessageContainer
        {
            foreach (var itemEventHosts in this._messageContainers)
            {
                if (itemEventHosts.Key != typeof(TView))
                {
                    continue;
                }

                foreach (var itemEventHost in itemEventHosts.Value)
                {
                    if (itemEventHost.MessageType != message.GetType())
                    {
                        continue;
                    }

                    itemEventHost.Send(message);
                    return true;
                }
            }

            if (this.OpenNewView<TView, TMessageType>(message, openView))
            {
                return true;
            }

            throw new EventbusException($"Not found or registered. View: {typeof(TView).Name}, {typeof(TMessageType).Name}");
        }

        private bool OpenNewView<TView, TMessageContainer>(TMessageContainer message, bool openView) where TMessageContainer : IMessageContainer
        {
            if (openView)
            {
                this.OpenView<TView>(message);

                if (this.Send<TView, TMessageContainer>(message))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
