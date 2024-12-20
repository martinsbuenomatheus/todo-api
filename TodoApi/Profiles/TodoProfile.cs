﻿using AutoMapper;
using TodoApi.Data.Dto;
using TodoApi.Models;

namespace TodoApi.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile() 
    {
        CreateMap<CreateTodoDto, Todo>();
        CreateMap<UpdateTodoDto, Todo>();
        CreateMap<Todo, ReadTodoDto>();
        CreateMap<Todo, UpdateTodoDto>();
    }
}
