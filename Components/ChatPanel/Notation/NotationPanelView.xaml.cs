using System.Windows.Controls;

namespace UFIP.EngChat.Components.ChatPanel.Notation
{
    /// <summary>
    /// NotationView.xaml interacts with its datacontext binding the note (:Note) 
    /// given to the conversation; and binding the command (:Record) to the button
    /// </summary>
    public partial class NotationPanelView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotationPanelView"/> class.
        /// </summary>
        public NotationPanelView()
        {
            InitializeComponent();
        }
    }
}
