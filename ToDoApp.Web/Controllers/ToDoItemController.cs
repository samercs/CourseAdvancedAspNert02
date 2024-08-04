﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.CQRS.Queries;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Services;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController(ToDoService _service, ISender _sender) : ControllerBase
    {
        

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("get-all-query")]
        public IActionResult GetAllQuery()
        {
            var query = new GetAllQuery()
            {
                Title = "Test"
            };
            var result = _sender.Send(query).Result;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromBody]int id)
        {
            var item = _service.GetById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Post(CreateToDoItemDto item)
        {
            _service.Create(item);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UpdateToDoItemDto dto)
        {
            var result = _service.Update(id, dto);
            if (result)
            {
                return Ok();
            }

            return BadRequest("Update operation failed");
        }
        //[HttpGet("{query:alpha}")]
        [HttpGet("{query}")]
        public IActionResult Search(string query)
        {
            var result = _service.Search(query);
            return Ok(result);
        }

        [HttpPut("update-complete")]
        public IActionResult UpdateComplete(UpdateCompleteDto dto)
        {
            var result = _service.UpdateComplete(dto);
            return Ok(result);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("test");
        }
    }
}
