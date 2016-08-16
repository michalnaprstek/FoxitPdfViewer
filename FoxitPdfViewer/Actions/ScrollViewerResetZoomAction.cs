using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace FoxitPdfViewer.Actions
{
  /// <summary>
  /// Resets zoom of <see cref="ScrollViewer"/>.
  /// </summary>
  public class ScrollViewerResetZoomAction : DependencyObject, IAction
  {

    #region Dependency properties definition

    public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register(
      "ScrollViewer", typeof(ScrollViewer), typeof(ScrollViewerResetZoomAction), new PropertyMetadata(default(ScrollViewer)));

    #endregion Dependency properties definition
    
    #region Public properties

    /// <summary>
    /// Scrollviewer to reset zoom. It is a dependecy property.
    /// </summary>
    public ScrollViewer ScrollViewer
    {
      get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
      set { SetValue(ScrollViewerProperty, value); }
    }

    #endregion Public properties

    #region Implementation of IAction

    /// <summary>
    /// Executes the action.
    /// </summary>
    /// <param name="sender">The <see cref="T:System.Object"/> that is passed to the action by the behavior. Generally this is <seealso cref="P:Microsoft.Xaml.Interactivity.IBehavior.AssociatedObject"/> or a target object.</param><param name="parameter">The value of this parameter is determined by the caller.</param>
    /// <remarks>
    /// An example of parameter usage is EventTriggerBehavior, which passes the EventArgs as a parameter to its actions.
    /// </remarks>
    /// <returns>
    /// Returns the result of the action.
    /// </returns>
    public object Execute(object sender, object parameter)
    {
      if (this.ScrollViewer == null)
        return false;
      this.ScrollViewer.ChangeView(0, 0, 1);
      return true;
    }

    #endregion
  }
}
