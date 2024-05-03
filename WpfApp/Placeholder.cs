using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFMultiLanguage
{
    /// <summary>
    /// xmlns:com="clr-namespace:Common"
    /// <TextBox x:Name="userName" Width="240" com:Placeholder.Text="Please enter..." />
    /// </summary>
    public class Placeholder : DependencyObject
    {
        private static readonly Dictionary<TextBox, VisualBrush> TxtBrushes = new Dictionary<TextBox, VisualBrush>();

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(Placeholder),
                new PropertyMetadata("", OnPlaceholderChanged));

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static void OnPlaceholderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if ((txtBox != null) && (!TxtBrushes.ContainsKey(txtBox)))
            {
                var phBox = new TextBox();
                var binding = new Binding
                {
                    Source = txtBox,
                    Path = new PropertyPath("(0)", TextProperty)
                };
                phBox.SetBinding(TextBox.TextProperty, binding);
                phBox.Opacity = 0.8;
                phBox.Padding = new Thickness(3, 0, 0, 0);
                phBox.BorderThickness = new Thickness(0, 0, 0, 0);
                phBox.Foreground = Brushes.Gray;
                phBox.Background = Brushes.Transparent;

                var phVisualBrush = new VisualBrush
                {
                    AlignmentX = AlignmentX.Left,
                    Stretch = Stretch.None,
                    Visual = phBox
                };

                txtBox.Background = phVisualBrush;
                txtBox.TextChanged += PlaceholderTextBox_TextChanged;
                txtBox.Unloaded += PlaceholderTextBox_Unloaded;

                TxtBrushes.Add(txtBox, phVisualBrush);
            }
        }

        private static void PlaceholderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if ((txtBox != null) && (TxtBrushes.ContainsKey(txtBox)))
            {
                var placeholderVisualBrush = TxtBrushes[txtBox];
                txtBox.Background = string.IsNullOrEmpty(txtBox.Text) ? placeholderVisualBrush : null;
            }
        }

        private static void PlaceholderTextBox_Unloaded(object sender, RoutedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if ((txtBox != null) && (TxtBrushes.ContainsKey(txtBox)))
            {
                TxtBrushes.Remove(txtBox);

                txtBox.TextChanged -= PlaceholderTextBox_TextChanged;
                txtBox.Unloaded -= PlaceholderTextBox_Unloaded;
            }
        }

    }
}
