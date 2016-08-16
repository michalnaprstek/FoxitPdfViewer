using Windows.System;
using Windows.UI.Xaml;

namespace FoxitPdfViewer.Behaviors
{
  public class KeyUpTriggerBehavior : Trigger<UIElement>
  {
    #region Dependency properties definition

    public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(
      "Key", typeof (VirtualKey), typeof (KeyUpTriggerBehavior), new PropertyMetadata(default(VirtualKey)));

    #endregion Dependency properties definition

    #region Public properties
    
    /// <summary>
    /// Key to handle.
    /// </summary>
    public VirtualKey Key
    {
      get { return (VirtualKey)GetValue(KeyProperty); }
      set { SetValue(KeyProperty, value); }
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
      this.AssociatedObject.KeyUp += AssociatedObject_KeyUp;
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
      this.AssociatedObject.KeyUp -= AssociatedObject_KeyUp;
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Handles keup event.
    /// </summary>
    private void AssociatedObject_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
      if (!this.CanExecute)
        return;

      if(e.Key == this.Key)
        this.ExecuteActions(sender);
    }

    #endregion Private methods
  }
}
