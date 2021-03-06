﻿using System;

namespace Components.Ui.EventBus
{
    public interface IMessageEventHost
    {
        Type ViewType { get; }
        Type MessageType { get; }

        void Send(IMessageContainer message);

        void Subscribe(Func<IMessageContainer, bool> receiverMethod);
        void RemoveEventMethod();
    }
}