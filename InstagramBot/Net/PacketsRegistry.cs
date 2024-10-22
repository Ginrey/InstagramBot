﻿#region

using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Net.Packets;

#endregion

namespace InstagramBot.Net
{
    public class PacketsRegistry
    {
        readonly Dictionary<States, IActionPacket> packets = new Dictionary<States, IActionPacket>();

        public PacketsRegistry(Session session)
        {
            Session = session;
            packets[States.SelectLanguage] = new OnSelectLanguage {Session = session};
            packets[States.Registering] = new OnRegistering {Session = session};
            packets[States.WaitUrlFrom] = new OnGetFromReferal {Session = session};
            packets[States.WaitUrl] = new OnWaitUrl {Session = session};
            packets[States.WaitSubscribe] = new OnWaitSubscribe {Session = session};
            packets[States.Done] = new OnDone {Session = session};
            packets[States.OnAlreadyUsing] = new OnAlreadyUsing {Session = session};
            packets[States.OnFindClients] = new OnFindClients {Session = session};
            packets[States.Blocked] = new OnAlreadyUsing {Session = session};
            packets[States.ChangeLanguage] = new OnChangeLanguage {Session = session};
            packets[States.FirstStep] = new OnFirstStep {Session = session};
            packets[States.SecondStep] = new OnSecondStep {Session = session};
            packets[States.VipInstagram] = new OnVipInstagram {Session = session};
            packets[States.Multiaccount] = new OnMultiClients {Session = session};
        }

        public Session Session { get; set; }

        public IActionPacket GetPacketType(States state)
        {
            IActionPacket type;
            if (!TryGetPacketType(state, out type))
            {
                throw new Exception("Unknown action");
            }
            return type;
        }

        public bool TryGetPacketType(States state, out IActionPacket type)
        {
            return packets.TryGetValue(state, out type);
        }
    }
}