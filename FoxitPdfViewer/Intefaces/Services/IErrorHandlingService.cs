using System;
using System.Threading.Tasks;

namespace FoxitPdfViewer.Intefaces.Services
{
  /// <summary>
  /// Interface of error handling service.
  /// </summary>
  public interface IErrorHandlingService
  {
    /// <summary>
    /// Handles error whith exception.
    /// </summary>
    /// <param name="ex">Exception to handle.</param>
    /// <param name="message">Message with description of error.</param>
    Task HandleError(Exception ex, string message);

    /// <summary>
    /// Handles error whith exception.
    /// </summary>
    /// <param name="ex">Exception to handle.</param>
    /// <param name="message">Message with description of error.</param>
    Task HandleError(string message);
  }
}
