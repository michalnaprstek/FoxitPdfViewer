using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Foxit;
using FoxitPdfViewer.Intefaces.Services;
using FoxitPdfViewer.Services;
using FoxitPdfViewer.ViewModels;
using FoxitPdfViewer.Views;

namespace FoxitPdfViewer
{
  /// <summary>
  /// Provides application-specific behavior to supplement the default Application class.
  /// </summary>
  sealed partial class App
  {
    private WinRTContainer container;

    public App()
    {
      InitializeComponent();
    }

    protected override async void Configure()
    {
      container = new WinRTContainer();
      container.RegisterWinRTServices();

      // Register service instances
      this.container.RegisterInstance(typeof(IErrorHandlingService), "IErrorHandlingService", new ErrorHandlingService());
      this.container.RegisterInstance(typeof(IDisplayCalculationService), "IDisplayCalculationService", new DisplayCalculationService());
      this.container.RegisterInstance(typeof(IFoxitLicenseService), "IFoxitLicenseService", new FoxitLicenseService());

      // Register your view models at the container
      this.container.RegisterPerRequest(typeof(MainViewModel), "MainViewModel", typeof(MainViewModel));


      if (!await this.InitFoxitLibraryAsync())
      {
        this.Exit();
      }
    }

    protected override object GetInstance(Type service, string key)
    {
      var instance = container.GetInstance(service, key);
      if (instance != null)
        return instance;
      throw new Exception("Could not locate any instances.");
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
      container.BuildUp(instance);
    }

    protected override void PrepareViewFirst(Frame rootFrame)
    {
      container.RegisterNavigationService(rootFrame);
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
      if (args.PreviousExecutionState == ApplicationExecutionState.Running)
        return;
      
      this.DisplayRootView<MainView>();
    }

    /// <summary>
    /// Initialzes Foxit library (https://www.foxitsoftware.com/products/sdk/net-sdk/).
    /// </summary>
    /// <returns>True, if library is successfully initialized. Otherwise false.</returns>
    private async Task<bool> InitFoxitLibraryAsync()
    {
      try
      {
        var license = await this.container.GetInstance<IFoxitLicenseService>().GetLicenseSettingAsync();
        if (license == null)
          return false;

        Library.Load(license.LicenseKey, license.UnlockCode);

        var resultCode = Library.GetLastError();
        if (resultCode != ErrorCode.Success)
        {

#if (DEBUG)
          await
            this.container.GetInstance<IErrorHandlingService>()
              .HandleError($"Foxit library load failed with code: {resultCode}. Please check foxitLicense.json asset.");
#else
          await
            this.container.GetInstance<IErrorHandlingService>()
              .HandleError($"Foxit library load failed with code: {resultCode}");
#endif
          return false;
        }

        Library.LoadSystemFonts();
        resultCode = Library.GetLastError();
        if (resultCode != ErrorCode.Success)
        {
          await
            this.container.GetInstance<IErrorHandlingService>()
              .HandleError($"Foxit library load system fonts failed with code: {resultCode}");
          return false;
        }
      }
      catch (Exception ex)
      {
        await
            this.container.GetInstance<IErrorHandlingService>()
              .HandleError(ex, "Foxit library initialization failed.");
        return false;
      }

      return true;
    }

  }
}
