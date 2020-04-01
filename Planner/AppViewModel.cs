using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
        public int I { get; set; } = 0;
        public RelayCommand ClosingWindowCommand { get; private set;}
        public RelayCommand MinimizeWindowCommand { get; private set; }
        public RelayCommand AddFolderCommand { get; private set; }
        public RelayCommand SelectFolderCommand { get; private set; }
        public ObservableCollection<Folder> Folders { get; set; }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void AddFolder()
        {
            I++;
            Folders.Add(new Folder(DateTime.Now.ToShortTimeString(),I.ToString()));
        }

        private void SelectFolder(object parameter)
        {
            if (parameter != null)
            {
                foreach (Folder i in Folders)
                {
                    i.Selected = false;
                }
            (parameter as Folder).Selected = true;
            }
            else MessageBox.Show("ff");
        }
        private bool CanSelectFolder()
        {
            return Folders.Count > 0 ? true : false;
        }
        public AppViewModel()
        {
            ClosingWindowCommand = new RelayCommand(p=> Application.Current.Shutdown(), p=>true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            AddFolderCommand = new RelayCommand(p => AddFolder(), p => true);
            SelectFolderCommand = new RelayCommand(p=>SelectFolder(p), p=>CanSelectFolder());
            Folders = new ObservableCollection<Folder>();
        }
    }
}
