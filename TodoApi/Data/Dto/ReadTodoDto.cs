using System.ComponentModel.DataAnnotations;

namespace TodoApi.Data.Dto;

public class ReadTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsComplete { get; set; } = false;
}
