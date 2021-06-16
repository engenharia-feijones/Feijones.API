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

	public class IncomesController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<IncomesController> _log;
		private readonly IConfiguration _config;

		public IncomesController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<IncomesController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get()
		{
			IncomesBO incomesBO;
			List<Income> incomes;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				incomesBO = new IncomesBO(_loggerFactory, _config);
				incomes = incomesBO.Get();

				response = Ok(incomes);

				_log.LogInformation($"Finishing Get() with '{incomes.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetIncomes")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			IncomesBO incomesBO;
			Income income;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				incomesBO = new IncomesBO(_loggerFactory, _config);

				income = incomesBO.Get(id);

				if (income != null)
				{
					response = Ok(income);
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
		public IActionResult Post([FromBody] Income income)
		{
			IncomesBO incomesBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(income, Formatting.None)}')");

				incomesBO = new IncomesBO(_loggerFactory, _config);

				income = incomesBO.Insert(income);

				response = Ok(income);

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
		public IActionResult Put(int id, Income income)
		{
			IncomesBO incomesBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(income, Formatting.None)}')");

				incomesBO = new IncomesBO(_loggerFactory, _config);

				income.ID = id;
				income = incomesBO.Update(income);

				response = Ok(income);

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
			IncomesBO incomesBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				incomesBO = new IncomesBO(_loggerFactory, _config);
				incomesBO.Delete(id);

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
