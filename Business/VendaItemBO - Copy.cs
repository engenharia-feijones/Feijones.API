using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Cliente.API.Data.Repository;
using Cliente.API.Model;

namespace Cliente.API.Business
{
	public class VendaItemBO
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<VendaItemBO> _log;
		private readonly IConfiguration _config;

		public VendaItemBO(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<VendaItemBO>();
			_config = config;
		}

		#region Change Data

		public VendaItemModel Insert(VendaItemModel vendaItem)
		{
			VendaItemRepository VendaItemRepository;

			try
			{
				VendaItemRepository = new VendaItemRepository(_loggerFactory, _config);

				if (vendaItem.ID == 0)
				{
					vendaItem = VendaItemRepository.Insert(vendaItem);
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

			return vendaItem;
		}

		public VendaItemModel Update(VendaItemModel vendaItem)
		{
			VendaItemRepository vendaItemRepository;

			try
			{
				vendaItemRepository = new VendaItemRepository(_loggerFactory, _config);

				if (vendaItem.ID == 0)
				{
					throw new Exception("ID diferente de 0, avalie a utilização do POST");
				}
				else
				{
					vendaItemRepository.Update(vendaItem);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return vendaItem;
		}

		public void Delete(int id)
		{
			VendaItemRepository vendaItemRepository;
			
			VendaItemModel vendaItem;

			try
			{
				if (id == 0)
				{
					throw new Exception("ID inválido");
				}
				else
				{
					vendaItemRepository = new VendaItemRepository(_loggerFactory, _config);
					vendaItem = Get(id);
					if (vendaItemRepository != null)
					{

						vendaItemRepository.Delete(id);
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

		public VendaItemModel Get(int id)
		{
			VendaItemRepository vendaItemRepository;
			VendaItemModel vendaItem;

			try
			{
				vendaItemRepository = new VendaItemRepository(_loggerFactory, _config);

				vendaItem = vendaItemRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return vendaItem;
		}

		public List<VendaItemModel> Get( )
		{
			VendaItemRepository vendaItemRepository;
			List<VendaItemModel> produtos;

			try
			{
				vendaItemRepository = new VendaItemRepository(_loggerFactory, _config);

				produtos = vendaItemRepository.Get();
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
