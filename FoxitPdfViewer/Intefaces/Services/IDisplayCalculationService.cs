using Windows.Foundation;

namespace FoxitPdfViewer.Intefaces.Services
{
  /// <summary>
  /// Interface of service for display calculations.
  /// </summary>
  public interface IDisplayCalculationService
  {
    /// <summary>
    /// Calculates new size for object of original <paramref name="originalSize"/> to fit into <see cref="CurrentViewPortSize"/>.
    /// </summary>
    /// <param name="originalSize"></param>
    Size Fit(Size originalSize);

    /// <summary>
    /// Current size of main view port.
    /// </summary>
    Size CurrentViewPortSize { get; set; }
  }
}
