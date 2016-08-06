using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using FoxitPdfViewer.Intefaces.Services;

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
    }

    private void Scrollster_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      var displayCalculationService =
        (IDisplayCalculationService) IoC.GetInstance(typeof (IDisplayCalculationService), "IDisplayCalculationService");

      displayCalculationService.CurrentViewPortSize = e.NewSize;
    }
  }
}
