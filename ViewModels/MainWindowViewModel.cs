using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;

namespace ToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<ToDo> _todoList = [];
    
    [ObservableProperty] private string? _newToDoContent;
    public MainWindowViewModel()
    {

    }
    
    [RelayCommand]
    private void AddToDo()
    {
        if (string.IsNullOrWhiteSpace(NewToDoContent))
            return;

        var newToDo = new ToDo
        {
            Content = NewToDoContent,
            IsCompleted = false
        };

        TodoList.Add(newToDo);
        NewToDoContent = string.Empty;
    }
    
    [RelayCommand]
    private void DeleteTodo(ToDo todo)
    {
        TodoList.Remove(todo);
    }
}