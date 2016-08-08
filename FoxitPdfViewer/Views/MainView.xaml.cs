using Windows.System;
using Caliburn.Micro;
using FoxitPdfViewer.Intefaces.Services;
using FoxitPdfViewer.Intefaces.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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

    private void ViewPortImage_OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
    {
      var vm = this.DataContext as IMainViewModel;
      if (vm == null)
        return;

      if (this.Scrollster.ZoomFactor > 1.0)
        return;

      if (e.Cumulative.Translation.X < 0)
      {
        if (vm.CanNextPage)
          vm.NextPage();
      }
      else if (e.Cumulative.Translation.X > 0)
      {
        if (vm.CanPreviousPage)
          vm.PreviousPage();
      }
    }

    private void MainView_OnKeyUp(object sender, KeyRoutedEventArgs e)
    {
      if (e.Handled)
        return;

      var vm = this.DataContext as IMainViewModel;
      if (vm == null)
        return;

      if (e.Key == VirtualKey.Right && vm.CanNextPage)
        vm.NextPage();
      else if (e.Key == VirtualKey.Left && vm.CanPreviousPage)
        vm.PreviousPage();

    }
  }
}
