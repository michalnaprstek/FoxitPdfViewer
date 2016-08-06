using System;
using System.Threading.Tasks;
using FoxitPdfViewer.Intefaces.Services;
using FoxitPdfViewer.Models;
using FoxitPdfViewer.Utils;

namespace FoxitPdfViewer.Services
{
  public class FoxitLicenseService : IFoxitLicenseService
  {
    private FoxitLicenseSetting licenseSetting;

    #region Implementation of IFoxitLicenseService

    /// <summary>
    /// Provides license setting values.
    /// </summary>
    /// <returns></returns>
    public async Task<FoxitLicenseSetting> GetLicenseSettingAsync()
    {
      return this.licenseSetting ?? (this.licenseSetting = await this.LoadLicenseAsync());
    }

    private async Task<FoxitLicenseSetting> LoadLicenseAsync()
    {
      try
      {
        var jsonParser = new JsonFileParser("ms-appx:///Assets/foxitLicense.json");
        return await jsonParser.ParseAsync<FoxitLicenseSetting>();
      }
      catch(Exception ex)
      {
        return null;
      }
    }

    #endregion
  }
}
