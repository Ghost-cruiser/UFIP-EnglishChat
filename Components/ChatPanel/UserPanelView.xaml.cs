using System.Windows.Controls;
using System.Windows.Data;

namespace UFIP.EngChat.Components.ChatPanel
{
    /// <summary>
    /// Logique d'interaction pour UserPanelView.xaml
    /// </summary>
    public partial class ChatPanelView : UserControl
    {
        public ChatPanelView()
        {
            InitializeComponent();
        }

        private void ContentPresenter_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            var i = e;
        }
    }
}
