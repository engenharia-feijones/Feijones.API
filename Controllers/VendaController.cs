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
using Cliente.API.Business;
using Cliente.API.Model;

namespace Cliente.API.Controllers
{
	[EnableCors("Policy1")]
	[ApiController]
	[Route("api/[controller]")]

	public class VendaController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<VendaController> _log;
		private readonly IConfiguration _config;

		public VendaController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<VendaController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get()
		{
			VendaBO vendaBO;
			List<VendaModel> enderecos;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				vendaBO = new VendaBO(_loggerFactory, _config);
				enderecos = vendaBO.Get();

				response = Ok(enderecos);

				_log.LogInformation($"Finishing Get() with '{enderecos.Count}' results");
			}
			catch (Exception ex)
			{
				_log.LogError(ex.Message);
				response = StatusCode(500, ex.Message);
			}

			return response;
		}

		// GET: api/Cliente/5
		[HttpGet("{id}", Name = "GetVenda")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			VendaBO vendaBO;
			VendaModel venda;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				vendaBO = new VendaBO(_loggerFactory, _config);

				venda = vendaBO.Get(id);

				if (venda != null)
				{
					response = Ok(venda);
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
		public IActionResult Post([FromBody] VendaModel venda)
		{
			VendaBO vendaBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(venda, Formatting.None)}')");

				vendaBO = new VendaBO(_loggerFactory, _config);

				venda = vendaBO.Insert(venda);

				response = Ok(venda);

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
		public IActionResult Put(int id, VendaModel venda)
		{
			VendaBO vendaBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(venda, Formatting.None)}')");

				vendaBO = new VendaBO(_loggerFactory, _config);

				venda.ID = id;
				venda = vendaBO.Update(venda);

				response = Ok(venda);

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
			VendaBO vendaBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				vendaBO = new VendaBO(_loggerFactory, _config);
				vendaBO.Delete(id);

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
