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

	public class QuotasController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CustomersController> _log;
		private readonly IConfiguration _config;

		public QuotasController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CustomersController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get()
		{
			QuotasBO quotasBO;
			List<Quota> quotas;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				quotasBO = new QuotasBO(_loggerFactory, _config);
				quotas = quotasBO.Get();

				response = Ok(quotas);

				_log.LogInformation($"Finishing Get() with '{quotas.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetQuotas")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			QuotasBO quotasBO;
			Quota quota;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				quotasBO = new QuotasBO(_loggerFactory, _config);

				quota = quotasBO.Get(id);

				if (quota != null)
				{
					response = Ok(quota);
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
		public IActionResult Post([FromBody] Quota quota)
		{
			QuotasBO quotasBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(quota, Formatting.None)}')");

				quotasBO = new QuotasBO(_loggerFactory, _config);

				quota = quotasBO.Insert(quota);

				response = Ok(quota);

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
		public IActionResult Put(int id, Quota quota)
		{
			QuotasBO quotasBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(quota, Formatting.None)}')");

				quotasBO = new QuotasBO(_loggerFactory, _config);

				quota.ID = id;
				quota = quotasBO.Update(quota);

				response = Ok(quota);

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
			QuotasBO quotasBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				quotasBO = new QuotasBO(_loggerFactory, _config);
				quotasBO.Delete(id);

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
