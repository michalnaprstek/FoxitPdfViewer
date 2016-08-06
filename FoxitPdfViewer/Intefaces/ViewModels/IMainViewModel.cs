using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace FoxitPdfViewer.Intefaces.ViewModels
{
  /// <summary>
  /// Interface for main view model.
  /// </summary>
  public interface IMainViewModel
  {
    /// <summary>
    /// Command method for selecting folder.
    /// </summary>
    void SelectFolder();

    /// <summary>
    /// Opens given file in view port.
    /// </summary>
    /// <param name="file">File to display.</param>
    void OpenFile(StorageFile file);

    /// <summary>
    /// Guard method for <see cref="NextPage"/> command method.
    /// </summary>
    bool CanNextPage { get; }

    /// <summary>
    /// Navigates to next page of displayed document.
    /// </summary>
    void NextPage();

    /// <summary>
    /// Guard method for <see cref="PreviousPage"/> command method.
    /// </summary>
    bool CanPreviousPage { get; }

    /// <summary>
    /// Navigates to previous page of displayed document.
    /// </summary>
    void PreviousPage();

    /// <summary>
    /// Total page count of the document.
    /// </summary>
    int PageCount { get; set; }

    /// <summary>
    /// Number of curretly displayed page.
    /// </summary>
    int CurrentPageNumber { get; set; }

    /// <summary>
    /// Bitmap of curretly displayed page.
    /// </summary>
    WriteableBitmap CurrentPageBitmap { get; set; }

    /// <summary>
    /// Width of curretly displayed page.
    /// </summary>
    int PageWidth { get; set; }

    /// <summary>
    /// Height of curretly displayed page.
    /// </summary>
    int PageHeight { get; set; }

    /// <summary>
    /// Display name of the document.
    /// </summary>
    string DocumentTitle { get; set; }

    /// <summary>
    /// Indicates whether is currently loaded any document or not.
    /// </summary>
    bool IsDocumentLoaded { get; }

    /// <summary>
    /// Controls displaying navigation page.
    /// </summary>
    bool IsPaneOpen { get; set; }

    /// <summary>
    /// Command method displaying and hiding navigation pane.
    /// </summary>
    void TogglePane();

    /// <summary>
    /// Name of current folder documents are listed from.
    /// </summary>
    string CurrentFolderName { get; set; }

    /// <summary>
    /// List of documents can be displayed.
    /// </summary>
    List<StorageFile> Files { get; set; } 
  }
}
