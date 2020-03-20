using Components.ImageToGraph;
using Components.Ui.EventBus;
using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ImageToPlotterConverter.Views.Main
{
    public partial class MainView : UserControl, IDisposable
    {
        private readonly MainViewModel _viewModel;

        public MainView()
        {
            this.InitializeComponent();

            this._viewModel = (MainViewModel)this.DataContext;

            EventbusManager.GetEventbus().Register<MainView, MainMessage>(this.EventBusReceivedMessage);

            this.FinishConvertEvent += this.MainView_FinishConvertEvent;
        }

        private bool EventBusReceivedMessage(IMessageContainer arg)
        {
            if (arg.Content is string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }

                this.ConvertImage(path);

                return true;
            }

            return false;
        }

        private void ConvertImage(string path)
        {
            var converter = new ImageToGraphConverter();
            var bitmap = new Bitmap(path);

            var res = converter.Convert(bitmap, 5, 5, this._viewModel.State);

            var ppb = new PathPlotterBuilder();

            var result = ppb.CreatePathLine(res, System.Windows.Media.Brushes.Black, 1280, 960, 2.5);

            this.FinishConvertEvent?.Invoke(result);
        }

        private void MainView_FinishConvertEvent(Path pathElement)
        {
            this.GridPlotterresult.Children.Clear();
            this.GridPlotterresult.Children.Add(pathElement);
        }

        private delegate void FinishConvertEventHandler(Path pathElement);
        private event FinishConvertEventHandler FinishConvertEvent;

        public void Dispose()
        {
            EventbusManager.GetEventbus().Deregister<MainView>();
            this.FinishConvertEvent -= this.MainView_FinishConvertEvent;

        }
    }
}
