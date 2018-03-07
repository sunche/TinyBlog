using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tinyblog.Client.Annotations;

namespace Tinyblog.Client.ViewModels
{
    /// <summary>
    /// Base viewmodel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged"/>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
