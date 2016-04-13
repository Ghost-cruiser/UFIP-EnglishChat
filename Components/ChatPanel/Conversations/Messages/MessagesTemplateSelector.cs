using S22.Xmpp.Im;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UFIP.EngChat.Components.ChatPanel.Conversations.Messages
{
    public class MessagesTemplateSelector : DataTemplateSelector
    {
        private static MessagesPanel Caller { get; set; }
        
        private S22.Xmpp.Jid sender { get; } = Common.Sources.UserSource.Center.ConnectedUser.Jid;

        public MessagesTemplateSelector(MessagesPanel caller)
        {
            Caller = caller;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            System.Windows.FrameworkElement element = container as System.Windows.FrameworkElement;

            if (element != null && item != null && item is Message)
            {
                Message messageItem = (Message)item;

                if (messageItem != null)
                {
                    if (messageItem.From == sender)
                    {
                        return
                            Caller.FindResource("OwnMessages") as System.Windows.DataTemplate;
                    }

                    else
                    {

                        return
                            Caller.FindResource("OthersMessages") as System.Windows.DataTemplate;
                    }
                }

            }

            return null;
        }
    }
}

