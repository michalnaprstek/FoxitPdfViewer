using Windows.UI.Input;
using Windows.UI.Xaml.Markup;

namespace FoxitPdfViewer.Behaviors
{
  /// <summary>
  /// Handles left swipe gesture.
  /// </summary>
  public class LeftSwipeMethodActionBehavior : SwipeMethodActionBehavior
  {
    #region Overrides of SwipeActionBehavior

    /// <summary>
    /// Methods decides if direction of swipe has the right way to fire the action.
    /// </summary>
    /// <param name="manipulationDelta">Details of gesture direction.</param>
    protected override bool IsTheRightDirection(ManipulationDelta manipulationDelta)
    {
      return manipulationDelta.Translation.X < 0;
    }

    #endregion
  }
}
