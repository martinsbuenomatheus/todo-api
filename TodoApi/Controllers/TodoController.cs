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
    /// Cria um item ToDo
    /// </summary>
    /// <param name="todoDto"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateTodo([FromBody] CreateTodoDto todoDto)
    {
        try
        {

            Todo todo = _mapper.Map<Todo>(todoDto);

            _todoContext.Add(todo);
            _todoContext.SaveChanges();

            return CreatedAtAction(nameof(GetTodoById), new { todo.Id }, todo);
        }
        catch (Exception ex) 
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Busca Todos, por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    [HttpGet("{id}")]
    public IActionResult GetTodoById(int id)
    {
        try
        {
            var todo = _todoContext.Todos.FirstOrDefault(filme => filme.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            var todoDto = _mapper.Map<ReadTodoDto>(todo);

            return Ok(todoDto);
        }
        catch (Exception ex) 
        {
            return StatusCode(500, ex.Message);
        }   
    }

    /// <summary>
    /// Busca Todos, paginado
    /// </summary>
    /// <param name="page">Pagina</param>
    /// <param name="take">Quantidade de itens</param>
    /// <returns>IActionResult</returns>
    [HttpGet]
    public IEnumerable<ReadTodoDto> GetTodo([FromQuery] int page = 1, [FromQuery] int take = 10)
    {
        try
        {
            // Offset de paginação
            var skip = (page * take) - take;

            return _mapper.Map<List<ReadTodoDto>>(_todoContext.Todos.Skip(skip).Take(take));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    /// <summary>
    /// Atualiza um Todo
    /// </summary>
    /// <param name="id"></param>
    /// <param name="todoDto"></param>
    /// <returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id, [FromBody] UpdateTodoDto todoDto)
    {
        try
        {
            var todo = _todoContext.Todos.FirstOrDefault(todo => todo.Id == id);

            if(todo == null)
            {
                return NotFound(); 
            }
            
            _mapper.Map(todoDto, todo); 
            _todoContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        } 
    }

    /// <summary>
    /// Check um Todo - altera a flag
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    public IActionResult CheckTodo(int id) 
    {
        try
        {
            var todo = _todoContext.Todos.FirstOrDefault(todo => todo.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = !todo.IsComplete;
            _todoContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex) 
        {
            return StatusCode(500, ex.Message);
        }
    }
}
