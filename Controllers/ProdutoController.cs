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

	public class ProdutoController : ControllerBase
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ProdutoController> _log;
		private readonly IConfiguration _config;

		public ProdutoController(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ProdutoController>();
			_config = config;
		}


		[HttpGet]
		
		public IActionResult Get(string name = null)
		{
			ProdutoBO produtoBO;
			List<ProdutoModel> enderecos;
			ObjectResult response;

			try
			{
				_log.LogInformation("Starting Get()");

				produtoBO = new ProdutoBO(_loggerFactory, _config);
				enderecos = produtoBO.Get(name);

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
		[HttpGet("{id}", Name = "GetProduto")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Get(int id)
		{
			ProdutoBO produtoBO;
			ProdutoModel produto;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Get( {id} )");

				produtoBO = new ProdutoBO(_loggerFactory, _config);

				produto = produtoBO.Get(id);

				if (produto != null)
				{
					response = Ok(produto);
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
		public IActionResult Post([FromBody] ProdutoModel produto)
		{
			ProdutoBO produtoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Post('{JsonConvert.SerializeObject(produto, Formatting.None)}')");

				produtoBO = new ProdutoBO(_loggerFactory, _config);

				produto = produtoBO.Insert(produto);

				response = Ok(produto);

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
		public IActionResult Put(int id, ProdutoModel produto)
		{
			ProdutoBO produtoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Put( {id}, '{JsonConvert.SerializeObject(produto, Formatting.None)}')");

				produtoBO = new ProdutoBO(_loggerFactory, _config);

				produto.ID = id;
				produto = produtoBO.Update(produto);

				response = Ok(produto);

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
			ProdutoBO produtoBO;
			ObjectResult response;

			try
			{
				_log.LogInformation($"Starting Delete( {id} )");

				produtoBO = new ProdutoBO(_loggerFactory, _config);
				produtoBO.Delete(id);

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
