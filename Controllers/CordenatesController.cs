/*using System;
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
using Cliente.API.Business;
using Cliente.API.Model;

namespace Cliente.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class CordenatesController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CordenatesController> _log;
		private readonly IConfiguration _config;

		public CordenatesController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CordenatesController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get(string name = null)
		{
			CordenatesBO cordenatesBO;
			List<CordenatesModel> cordenates;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				cordenatesBO = new CordenatesBO(_loggerFactory, _config);
				cordenates = cordenatesBO.Get(name);

				response = Ok(cordenates);

				_log.LogInformation($"Finishing Get() with '{cordenates.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetCordenates")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			CordenatesBO cordenatesBO;
			CordenatesModel cordenate;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				cordenatesBO = new CordenatesBO(_loggerFactory, _config);

				cordenate = cordenatesBO.Get(id);

				if (cordenate != null)
				{
					response = Ok(cordenate);
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
		public IActionResult Post([FromBody] CordenatesModel cordenate)
		{
			CordenatesBO cordenatesBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(cordenate, Formatting.None)}')");

				cordenatesBO = new CordenatesBO(_loggerFactory, _config);

				cordenate = cordenatesBO.Insert(cordenate);

				response = Ok(cordenate);

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
		public IActionResult Put(int id, CordenatesModel cordenate)
		{
			CordenatesBO cordenatesBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(cordenate, Formatting.None)}')");

				cordenatesBO = new CordenatesBO(_loggerFactory, _config);

				cordenate.ID = id;
				cordenate = cordenatesBO.Update(cordenate);

				response = Ok(cordenate);

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
			CordenatesBO cordenatesBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				cordenatesBO = new CordenatesBO(_loggerFactory, _config);
				cordenatesBO.Delete(id);

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
*/