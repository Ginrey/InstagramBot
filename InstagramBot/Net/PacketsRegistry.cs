using System;
using System.Collections.Generic;
using InstagramBot.Data;
using InstagramBot.Net.Packets;

namespace InstagramBot.Net
{
    public class PacketsRegistry
    {
        public Session Session { get; set; }
        Dictionary<States, ActionPacket> packets = new Dictionary<States, ActionPacket>();
        public PacketsRegistry(Session session)
        {
            Session = session;
            packets[States.Registering] = new OnRegistering { Session = session };
            packets[States.WaitUrl] = new OnWaitUrl { Session = session };
            packets[States.WaitSubscribe] = new OnWaitSubscribe { Session = session };
            packets[States.Done] = new OnDone { Session = session };
            packets[States.Blocked] = new OnBlocked { Session = session };
        }

        public ActionPacket GetPacketType(States state)
        {
            ActionPacket type;
            if (!TryGetPacketType(state, out type))
            {
                throw new Exception("Unknown action");
            }
            return type;
        }
        public bool TryGetPacketType(States state, out ActionPacket type)
        {
            return packets.TryGetValue(state, out type);
        }
    }
}
