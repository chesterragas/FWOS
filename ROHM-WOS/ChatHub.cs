﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.SignalR;
using ROHM_WOS.Models;

namespace ROHM_WOS
{
   
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string ConversationID)
        {
            string Timenow = DateTime.Now.ToString("HH:mm");
            
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, ConversationID, Timenow);
        }
    }
}