using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.Extensions.Behaviors
{
    public class AppendTextBehavior : Behavior<TextBox>
    {
        public Action<string> AppendTextAction
        {
            get { return (Action<string>)GetValue(AppendTextActionProperty); }
            set { SetValue(AppendTextActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppendTextAction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppendTextActionProperty =
            DependencyProperty.Register("AppendTextAction", typeof(Action<string>), typeof(AppendTextBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            SetCurrentValue(AppendTextActionProperty, (Action<string>)AssociatedObject.AppendText);
            base.OnAttached();
        }
    }

}
