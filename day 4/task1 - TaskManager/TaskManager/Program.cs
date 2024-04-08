using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum TaskCategory
{
    Personal,
    Work,
    Errands,
    School
}

public class TaskItem
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
}

public class TaskManagement
{
    private List<TaskItem> tasks = new List<TaskItem>();

    public void AddTask(TaskItem task)
    {
        try
        {
            tasks.Add(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Sorry, there is an error trying to add {task}: {ex.Message}");
        }
    }

    public void ViewTasks()
    {
        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
        }
    }

    public void ViewTasksByCategory(TaskCategory category)
    {
        var categorizedTasks = tasks.Where(task => task.Category == category);
        foreach (var task in categorizedTasks)
        {
            Console.WriteLine($"The Name of the Task: {task.Name}");
            Console.WriteLine($"The Description of the Task: {task.Description}");
            Console.WriteLine($"The Category of the Task: {task.Category}");
            Console.WriteLine($"The status of the Task: {task.IsCompleted}");
        }
    }

    public async Task SaveTasks(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while trying to save tasks to {filePath}: {ex.Message}");
        }
    }

    public async Task LoadTasks(string filePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] parts = line.Split(',');
                    TaskItem task = new TaskItem
                    {
                        Name = parts[0],
                        Description = parts[1],
                        Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), parts[2]),
                        IsCompleted = bool.Parse(parts[3])
                    };
                    tasks.Add(task);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error trying to load tasks from {filePath}: {ex.Message} ");
        }
    }
}

class TaskManager
{
    static async Task Main(string[] args)
    {
        TaskManagement taskManagement = new TaskManagement();

        TaskItem task1 = new TaskItem
        {
            Name = "Task 1",
            Description = "Take a Bath",
            Category = TaskCategory.Personal,
            IsCompleted = false
        };

        TaskItem task2 = new TaskItem
        {
            Name = "Task 2",
            Description = "Study C#",
            Category = TaskCategory.Work,
            IsCompleted = true
        };

        TaskItem task3 = new TaskItem
        {
            Name = "Task 3",
            Description = "Study Calculus",
            Category = TaskCategory.School,
            IsCompleted = false
        };

        taskManagement.AddTask(task1);
        taskManagement.AddTask(task2);
        taskManagement.AddTask(task3);

        Console.WriteLine("All your tasks are shown below:");
        taskManagement.ViewTasks();

        Console.WriteLine("\nYour personalized tasks:");
        taskManagement.ViewTasksByCategory(TaskCategory.Personal);


        Console.WriteLine("\nYour School tasks:");
        taskManagement.ViewTasksByCategory(TaskCategory.School);

        Console.WriteLine("\nYour Work tasks:");
        taskManagement.ViewTasksByCategory(TaskCategory.Work);

        string filePath = "tasksFilePath.csv";
        await taskManagement.SaveTasks(filePath);
        Console.WriteLine($"\nTasks saved to {filePath}");

        await taskManagement.LoadTasks(filePath);
        Console.WriteLine("\nTasks loaded from file:");
        taskManagement.ViewTasks();
    }
}
