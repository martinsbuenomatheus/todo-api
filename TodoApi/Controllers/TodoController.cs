using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Data.Dto;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private TodoDbContext _todoContext;
    private IMapper _mapper;

    public TodoController(IMapper mapper, TodoDbContext todoContext)
    {
        _mapper = mapper;
        _todoContext = todoContext;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateTodo([FromBody] CreateTodoDto todoDto)
    {
        Todo todo = _mapper.Map<Todo>(todoDto);

        _todoContext.Add(todo);
        _todoContext.SaveChanges();

        return CreatedAtAction(nameof(GetTodoById), new { todo.Id }, todo);
    }

    /// <summary>
    /// Busca Todos, por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult GetTodoById(int id)
    {
        var todo = _todoContext.Todos.FirstOrDefault(filme => filme.Id == id);
        if (todo == null)
        {
            return NotFound();
        }

        var todoDto = _mapper.Map<ReadTodoDto>(todo);

        return Ok(todoDto);
    }
}
