using S22.Xmpp.Im;
using System.Windows.Controls;

namespace UFIP.EngChat.Components.ChatPanel.Conversations.Messages
{
    /// <summary>
    /// Selects the template to use for a message, depending on if the user is the sender or not.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.DataTemplateSelector" />
    public class MessagesTemplateSelector : DataTemplateSelector
    {
        private static MessagesPanel Caller { get; set; }
        
        private S22.Xmpp.Jid sender { get; } = Common.Sources.UserSource.Center.ConnectedUser.Jid;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesTemplateSelector"/> class.
        /// </summary>
        /// <param name="caller">The caller.</param>
        public MessagesTemplateSelector(MessagesPanel caller)
        {
            Caller = caller;
        }

        /// <summary>
        /// Selects the template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="container">The container.</param>
        /// <returns></returns>
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

