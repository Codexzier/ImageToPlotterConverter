using Components.ImageToGraph;
using ImageToPlotterConverter.Views.Base;

namespace ImageToPlotterConverter.Views.Main
{
    public class MainViewModel : BaseViewModel
    {
        private ImageToGraphState _state = new ImageToGraphState();

        public ImageToGraphState State 
        { 
            get => this._state;
            set
            {
                this._state = value;
                this.OnNotifyPropertyChanged(nameof(this.State));
            }
        }
    }
}
