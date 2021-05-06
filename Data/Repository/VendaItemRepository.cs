using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cliente.API.Data.Base;
using Cliente.API.Model;



namespace Cliente.API.Data.Repository
{
	public class VendaItemRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<VendaItemRepository> _log;
		private readonly IConfiguration _config;

		public VendaItemRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<VendaItemRepository>();
			_config = config;
		}

		#region LoadModel

		private List<VendaItemModel> Load(DataSet data)
		{
			List<VendaItemModel> vendas;
			VendaItemModel vendaItem;

			try
			{
				vendas = new List<VendaItemModel>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					vendaItem = new VendaItemModel();

					vendaItem.ID = row.Field<int>("ID");
					vendaItem.ID_prod = row.Field<int>("ID_prod");
					vendaItem.qtd = row.Field<int>("qtd");
					vendaItem.preco = row.Field<decimal>("preco");


					vendas.Add(vendaItem);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return vendas;
		}

		#endregion

		#region Change Data

		public VendaItemModel Insert(VendaItemModel VendaItem)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"SET ANSI_WARNINGS  OFF; INSERT INTO Venda_Item
											(
												
												
												
												 
												ID_prod
												,qtd
												,preco
												
										
											)
										 OUTPUT inserted.ID
										 VALUES
											(
												 
												
												
												 
												@ID_prod
												,@qtd
												,@preco
												
										

											)");


				
				
				command.Parameters.AddWithValue("ID_prod", VendaItem.ID_prod.AsDbValue());
				command.Parameters.AddWithValue("qtd", VendaItem.qtd.AsDbValue());
				command.Parameters.AddWithValue("preco", VendaItem.preco.AsDbValue());
				



				VendaItem.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return VendaItem;
		}

		public VendaItemModel Update(VendaItemModel VendaItem)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@" SET ANSI_WARNINGS  OFF; UPDATE Venda_Item SET

												 
												
												
												ID= @ID
												,ID_prod= @ID_prod
												,qtd= @qtd
												,preco= @preco
												
											

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", VendaItem.ID.AsDbValue());
				command.Parameters.AddWithValue("ID_prod", VendaItem.ID_prod.AsDbValue());
				command.Parameters.AddWithValue("qtd", VendaItem.qtd.AsDbValue());
				command.Parameters.AddWithValue("preco", VendaItem.preco.AsDbValue());


				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return VendaItem;
		}

		public bool Delete(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from venda_item WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", id.AsDbValue());

				result = dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return (result > 0);
		}

        #endregion

        #region Retrieve Data

        public VendaItemModel Get(int ID)
        {
            SqlHelper dataConnection;
            SqlCommand command;
            DataSet dataSet;

            VendaItemModel Cliente;

            try
            {
                dataConnection = new SqlHelper(_loggerFactory, _config);

                command = new SqlCommand($"SELECT * FROM Venda_Item WHERE ID = @ID");
                command.Parameters.AddWithValue("ID", ID.AsDbValue());

                dataSet = dataConnection.ExecuteDataSet(command);

                Cliente = Load(dataSet).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Cliente;
        }

        public List<VendaItemModel> Get()
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<VendaItemModel> VendaItem;
			

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Venda_Item");
			
				dataSet = dataConnection.ExecuteDataSet(command);

				VendaItem = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return VendaItem;
		}

		#endregion
	}
}



