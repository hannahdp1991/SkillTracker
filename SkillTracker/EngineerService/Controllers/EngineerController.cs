﻿using EngineerService.Exceptions;
using EngineerService.Models;
using EngineerService.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace EngineerService.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("skill-tracker/api/v1/[controller]")]
    public class EngineerController : ControllerBase
    {
        private readonly ILogger<EngineerController> _logger;
        private readonly ISkillService _service;

        public EngineerController(ILogger<EngineerController> logger, ISkillService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Critical, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost("/add-profile")]
        public IActionResult Post([FromBody] SkillProfile skillProfile)
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, _service.Add(skillProfile));
            }
            catch(Exception exception)
            {
                _logger.Log(LogLevel.Critical, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPut("/update-profile/{userId}")]
        public IActionResult Put(string userId, [FromBody] UpdateSkillProfile skillProfile)
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, _service.Update(userId, skillProfile));
            }
            catch (UserNotFoundException exception)
            {
                _logger.Log(LogLevel.Critical, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Critical, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
