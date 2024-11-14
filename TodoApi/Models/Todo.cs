using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Todo
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O titulo é obrigatório.")]
    [MaxLength(500, ErrorMessage = "Tamanho máximo é de 500 caracteres.")]
    public string Title { get; set; }
    public bool IsComplete { get; set; } = false;
}
