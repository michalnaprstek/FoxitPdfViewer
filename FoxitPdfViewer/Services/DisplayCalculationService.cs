using Windows.Foundation;
using FoxitPdfViewer.Intefaces.Services;

namespace FoxitPdfViewer.Services
{
  /// <summary>
  /// Service for display calculations.
  /// </summary>
  public class DisplayCalculationService : IDisplayCalculationService
  {
    /// <summary>
    /// Calculates new size for object of original <paramref name="originalSize"/> to fit into <see cref="CurrentViewPortSize"/>.
    /// </summary>
    /// <param name="originalSize"></param>
    public Size Fit(Size originalSize)
    {
      var sizeToFit = new Size(this.CurrentViewPortSize.Width, this.CurrentViewPortSize.Height);
      if (originalSize.Equals(sizeToFit)) return sizeToFit;

      var hCoef = sizeToFit.Height/originalSize.Height;

      var newWidth = originalSize.Width*hCoef;

      if (newWidth <= sizeToFit.Width)
      {
        return new Size(newWidth, sizeToFit.Height);
      }
      var wCoef = sizeToFit.Width/originalSize.Width;
      return new Size(sizeToFit.Width, originalSize.Height*wCoef);
    }

    /// <summary>
    /// Current size of main view port.
    /// </summary>
    public Size CurrentViewPortSize { get; set; }
  }
}
