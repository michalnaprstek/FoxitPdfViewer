using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FoxitPdfViewer.Intefaces.Services;

namespace FoxitPdfViewer.Services
{
  /// <summary>
  /// Error handling service.
  /// </summary>
  class ErrorHandlingService : IErrorHandlingService
  {
    /// <summary>
    /// Handles error whith exception.
    /// </summary>
    /// <param name="ex">Exception to handle.</param>
    /// <param name="message">Message with description of error.</param>
    public async Task HandleError(Exception ex, string message)
    {
      var dialog = new MessageDialog(message);
      await dialog.ShowAsync();
    }

    /// <summary>
    /// Handles error whith exception.
    /// </summary>
    /// <param name="ex">Exception to handle.</param>
    /// <param name="message">Message with description of error.</param>
    public async Task HandleError(string message)
    {
      var dialog = new MessageDialog(message);
      await dialog.ShowAsync();
    }
  }
}
