using Microsoft.Xaml.Interactivity;
using Windows.UI.Input;
using Windows.UI.Xaml;

namespace FoxitPdfViewer.Behaviors
{
  /// <summary>
  /// Triggers action method when swipe gesture is performed.
  /// </summary>
  public abstract class SwipeTriggerBehavior : Trigger<UIElement>
  {

    #region Overrides of Behavior<UIElement>

    /// <summary>
    /// Called after the behavior is attached to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject"/>.
    /// </summary>
    /// <remarks>
    /// Override this to hook up functionality to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject"/>
    /// </remarks>
    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.ManipulationCompleted += AssociatedObject_ManipulationCompleted;
    }

    /// <summary>
    /// Called when the behavior is being detached from its <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject"/>.
    /// </summary>
    /// <remarks>
    /// Override this to unhook functionality from the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject"/>
    /// </remarks>
    protected override void OnDetaching()
    {
      base.OnDetaching();
      this.AssociatedObject.ManipulationCompleted -= AssociatedObject_ManipulationCompleted;
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Event handler for gesture.
    /// </summary>
    private void AssociatedObject_ManipulationCompleted(object sender, Windows.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
    {
      if (!this.CanExecute || this.Actions == null)
        return;

      if (this.IsTheRightDirection(e.Cumulative))
      {
        this.ExecuteActions(sender);
      }
    }

    /// <summary>
    /// Methods decides if direction of swipe has the right way to fire the action.
    /// </summary>
    /// <param name="manipulationDelta">Details of gesture direction.</param>
    protected abstract bool IsTheRightDirection(ManipulationDelta manipulationDelta);


    #endregion Private methods
  }
}
