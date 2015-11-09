using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceDemo;
using Ubiq.InterfaceAPI;

namespace ServiceDemo
{
    partial class ServiceDemo
    {
        int _value;

        Queue<ServiceDemoMessageType> _queue;
        private readonly Dictionary<uint, int> _usersToSubscriptionID = new Dictionary<uint, int>();

        protected async Task UserSection()
        {
            _value = 0;
            _queue = new Queue<ServiceDemoMessageType>();

            _serviceOutPort0.MessageReceived += _serviceOutPort0_MessageReceived;

            for (; ; )
            {
                while (_queue.Count > 0)
                {
                    var msg = _queue.Dequeue();
                    switch (msg.MessageType)
                    {
                    //case MessageType.Init:
                    // empty
                    case MessageType.IncData:
                        _value++;
                        NotifyDataUpdate();
                        break;
                    default:
                        // a lot of exceptions TO YOUR FACE!!!111
                        break;
                    }
                }
                await Wait();
            }
        }

        void NotifyDataUpdate()
        {
            foreach (int subscriptionId in _usersToSubscriptionID.Values)
            {
                var msg = new ServiceDemoMessageType(0, MessageType.DataUpdated)
                {
                    subscriptionID = subscriptionId,
                    Value = _value,
                };
                _serviceOutPort0.Send(msg);
            }
        }

        void _serviceOutPort0_MessageReceived(UbiqInterface sender, EventArgs<IntMessage> e)
        {
            var msg = e.Value as ServiceDemoMessageType;
            if (msg == null)
                return;

            _queue.Enqueue(msg);
            _usersToSubscriptionID[msg.ClientId] = msg.subscriptionID;
        }
    }
}







