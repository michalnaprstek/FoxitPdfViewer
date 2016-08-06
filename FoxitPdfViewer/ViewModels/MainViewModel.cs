using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Foxit;
using Foxit.PDF;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using FoxitPdfViewer.Intefaces.Services;
using FoxitPdfViewer.Intefaces.ViewModels;

namespace FoxitPdfViewer.ViewModels
{
  /// <summary>
  /// View model for main view of application.
  /// </summary>
  public class MainViewModel : Screen, IMainViewModel
  {
    #region injected dependencies

    private readonly IDisplayCalculationService displayCalculationService;
    private readonly IErrorHandlingService errorHandlingService;

    #endregion // injected dependencies

    #region private fields

    private int currentPageNumber;

    private WriteableBitmap currentPageBitmap;
    
    private int pageCount;

    private int pageHeight;

    private int pageWidth;

    private Document document;

    private string documentTitle;

    private bool isPaneOpen;

    private string currentFolderName;

    private List<StorageFile> files;

    #endregion // private fields

    #region ctors

    public MainViewModel(IErrorHandlingService errorHandlingService,
      IDisplayCalculationService displayCalculationService)
    {
      this.errorHandlingService = errorHandlingService;
      this.displayCalculationService = displayCalculationService;
      this.IsPaneOpen = true;
    }

    #endregion // ctors

    #region public members
    
    /// <summary>
    /// Command method for selecting folder.
    /// </summary>
    public async void SelectFolder()
    {
      var picker = new FolderPicker
      {
        ViewMode = PickerViewMode.Thumbnail,
        SuggestedStartLocation = PickerLocationId.DocumentsLibrary
      };
      picker.FileTypeFilter.Add("*");

      await this.SetFolder(await picker.PickSingleFolderAsync());
    }

    /// <summary>
    /// Opens given file in view port.
    /// </summary>
    /// <param name="file">File to display.</param>
    public async void OpenFile(StorageFile file)
    {
      if (file == null)
        return;
      if (!await this.LoadDocument(file))
      {
        await this.errorHandlingService.HandleError("Document load failed.");
        return;
      }
      this.NotifyOfPropertyChange(() => this.IsDocumentLoaded);
      this.CurrentPageNumber = 1;
      this.PageCount = this.document.CountPages();
      var page = await this.LoadCurrentPage();
      await this.Render(page);
    }

    /// <summary>
    /// Guard method for <see cref="NextPage"/> command method.
    /// </summary>
    public bool CanNextPage => this.document != null && this.CurrentPageNumber < this.PageCount;
    
    /// <summary>
    /// Navigates to next page of displayed document.
    /// </summary>
    public async void NextPage()
    {
      if (!this.CanNextPage)
        return;

      this.CurrentPageNumber++;
      var page = await this.LoadCurrentPage();
      await this.Render(page);
    }

    /// <summary>
    /// Guard method for <see cref="PreviousPage"/> command method.
    /// </summary>
    public bool CanPreviousPage => this.document != null && this.CurrentPageNumber > 1;

    /// <summary>
    /// Navigates to previous page of displayed document.
    /// </summary>
    public async void PreviousPage()
    {
      if (!this.CanPreviousPage)
        return;

      this.CurrentPageNumber--;
      var page = await this.LoadCurrentPage();
      await this.Render(page);
    }

