using System.Reflection;
using System.Windows.Input;

namespace Phanteon.Resources.Behaviors
{
    /// <summary>
    /// Behavior que convierte eventos en comandos
    /// Útil para eventos que no tienen comando nativo
    /// </summary>
    public class EventToCommandBehavior : Behavior<View>
    {
        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private EventInfo? _eventInfo;
        private Delegate? _eventHandler;

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            if (string.IsNullOrWhiteSpace(EventName))
                return;

            _eventInfo = bindable.GetType().GetEvent(EventName);
            if (_eventInfo == null)
                return;

            _eventHandler = CreateEventHandler(_eventInfo);
            _eventInfo.AddEventHandler(bindable, _eventHandler);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            if (_eventInfo != null && _eventHandler != null)
            {
                _eventInfo.RemoveEventHandler(bindable, _eventHandler);
            }

            base.OnDetachingFrom(bindable);
        }

        private Delegate CreateEventHandler(EventInfo eventInfo)
        {
            var eventHandlerType = eventInfo.EventHandlerType;
            var methodInfo = typeof(EventToCommandBehavior).GetMethod(nameof(OnEvent), BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo == null)
                throw new InvalidOperationException("OnEvent method not found");

            return Delegate.CreateDelegate(eventHandlerType!, this, methodInfo);
        }

        private void OnEvent(object? sender, EventArgs e)
        {
            if (Command?.CanExecute(CommandParameter) == true)
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
