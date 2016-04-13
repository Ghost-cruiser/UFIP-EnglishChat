using S22.Xmpp.Im;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFIP.EngChat.Common.Models;

namespace UFIP.EngChat.Components.Contacts
{
    public class ContactList : ObservableCollection<User>
    {
        public bool IsListAdmin { get; private set; }

        //public ContactList(Roster roster)
        //{
        //    foreach (RosterItem rosterItem in roster)
        //    {
        //        Add(rosterItem);
        //    }
        //}

        private void RosterHandler(object sender, RosterUpdatedEventArgs e)
        {
            if (e.Removed)
            {
                Remove(e.Item);
            }
            else
            {
                var update = false;

                for (int i = 0; i < Count; i++)
                {

                    if (e.Item.Jid.Equals(this[i].Jid))
                    {
                        this[i] = e.Item;
                        update = true;
                        break;
                    }
                }
                if (!update)
                {
                    Add(e.Item);
                }
            }
        }

        public void UpdateStatus(string JID, Status newStatus)
        {
            foreach (var contact in this)
            {
                if (contact.Jid == JID)
                {
                    contact.CurrentStatus = newStatus;
                }
            }
        }
    }
}
