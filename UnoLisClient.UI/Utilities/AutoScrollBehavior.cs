using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace UnoLisClient.UI.Utilities
{
    public static class AutoScrollBehavior
    {
        public static readonly DependencyProperty AutoScrollToBottomProperty =
            DependencyProperty.RegisterAttached(
                "AutoScrollToBottom",
                typeof(bool),
                typeof(AutoScrollBehavior),
                new PropertyMetadata(false, OnAutoScrollToBottomChanged));

        public static bool GetAutoScrollToBottom(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToBottomProperty);
        }

        public static void SetAutoScrollToBottom(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToBottomProperty, value);
        }

        private static void OnAutoScrollToBottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                var autoScroll = (bool)e.NewValue;

                if (autoScroll)
                {
                    if (listBox.Items != null)
                    {
                        ((INotifyCollectionChanged)listBox.Items).CollectionChanged += (s, args) =>
                        {
                            if (args.Action == NotifyCollectionChangedAction.Add && listBox.Items.Count > 0)
                            {
                                listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
                            }
                        };
                    }
                }
            }
        }
    }
}