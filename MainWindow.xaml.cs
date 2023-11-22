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

namespace WpfApptextformatting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormattingToSelection(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormattingToSelection(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormattingToSelection(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFormatting();
        }

        private void FontSizeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(((ComboBoxItem)fontSizeComboBox.SelectedItem)?.Content?.ToString(), out double fontSize))
            {
                ApplyFormattingToSelection(TextElement.FontSizeProperty, fontSize);
            }
        }

        private void ColorComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (colorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                ApplyFormattingToSelection(TextElement.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedItem.Content.ToString())));
            }
        }

        private void ApplyFormattingToSelection(DependencyProperty property, object value)
        {
            int startIndex, endIndex;
            if (int.TryParse(startIndexTextBox.Text, out startIndex) && int.TryParse(endIndexTextBox.Text, out endIndex) &&
                startIndex >= 0 && endIndex >= startIndex && endIndex <= formattedRichTextBox.Document.ContentStart.GetOffsetToPosition(formattedRichTextBox.Document.ContentEnd))
            {
                TextRange selection = new TextRange(formattedRichTextBox.Document.ContentStart.GetPositionAtOffset(startIndex),
                                                    formattedRichTextBox.Document.ContentStart.GetPositionAtOffset(endIndex));
                selection.ApplyPropertyValue(property, value);
            }
            else
            {
                ClearFormatting();
            }
        }

        private void ClearFormatting()
        {
            formattedRichTextBox.SelectAll();
            formattedRichTextBox.Selection.ClearAllProperties();
        }
    }
}