    /// <summary>
    /// Total page count of the document.
    /// </summary>
    public int PageCount
    {
      get { return this.pageCount; }
      set
      {
        this.pageCount = value;
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.PageCount)));
      }
    }

    /// <summary>
    /// Number of curretly displayed page.
    /// </summary>
    public int CurrentPageNumber
    {
      get { return this.currentPageNumber; }
      set
      {
        this.currentPageNumber = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.CurrentPageNumber)));
      }
    }

    /// <summary>
    /// Bitmap of curretly displayed page.
    /// </summary>
    public WriteableBitmap CurrentPageBitmap
    {
      get { return this.currentPageBitmap; }
      set
      {
        this.currentPageBitmap = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.CurrentPageBitmap)));
      }
    }

    /// <summary>
    /// Width of curretly displayed page.
    /// </summary>
    public int PageWidth
    {
      get { return this.pageWidth; }
      set
      {
        this.pageWidth = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.PageWidth)));
      }
    }

    /// <summary>
    /// Height of curretly displayed page.
    /// </summary>
    public int PageHeight
    {
      get { return this.pageHeight; }
      set
      {
        this.pageHeight = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.PageHeight)));
      }
    }

    /// <summary>
    /// Display name of the document.
    /// </summary>
    public string DocumentTitle
    {
      get { return this.documentTitle; }
      set
      {
        this.documentTitle = value;
        this.NotifyOfPropertyChange(nameof(this.DocumentTitle));
      }
    }

    /// <summary>
    /// Indicates whether is currently loaded any document or not.
    /// </summary>
    public bool IsDocumentLoaded => this.document != null;

    /// <summary>
    /// Controls displaying navigation page.
    /// </summary>
    public bool IsPaneOpen
    {
      get { return this.isPaneOpen; }
      set
      {
        this.isPaneOpen = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.IsPaneOpen)));
        this.RenderCurrentPage().AsResult();
      }
    }

    /// <summary>
    /// Command method displaying and hiding navigation pane.
    /// </summary>
    public void TogglePane()
    {
      this.IsPaneOpen = !this.IsPaneOpen;
    }

    /// <summary>
    /// Name of current folder documents are listed from.
    /// </summary>
    public string CurrentFolderName
    {
      get { return this.currentFolderName; }
      set
      {
        this.currentFolderName = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.CurrentFolderName)));
      }
    }

    /// <summary>
    /// List of documents can be displayed.
    /// </summary>
    public List<StorageFile> Files
    {
      get { return this.files; }
      set
      {
        this.files = value;
        this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Files)));
      }
    }

    #endregion public members

    #region private methods

    private async Task<bool> LoadDocument(StorageFile file)
    {
      this.DocumentTitle = file.DisplayName;
      this.document = new Document();
      return await this.document.LoadAsync(file, "", 0);
    }

    private async Task Render(Page page)
    {
      var fitSize = this.displayCalculationService.Fit(page.GetSize());
      var bitmap = new PixelSource
      {
        Width = (int) fitSize.Width,
        Height = (int) fitSize.Height
      };

      // what to display
      var matrix = page.GetDisplayMatrix(0, 0,
        (int) fitSize.Width, (int)fitSize.Height,
        0);
      IRandomAccessStream randomStream = await page.RenderPageAsync(bitmap,
        matrix, (uint) RenderFlags.Annot, null);
      if (null != randomStream)
      {
        var bmpImage =
          new WriteableBitmap((int) fitSize.Width, (int) fitSize.Height);
        bmpImage.SetSource(randomStream);
        bmpImage.Invalidate();
        this.CurrentPageBitmap = bmpImage;
        this.PageWidth = (int) fitSize.Width;
        this.PageHeight = (int) fitSize.Height;
      }
      this.OnPageRendered();
    }

    private void OnPageRendered()
    {
      this.NotifyOfPropertyChange(() => this.CanNextPage);
      this.NotifyOfPropertyChange(() => this.CanPreviousPage);
    }

    private async Task<StorageFile> PickFile()
    {
      var picker = new FileOpenPicker();
      picker.ViewMode = PickerViewMode.Thumbnail;
      picker.SuggestedStartLocation =
        PickerLocationId.DocumentsLibrary;
      picker.FileTypeFilter.Add(".pdf");
      return await picker.PickSingleFileAsync();
    }

    private async Task<Page> LoadCurrentPage()
    {
      var currentPage = await this.document.LoadPageAsync(this.CurrentPageNumber-1, 0, null);
      if (currentPage.pointer == 0)
      {
        await this.errorHandlingService.HandleError("File could not be loaded.");
        return await Task.FromResult<Page>(null);
      }
      return currentPage;
    }

    protected override void OnInitialize()
    {
      var size = this.displayCalculationService.CurrentViewPortSize;
      this.PageHeight = (int) size.Height;
      this.PageWidth = (int) size.Width;
      this.CurrentFolderName = "Select folder";

      Window.Current.SizeChanged += Current_SizeChanged;
    }

    private async void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
    {
      if(this.IsDocumentLoaded)
        await this.RenderCurrentPage();
    }

    protected async Task RenderCurrentPage()
    {
      await Render(await this.LoadCurrentPage());
    }

    protected async Task SetFolder(StorageFolder folder)
    {
      if (folder == null)
        return;

      this.CurrentFolderName = folder.DisplayName;
      var allFiles = await folder.GetFilesAsync();
      this.Files = allFiles
        .Where(f => f.FileType.ToLower() == ".pdf")
        .ToList();
    }

    #endregion private methods
  }
}