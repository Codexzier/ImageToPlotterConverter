using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Components.EventBus
{
    public class SideHostControl : Control
    {
        private ContentPresenter _presenter;

        static SideHostControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SideHostControl), new FrameworkPropertyMetadata("nothing"));
        }

        private readonly EventbusManager _eventbus;

        public SideHostControl()
        {
            this._eventbus = EventbusManager.GetEventbus();
            this._eventbus.OpenViewEvent += this._eventbus_OpenViewEvent;
        }

        public override void OnApplyTemplate()
        {
            this._presenter = this.SetContentPresenter(this);
            //this._presenter.Content = new Side1View();
        }

        private ContentPresenter SetContentPresenter(DependencyObject dependencyObject)
        {
            var num = VisualTreeHelper.GetChildrenCount(this);
            for (int index = 0; index < num; index++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(dependencyObject, index);
                if (v is ContentPresenter presenter)
                {
                    return presenter;
                }

                return this.SetContentPresenter(v);
            }

            return null;
        }

        private void _eventbus_OpenViewEvent(object obj)
        {
            if (this._presenter.Content != null &&
               this._presenter.Content is IDisposable disposable)
            {
                disposable.Dispose();
            }

            this._presenter.Content = (Control)obj;
        }
    }
}
