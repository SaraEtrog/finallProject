using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ui.Hub
{
    public class MyHub1 : Microsoft.AspNet.SignalR.Hub
    {
        public Task Driver(int id,string mess)
        {
            return Clients.Group(id.ToString()).SendToDriver(mess);
        }

        public Task JoinGroup(int id)
        {
            return Groups.Add(Context.ConnectionId, id.ToString());
        }

        public Task LeaveGroup(int id)
        {
            return Groups.Remove(Context.ConnectionId, id.ToString());
        }
    }
}