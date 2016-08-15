using Windows.UI.Input;

namespace FoxitPdfViewer.Behaviors
{
  /// <summary>
  /// Handles right swipe gesture.
  /// </summary>
  public class RightSwipeMethodActionBehavior : SwipeMethodActionBehavior
  {
    #region Overrides of SwipeActionBehavior

    /// <summary>
    /// Methods decides if direction of swipe has the right way to fire the action.
    /// </summary>
    /// <param name="manipulationDelta">Details of gesture direction.</param>
    protected override bool IsTheRightDirection(ManipulationDelta manipulationDelta)
    {
      return manipulationDelta.Translation.X > 0;
    }

    #endregion
  }
}
