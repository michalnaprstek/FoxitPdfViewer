using System.ComponentModel;
using Caliburn.Micro;
using FoxitPdfViewer.Intefaces.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FoxitPdfViewer.Views
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainView : Page
  {
    public MainView()
    {
      this.InitializeComponent();
      this.DataContextChanged += MainView_DataContextChanged;
    }

    private void MainView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
      var vm = this.DataContext as INotifyPropertyChanged;
      if (vm == null)
        return;

      vm.PropertyChanged += (o, eventArgs) =>
      {
        if (eventArgs.PropertyName == "CurrentPageBitmap" && this.Scrollster.ZoomFactor > 1)
          this.Scrollster.ChangeView(0, 0, 1);
      };
    }

    private void Scrollster_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      var displayCalculationService =
        (IDisplayCalculationService) IoC.GetInstance(typeof (IDisplayCalculationService), "IDisplayCalculationService");

      displayCalculationService.CurrentViewPortSize = e.NewSize;
    }
  }
}
