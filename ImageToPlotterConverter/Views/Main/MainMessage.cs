using Components.Ui.EventBus;

namespace ImageToPlotterConverter.Views.Main
{
    public class MainMessage : IMessageContainer
    {
        public MainMessage(object content)
        {
            this.Content = content;
        }
        public object Content { get; }
    }
}
