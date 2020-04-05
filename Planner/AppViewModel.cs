﻿using Planner.Models;
using Planner.Utilty;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Planner
{
    public class AppViewModel : ObservableObject
    {
        private string _inputTaskText;
        private string _inputFolderText;
        private bool _isFolderInputVisible;
        private bool _isFolderInputFocused;
        
        public RelayCommand ClosingWindowCommand { get; private set;}
        public RelayCommand MinimizeWindowCommand { get; private set; }
        public RelayCommand AddFolderCommand { get; private set; }
        public RelayCommand SelectFolderCommand { get; private set; }
        public RelayCommand AddTaskCommand { get; private set; }
        public RelayCommand MakeTaskDoneCommand { get; private set; }
        public RelayCommand DeleteTaskCommand { get; private set; }
        
        public ObservableCollection<Folder> Folders { get; set; }
        public string InputTaskText 
        {
            get
            {
                return _inputTaskText;
            }
            set
            {
                if (value == _inputTaskText)
                    return;
                _inputTaskText = value;
                OnPropertyChanged(nameof(InputTaskText));
            }
        }
        public bool IsFolderInputFocused
        {
            get
            {
                return _isFolderInputFocused;
            }
            set
            {
                if (value == _isFolderInputFocused)
                    return;
                _isFolderInputFocused = value;
                OnPropertyChanged(nameof(IsFolderInputFocused));
            }
        }
        public string InputFolderText
        {
            get
            {
                return _inputFolderText;
            }
            set
            {
                if (value == _inputFolderText)
                    return;
                _inputFolderText = value;
                OnPropertyChanged(nameof(InputFolderText));
            }
        }
        public bool IsFolderInputVisible
        {
            get
            {
                return _isFolderInputVisible;
            }
            set
            {
                if (value == _isFolderInputVisible)
                    return;
                _isFolderInputVisible = value;
                OnPropertyChanged(nameof(IsFolderInputVisible));
            }
        }
        private void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void MakeTaskDone(object parameter)
        {
            if (parameter != null)
               (parameter as Task).Done = !(parameter as Task).Done;
        }
        private void AddFolder(object parameter)
        {
            if (CanAddText(parameter.ToString()) && IsFolderInputVisible)
            {
                Folders.Add(new Folder(parameter as string));
                IsFolderInputVisible = !IsFolderInputVisible;
                return;
            }
            else
            {
                InputFolderText = "";
                IsFolderInputVisible = !IsFolderInputVisible;
            }
        }
        private bool CanAddText(string InputText)
        {
            return !(String.IsNullOrWhiteSpace(InputText));
        }

        private void DeleteTask(object parameter)
        {
            if (parameter != null)
            {
                for (int i = 0; i < Folders.Count; i++)
                    if (Folders[i].Selected)
                        Folders[i].Tasks.Remove(parameter as Task);
            }

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
        }
        private void AddTask()
        {
            if (CanAddText(InputTaskText))
                for(int i = 0; i < Folders.Count; i++)
                    if (Folders[i].Selected)
                        Folders[i].Tasks.Add(new Task(InputTaskText));
            InputTaskText = "";
        }
        public AppViewModel()
        {
            Folders = new ObservableCollection<Folder>();
            ClosingWindowCommand = new RelayCommand(p=> Application.Current.Shutdown(), p=>true);
            MinimizeWindowCommand = new RelayCommand(p => MinimizeWindow(), p => true);
            AddFolderCommand = new RelayCommand(p => AddFolder(p), p=>true);
            SelectFolderCommand = new RelayCommand(p=>SelectFolder(p), p=>true);
            AddTaskCommand = new RelayCommand(p => AddTask(), p => true);
            MakeTaskDoneCommand = new RelayCommand(p => MakeTaskDone(p), p => true);
            DeleteTaskCommand = new RelayCommand(p => DeleteTask(p), p => true);
            
            IsFolderInputFocused = true;
        }
    }
}
