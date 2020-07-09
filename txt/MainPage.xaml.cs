using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Collections.Generic;
using Windows.Storage.Streams;
using Windows.Storage.Provider;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace txt
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int f = 1;
        public int r = 3;
        public MainPage() => this.InitializeComponent();
        private void TabView_AddTabButtonClick(TabView sender, object args) => NewFile();
        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args) => sender.TabItems.Remove(args.Tab);
        private void NewFileClick(object sender, RoutedEventArgs e) => NewFile();
        public void NewFile()
        {
            TabViewItem item = new TabViewItem { Header = $"new {f}" };
            RichEditBox rich = new RichEditBox
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            item.Content = rich;
            item.IsSelected = true;
            tab.TabItems.Add(item);
            f++;
        }
        private async void SaveFileClick(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });
            savePicker.SuggestedFileName = "New Document";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status != FileUpdateStatus.Complete)
                {
                    MessageDialog errorBox = new MessageDialog("File " + file.Name + " couldn't be saved.");
                    await errorBox.ShowAsync();
                }
            }
        }
        private void FrameCloseClick(object sender, RoutedEventArgs e) => tab.TabItems.Remove(tab.SelectedItem);
        private void AllFrameCloseClick(object sender, DoubleTappedRoutedEventArgs e) => tab.TabItems.Clear();
        private void CopyTextClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.Copy();
            }
            catch (Exception) { }
        }
        private void CutTextClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.Cut();
            }
            catch (Exception) { }
        }
        private void PasteTextClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.Paste(0);
            }
            catch (Exception) { }
        }
        private void TextClearClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
            }
            catch (Exception) { }
        }
        private void SelecteTextClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Focus(FocusState.Pointer);
                rich.Document.Selection.SetRange(0, rich.Document.Selection.EndPosition);
            }
            catch { }
        }
        private void UndoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Undo();
            }
            catch (Exception) { }
        }
        private void RedoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Redo();
            }
            catch (Exception) { }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Italic = Windows.UI.Text.FormatEffect.Toggle;
            }
            catch (Exception) { }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Bold = Windows.UI.Text.FormatEffect.Toggle;
            }
            catch (Exception) { }
        }
        private void TextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            try
            {
                if (int.Parse(Size.Text) > 0 && int.Parse(Size.Text) < 1639)
                {
                    RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                    rich.Document.Selection.CharacterFormat.Size = int.Parse(Size.Text);
                }
            }
            catch (Exception) { }
        }
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Underline = UnderlineType.Dash;
            }
            catch (Exception) { }
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Underline = UnderlineType.Dotted;
            }
            catch (Exception) { }
        }

        private void MenuFlyoutItem_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Underline = UnderlineType.Thin;
            }
            catch (Exception) { }
        }

        private void MenuFlyoutItem_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Underline = UnderlineType.Wave;
            }
            catch (Exception) { }
        }

        private void MenuFlyoutItem_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.CharacterFormat.Underline = UnderlineType.Thick;
            }
            catch { }
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FileOpenPicker open = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            open.FileTypeFilter.Add(".rtf");
            StorageFile file = await open.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.Read);
                    TabViewItem item = new TabViewItem { Header = $"new {f}" };
                    RichEditBox rich = new RichEditBox
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };
                    item.Content = rich;
                    item.IsSelected = true;
                    tab.TabItems.Add(item);
                    rich.Document.LoadFromStream(TextSetOptions.FormatRtf, randAccStream);
                }
                catch
                {
                }
            }
        }
        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                InfoAbout info = new InfoAbout();
                await info.ShowAsync();
            }
            catch { }
        }
        private void Grid_Loading(FrameworkElement sender, object args)
        {
            string[] font = CanvasTextFormat.GetSystemFontFamilies();
            for (int i = 0; i < font.Length; i++)
            {
                fontsss.Items.Add(font[i]);
            }
        }
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                await Windows.Graphics.Printing.PrintManager.ShowPrintUIAsync();
            }
            catch
            {
                MessageDialog c = new MessageDialog("Errrrrrrrrrrrrrrrrrrrrrrrrrrrrrr0r");
                await c.ShowAsync();
            }
        }
        TextBox text = new TextBox();
        ContentDialog dialog = new ContentDialog();
        private async void Button_Click_8(object sender, RoutedEventArgs e)
        {
            text.Width = 200;
            text.Height = 25;
            dialog.Content = text;
            dialog.Title = "Замена";
            dialog.IsPrimaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Подтвердить";
            dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
            await dialog.ShowAsync();
        }
        private void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                ITextSelection selectedText = rich.Document.Selection;
                if (findBox.Text != "")
                {
                    dialog.Content = text;
                    string t = text.Text;
                    if (t != "")
                    {
                        rich.Document.GetText(TextGetOptions.None, out string textStr);
                        var myRichEditLength = textStr.Length;
                        rich.Document.Selection.SetRange(0, myRichEditLength);
                        int i = 3;
                        while (i > 0)
                        {
                            i = rich.Document.Selection.FindText(findBox.Text, myRichEditLength, FindOptions.Case);
                            if (selectedText != null) selectedText.FormattedText.Text = t;
                        }
                        findBox.Text = "";
                        text.Text = "";
                    }
                }
            }
            catch { }
        }

        private void fontsss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Focus(FocusState.Pointer);
                rich.Document.Selection.CharacterFormat.Name = fontsss.SelectedItem.ToString();
            }
            catch { }
        }
        private void findBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            try
            {
                RichEditBox rich = ((tab.SelectedItem as TabViewItem).Content as RichEditBox);
                rich.Document.Selection.SetRange(0, rich.Document.Selection.EndPosition);
                rich.Document.Selection.CharacterFormat.BackgroundColor = Color.FromArgb(0, 0, 0, 0);
                rich.Document.Selection.SetRange(0, 0);
                ITextSelection selectedText = rich.Document.Selection;
                Color color = Color.FromArgb(255, 0, 255, 10);
                rich.Document.GetText(TextGetOptions.None, out string textStr);
                var myRichEditLength = textStr.Length;
                rich.Document.Selection.SetRange(0, myRichEditLength);
                int i = 1;
                while (i > 0)
                {
                    i = rich.Document.Selection.FindText(findBox.Text, myRichEditLength, FindOptions.Case);
                    if (selectedText != null)
                    {
                        selectedText.CharacterFormat.BackgroundColor = color;
                    }
                }
            }
            catch
            {
            }
        }
    }
}
