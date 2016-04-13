using System;
using System.Collections.Generic;

namespace UFIP.EngChat.Common.Libraries
{
    public static class UICollection
    {
        public static void AddOnUI<T>(this ICollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            System.Windows.Application.Current.Dispatcher.BeginInvoke(addMethod, item);
        }
    }
}
