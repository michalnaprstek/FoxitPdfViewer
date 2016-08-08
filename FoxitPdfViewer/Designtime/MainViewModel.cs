using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using FoxitPdfViewer.Intefaces.ViewModels;

namespace FoxitPdfViewer.Designtime
{
  /// <summary>
  /// Design time view model.
  /// </summary>
  public class MainViewModel : IMainViewModel
  {
    public MainViewModel()
    {
      this.DocumentTitle = "Testovací soubor";
      this.PageWidth = 970;
      this.PageHeight = 686;
      this.CurrentPageNumber = 1;
      this.PageCount = 1;
      this.IsPaneOpen = true;
      this.CurrentFolderName = "Dokumenty";
      this.Files = new List<StorageFile>();
    }

    #region Implementation of IMainViewModel

    /// <summary>
    /// Command method for selecting folder.
    /// </summary>
    public void SelectFolder() { }

    /// <summary>
    /// Opens given file in view port.
    /// </summary>
    /// <param name="file">File to display.</param>
    public void OpenFile(StorageFile file) { }

    /// <summary>
    /// Guard method for <see cref="IMainViewModel.NextPage"/> command method.
    /// </summary>
    public bool CanNextPage => true;

    /// <summary>
    /// Navigates to next page of displayed document.
    /// </summary>
    public void NextPage() { }

    /// <summary>
    /// Guard method for <see cref="IMainViewModel.PreviousPage"/> command method.
    /// </summary>
    public bool CanPreviousPage => true;

    /// <summary>
    /// Navigates to previous page of displayed document.
    /// </summary>
    public void PreviousPage() { }

    /// <summary>
    /// Total page count of the document.
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// Number of curretly displayed page.
    /// </summary>
    public int CurrentPageNumber { get; set; }

    /// <summary>
    /// Bitmap of curretly displayed page.
    /// </summary>
    public WriteableBitmap CurrentPageBitmap { get; set; }

    /// <summary>
    /// Width of curretly displayed page.
    /// </summary>
    public int PageWidth { get; set; }

    /// <summary>
    /// Height of curretly displayed page.
    /// </summary>
    public int PageHeight { get; set; }

    /// <summary>
    /// Display name of the document.
    /// </summary>
    public string DocumentTitle { get; set; }

    /// <summary>
    /// Indicates whether is currently loaded any document or not.
    /// </summary>
    public bool IsDocumentLoaded => true;

    /// <summary>
    /// Controls displaying navigation page.
    /// </summary>
    public bool IsPaneOpen { get; set; }

    /// <summary>
    /// Command method displaying and hiding navigation pane.
    /// </summary>
    public void TogglePane() { }

    /// <summary>
    /// Name of current folder documents are listed from.
    /// </summary>
    public string CurrentFolderName { get; set; }

    /// <summary>
    /// List of documents can be displayed.
    /// </summary>
    public List<StorageFile> Files { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}
