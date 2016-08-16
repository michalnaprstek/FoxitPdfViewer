using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace FoxitPdfViewer.Behaviors
{
  /// <summary>
  /// Base class for trigger behaviours with action collection to execute.
  /// </summary>
  public class Trigger : Behavior
  {
    #region Dependency properties definition

    public static readonly DependencyProperty CanExecuteProperty = DependencyProperty.Register(
        "CanExecute",
        typeof(bool),
        typeof(SwipeTriggerBehavior),
        new PropertyMetadata(true)
      );

    public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
      "Actions",
      typeof(ActionCollection),
      typeof(SwipeTriggerBehavior),
      new PropertyMetadata(null));

    #endregion Dependency properties definition

    #region Public properties

    /// <summary>
    /// Enables/Disables execution of <see cref="Actions"/>.
    /// </summary>
    public bool CanExecute
    {
      get { return (bool)GetValue(CanExecuteProperty); }
      set { SetValue(CanExecuteProperty, value); }
    }

    /// <summary>
    /// Gets the collection of actions associated with the behavior. This is a dependency property.
    /// </summary>
    public ActionCollection Actions
    {
      get
      {
        ActionCollection actionCollection = (ActionCollection)this.GetValue(ActionsProperty);
        if (actionCollection == null)
        {
          actionCollection = new ActionCollection();
          this.SetValue(ActionsProperty, actionCollection);
        }

        return actionCollection;
      }
    }

    #endregion Public properties

    #region Private/protected methods

    /// <summary>
    /// Execute <see cref="Actions"/>.
    /// </summary>
    /// <param name="sender"></param>
    protected void ExecuteActions(object sender)
    {
      Interaction.ExecuteActions(sender, this.Actions, null);
    }

    #endregion Private/protected methods
  }
}
