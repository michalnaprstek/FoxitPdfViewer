using Windows.UI.Xaml;

namespace FoxitPdfViewer.Behaviors
{
  /// <summary>
  /// Base class for trigger behaviours with action collection to execute associated to particular type.
  /// </summary>
  /// <typeparam name="T">The object type to attach to</typeparam>
  public abstract class Trigger<T> : Trigger where T : DependencyObject
  {
    /// <summary>
    /// Gets the object to which this behavior is attached.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public new T AssociatedObject => base.AssociatedObject as T;
  }
}
