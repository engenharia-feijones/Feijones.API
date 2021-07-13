using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class VendaBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<VendaBO> _log;
		private readonly IConfiguration _config;

		public VendaBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<VendaBO>();
			_config = config;
		}

		#region Change Data

		public VendaModel Insert(VendaModel venda)
		{
			VendaRepository VendaRepository;

			try
			{
				VendaRepository = new VendaRepository(_loggerFactory, _config);

				if (venda.ID == 0)
				{
					venda = VendaRepository.Insert(venda);
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

			return venda;
		}

		public VendaModel Update(VendaModel venda)
		{
			VendaRepository vendaRepository;

			try
			{
				vendaRepository = new VendaRepository(_loggerFactory, _config);

				if (venda.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					vendaRepository.Update(venda);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return venda;
		}

		public void Delete(int id)
		{
			VendaRepository vendaRepository;
			
			VendaModel venda;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					vendaRepository = new VendaRepository(_loggerFactory, _config);
					venda = Get(id);
					if (vendaRepository != null)
					{

						vendaRepository.Delete(id);
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

		public VendaModel Get(int id)
		{
			VendaRepository vendaRepository;
			VendaModel venda;

			try
			{
				vendaRepository = new VendaRepository(_loggerFactory, _config);

				venda = vendaRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return venda;
		}

		public List<VendaModel> Get( )
		{
			VendaRepository vendaRepository;
			List<VendaModel> produtos;

			try
			{
				vendaRepository = new VendaRepository(_loggerFactory, _config);

				produtos = vendaRepository.Get();
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
