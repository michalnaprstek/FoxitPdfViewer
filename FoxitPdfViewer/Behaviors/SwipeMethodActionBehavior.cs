using Microsoft.Xaml.Interactivity;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Caliburn.Micro;

namespace FoxitPdfViewer.Behaviors
{
  /// <summary>
  /// Triggers action method when swipe gesture is performed.
  /// </summary>
  public abstract class SwipeMethodActionBehavior : Behavior<UIElement>
  {

    #region Dependency properties definition

    public static readonly DependencyProperty CanExecuteProperty = DependencyProperty.Register(
        "CanExecute",
        typeof(bool),
        typeof(SwipeMethodActionBehavior),
        new PropertyMetadata(true)
      );

    public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(
      "MethodName",
      typeof(string),
      typeof(SwipeMethodActionBehavior),
      new PropertyMetadata(null)
      );

    #endregion Dependency properties definition

    #region Public properties

    /// <summary>
    /// Enables/Disables execution of <see cref="Actions"/> on swipe.
    /// </summary>
    public bool CanExecute
    {
      get { return (bool)GetValue(CanExecuteProperty); }
      set { SetValue(CanExecuteProperty, value); }
    }

    /// <summary>
    /// Name of the method should be called on gesture.
    /// </summary>
    public string MethodName
    {
      get { return (string) GetValue(MethodNameProperty); }
      set { SetValue(MethodNameProperty, value); }
    }

    #endregion Public properties

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
      if (!this.CanExecute || this.MethodName == null)
        return;

      if (this.IsTheRightDirection(e.Cumulative))
      {
        Interaction.ExecuteActions(sender, this.Actions, null);
      }
    }

    /// <summary>
    /// Helper for build <see cref="ActionCollection"/> with one <see cref="ActionMessage"/> using <see cref="MethodName"/>.
    /// </summary>
    private ActionCollection Actions => new ActionCollection {new ActionMessage {MethodName = this.MethodName}};

    /// <summary>
    /// Methods decides if direction of swipe has the right way to fire the action.
    /// </summary>
    /// <param name="manipulationDelta">Details of gesture direction.</param>
    protected abstract bool IsTheRightDirection(ManipulationDelta manipulationDelta);


    #endregion Private methods
  }
}
