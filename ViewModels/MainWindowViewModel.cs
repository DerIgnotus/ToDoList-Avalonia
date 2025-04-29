using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.Models;
using ToDoList.Views;

namespace ToDoList.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<string> Categories { get; } =
    [
        "All",
        "General",
        "Work",
        "Chores"
    ];
    
    public ObservableCollection<string> ChoosableCategories { get; } =
    [
        "General",
        "Work",
        "Chores"
    ];
    
    public ObservableCollection<string> Priorities { get; } =
    [
        "All Priorities",
        "High Priority",
        "Medium Priority",
        "Low Priority"
    ];
    
    public ObservableCollection<string> ChoosablePriorities { get; } =
    [
        "High Priority",
        "Medium Priority",
        "Low Priority"
    ];
    
    [ObservableProperty] private string _selectedCategory = "All";
    [ObservableProperty] private string _selectedPriority = "All Priorities";
    
    [ObservableProperty] private ObservableCollection<ToDo> _todoList = [];
    private readonly ObservableCollection<ToDo> _nonFilteredToDoList = [];
    
    [ObservableProperty] private string? _newToDoContent;
    [ObservableProperty] private string _newToDoCategory = "General";
    [ObservableProperty] private string _newToDoPriority = "Medium Priority";
    
    private AddTaskCategories? toDoView;
    
    public MainWindowViewModel()
    {
        _nonFilteredToDoList.Add(new ToDo()
        {
            Content = "test",
        });
        
        FilterToDoList();
    }
    
    [RelayCommand]
    private void AddToDo()
    {
        if (string.IsNullOrWhiteSpace(NewToDoContent))
            return;

        toDoView = new AddTaskCategories(this);
        toDoView.Show();
    }

    [RelayCommand]
    private void FinalAddToDo()
    {
        toDoView.Close();
        
        var newToDo = new ToDo
        {
            Content = NewToDoContent,
            IsCompleted = false,
            Category = NewToDoCategory,
            Priority = NewToDoPriority
        };
        
        _nonFilteredToDoList.Add(newToDo);
        NewToDoContent = string.Empty;
    }
    
    [RelayCommand]
    private void DeleteTodo(ToDo todo)
    {
        _nonFilteredToDoList.Remove(todo);
    }
    
    partial void OnSelectedCategoryChanged(string value)
    {
        _ = value;
        
        FilterToDoList();
    }

    [RelayCommand]
    private void DoneEditing(TextBox textBox)
    {
        textBox.IsReadOnly = true;
        textBox.Focusable = false;

        var window = textBox.GetVisualRoot() as Window;
        window?.Focus();
    }

    [RelayCommand]
    private void EditTodo(TextBox textBox)
    {
        textBox.IsReadOnly = false;
        textBox.Focusable = true;
        textBox.Focus();
    }
    
    partial void OnSelectedPriorityChanged(string value)
    {
        _ = value;
        
        FilterToDoList();
    }
    
    private void FilterToDoList()
    {
        var firstFilterApplied = SelectedCategory == "All" ? _nonFilteredToDoList : new ObservableCollection<ToDo>(_nonFilteredToDoList.Where(todo => todo.Category == SelectedCategory));

        var secondFilterApplied = SelectedPriority == "All Priorities" ? firstFilterApplied : new ObservableCollection<ToDo>(firstFilterApplied.Where(todo => todo.Priority == SelectedPriority));

        TodoList = secondFilterApplied;
    }
}