using System.ComponentModel.DataAnnotations;

namespace TodoApi.Data.Dto;

public class UpdateTodoDto
{
    [Required(ErrorMessage = "O titulo é obrigatório.")]
    [StringLength(100, ErrorMessage = "Tamanho máximo é de 100 caracteres.")]
    public string Title { get; set; }
}
