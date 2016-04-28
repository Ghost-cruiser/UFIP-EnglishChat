using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace UFIP.EngChat.Components.ChatPanel.Conversations.Messages
{
    /// <summary>
    /// Logique d'interaction pour ListBoxMessages.xaml
    /// </summary>
    public partial class MessagesPanel : ItemsControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesPanel"/> class.
        /// </summary>
        public MessagesPanel()
        {
            InitializeComponent();
            ItemTemplateSelector = new MessagesTemplateSelector(this);
        }
        /// <summary>
        /// Raises the <see cref="E:ItemsChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            ScrollViewer myScrollviewer = FindVisualChild<ScrollViewer>(this);
            if (myScrollviewer != null)
                myScrollviewer.PageDown();
            base.OnItemsChanged(e);
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
         where childItem : DependencyObject
        {
            // Iterate through all immediate children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is childItem)
                    return (childItem)child;

                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);

                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }
    }
}
