using System;
using System.Collections.Generic;

namespace UFIP.EngChat.Common.Libraries
{
    /// <summary>
    /// Exposes functions that allows threads to trigger propertyChanged.
    /// </summary>
    public static class UICollection
    {
        /// <summary>
        /// Adds the item of a collection bound to the UI.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="item">The item.</param>
        public static void AddOnUI<T>(this ICollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            System.Windows.Application.Current.Dispatcher.BeginInvoke(addMethod, item);
        }
        /// <summary>
        /// Removes the item of a collection bound to the UI.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="item">The item.</param>
        public static void RemoveOnUI<T>(this ICollection<T> collection, T item)
        {
            Action<T> removeMethod = collection.Remove<T>;
            System.Windows.Application.Current.Dispatcher.BeginInvoke(removeMethod, item);
        }
        private static void Remove<T>(this ICollection<T> collection, T item)
        {
            var IsDeleted = collection.Remove(item);
        }


    }
}
