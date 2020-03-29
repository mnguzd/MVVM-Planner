using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Planner.Utilty
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool OnPropertyChanged<T>(ref T BackingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(BackingField, value))
                return false;
            BackingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
