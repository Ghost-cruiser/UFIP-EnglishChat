using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UFIP.EngChat.Components.ChatPanel.Conversations
{
    /// <summary>
    /// ConversationView.xaml is a Tabitem representing a conversation between a user and one of his contact.
    /// Interacts with a ConversationViewModel.
    /// </summary>
    public partial class ConversationView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationView"/> class.
        /// </summary>
        public ConversationView()
        {
            InitializeComponent();
        }
    }
}
