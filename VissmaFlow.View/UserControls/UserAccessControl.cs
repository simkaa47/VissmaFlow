using Avalonia;
using Avalonia.Controls;
using VissmaFlow.Core.Models.AccessControl;

namespace VissmaFlow.View.UserControls
{
    public class UserAccessControl : UserControl
    {
        public UserAccessControl()
        {
            AffectsMeasure<UserAccessControl>(UserLevelProperty);
        }


        #region Уровень доступа
        public UserAccessLevel UserLevel
        {
            get { return (UserAccessLevel)GetValue(UserLevelProperty); }
            set { SetValue(UserLevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly StyledProperty<UserAccessLevel> UserLevelProperty =
            AvaloniaProperty.Register<UserAccessControl, UserAccessLevel>(nameof(UserLevel), UserAccessLevel.None);
        #endregion
    }
}
