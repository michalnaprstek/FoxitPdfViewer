﻿using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Caliburn.Micro;
using FoxitPdfViewer.Intefaces.Services;
using FoxitPdfViewer.Intefaces.ViewModels;

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
      if (vm != null)
      {
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
    }
  }
}
