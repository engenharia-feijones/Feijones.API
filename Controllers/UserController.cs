using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MultipliqueV2.API.Business;
using MultipliqueV2.API.Model;

namespace MultipliqueV2.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class UserController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<UserController> _log;
		private readonly IConfiguration _config;

		public UserController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<UserController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get()
		{
			UserBO UserBO;
			List<User> user;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				UserBO = new UserBO(_loggerFactory, _config);
				user = UserBO.Get();

				response = Ok(user);

				_log.LogInformation($"Finishing Get() with '{user.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetDescription")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			UserBO UserBO;
			User user;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				UserBO = new UserBO(_loggerFactory, _config);

				user = UserBO.Get(id);

				if (user != null)
				{
					response = Ok(user);
				}
				else
				{
					response = NotFound(string.Empty);
				}

				_log.LogInformation($"Finishing Get( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Post([FromBody] User user)
		{
			UserBO UserBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(user, Formatting.None)}')");

				UserBO = new UserBO(_loggerFactory, _config);

				user = UserBO.Insert(user);

				response = Ok(user);

				_log.LogInformation($"Finishing Post");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// PUT: api/Cliente/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Put(int id, User user)
		{
			UserBO UserBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(user, Formatting.None)}')");

				UserBO = new UserBO(_loggerFactory, _config);

				user.ID = id;
				user = UserBO.Update(user);

				response = Ok(user);

				_log.LogInformation($"Finishing Put( {id} )");
			}	
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Delete(int id)
		{
			UserBO UserBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				UserBO = new UserBO(_loggerFactory, _config);
				UserBO.Delete(id);

				response = Ok(string.Empty);

				_log.LogInformation($"Finishing Delete( {id} )");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}
	}
}
