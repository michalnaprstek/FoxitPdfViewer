using System.Threading.Tasks;
using FoxitPdfViewer.Models;

namespace FoxitPdfViewer.Intefaces.Services
{
  /// <summary>
  /// Interface of service providing license information for Foxit SDK.
  /// </summary>
  internal interface IFoxitLicenseService
  {
    /// <summary>
    /// Provides license setting values.
    /// </summary>
    /// <returns></returns>
    Task<FoxitLicenseSetting> GetLicenseSettingAsync();
  }
}
