using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class ProdutoBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<ProdutoBO> _log;
		private readonly IConfiguration _config;

		public ProdutoBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<ProdutoBO>();
			_config = config;
		}

		#region Change Data

		public ProdutoModel Insert(ProdutoModel produto)
		{
			ProdutoRepository ProdutoRepository;

			try
			{
				ProdutoRepository = new ProdutoRepository(_loggerFactory, _config);

				if (produto.ID == 0)
				{
					produto = ProdutoRepository.Insert(produto);
				}
				else
				{
					throw new Exception("ID diferente de 0, avalie a utilização do PUT");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return produto;
		}

		public ProdutoModel Update(ProdutoModel produto)
		{
			ProdutoRepository produtoRepository;

			try
			{
				produtoRepository = new ProdutoRepository(_loggerFactory, _config);

				if (produto.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					produtoRepository.Update(produto);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return produto;
		}

		public void Delete(int id)
		{
			ProdutoRepository produtoRepository;
			
			ProdutoModel produto;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					produtoRepository = new ProdutoRepository(_loggerFactory, _config);
					produto = Get(id);
					if (produtoRepository != null)
					{

						produtoRepository.Delete(id);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		#endregion

		#region Retrieve Repository

		public ProdutoModel Get(int id)
		{
			ProdutoRepository produtoRepository;
			ProdutoModel produto;

			try
			{
				produtoRepository = new ProdutoRepository(_loggerFactory, _config);

				produto = produtoRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return produto;
		}

		public List<ProdutoModel> Get( string nome = null)
		{
			ProdutoRepository produtoRepository;
			List<ProdutoModel> produtos;

			try
			{
				produtoRepository = new ProdutoRepository(_loggerFactory, _config);

				produtos = produtoRepository.Get(nome);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return produtos;
		}

		#endregion
	}

	
}
