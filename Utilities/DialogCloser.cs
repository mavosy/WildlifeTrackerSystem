using System.Windows;

namespace WTS.Utilities
{
    public static class DialogCloser
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        public static void SetDialogResult(DependencyObject target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        public static bool? GetDialogResult(DependencyObject target)
        {
            return (bool?)target.GetValue(DialogResultProperty);
        }

        private static void DialogResultChanged(DependencyObject dependancyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependancyObject is Window window)
            {
                window.DialogResult = eventArgs.NewValue as bool?;
            }
        }
    }
}