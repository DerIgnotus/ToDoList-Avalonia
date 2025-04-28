namespace ToDoList.Models;

public class ToDo
{
    public string? Content { get; set; } 
    public bool IsCompleted { get; set; } = false;
    
    public string Priority { get; set; } = "Medium Priority";
    
    public string Category { get; set; } = "General";
}
