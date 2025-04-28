using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ToDoList.ViewModels;

namespace ToDoList.Views;

public partial class AddTaskCategories : Window
{
    public AddTaskCategories(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}