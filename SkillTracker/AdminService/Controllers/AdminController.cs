using AdminService.Helpers;
using AdminService.Models;
using AdminService.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdminService.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("skill-tracker/api/v1/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ISkillProfileService _service;
        private readonly ICriteriaBuilder _builder;

        public AdminController(ISkillProfileService service, ILogger<AdminController> logger, ICriteriaBuilder criteriaBuilder)
        {
            _logger = logger;
            _service = service;
            _builder = criteriaBuilder;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return StatusCode((int)HttpStatusCode.OK, _service.Get());
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Critical, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpGet("{criteria}/{criteriaValue}")]
        public IActionResult Get(string criteria, string criteriaValue)
        
        {
            try
            {
                _builder.Build(criteria, criteriaValue);
                return StatusCode((int)HttpStatusCode.OK, _service.SearchProfile(_builder.getCriteria()));
            }
            catch(Exception exception)
            {
                _logger.Log(LogLevel.Critical, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
