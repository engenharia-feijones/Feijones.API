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
	public class VendaRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<VendaRepository> _log;
		private readonly IConfiguration _config;

		public VendaRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<VendaRepository>();
			_config = config;
		}

		#region LoadModel

		private List<VendaModel> Load(DataSet data)
		{
			List<VendaModel> vendas;
			VendaModel venda;

			try
			{
				vendas = new List<VendaModel>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					venda = new VendaModel();

					venda.ID = row.Field<int>("ID");
					venda.ID_cliente = row.Field<int>("ID_cliente");
					venda.ID_end = row.Field<int>("ID_end");
					venda.total = row.Field<decimal>("total");
					venda.ID_venda_item = row.Field<int>("ID_venda_item");

					vendas.Add(venda);
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

		public VendaModel Insert(VendaModel Venda)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"SET ANSI_WARNINGS  OFF; INSERT INTO Venda
											(
												
												
												
												ID_cliente
												,ID_end
												,total
												,ID_venda_item
										
											)
										 OUTPUT inserted.ID
										 VALUES
											(
												 
												
												
												@ID_cliente
												,@ID_end
												,@total
												,@ID_venda_item
										

											)");


				
				command.Parameters.AddWithValue("ID_cliente", Venda.ID_cliente.AsDbValue());
				command.Parameters.AddWithValue("ID_end", Venda.ID_end.AsDbValue());
				command.Parameters.AddWithValue("total", Venda.total.AsDbValue());
				command.Parameters.AddWithValue("ID_venda_item", Venda.ID_venda_item.AsDbValue());



				Venda.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Venda;
		}

		public VendaModel Update(VendaModel Venda)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@" SET ANSI_WARNINGS  OFF; UPDATE Venda SET

												 
												
												
												ID_cliente= @ID_cliente
												,ID_end= @ID_end
												,total= @total
												,ID_venda_item=@ID_venda_item
											

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", Venda.ID.AsDbValue());
				command.Parameters.AddWithValue("ID_cliente", Venda.ID_cliente.AsDbValue());
				command.Parameters.AddWithValue("ID_end", Venda.ID_end.AsDbValue());
				command.Parameters.AddWithValue("total", Venda.total.AsDbValue());
				command.Parameters.AddWithValue("ID_venda_item", Venda.ID_venda_item.AsDbValue());


				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Venda;
		}

		public bool Delete(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from venda WHERE ID = @ID");

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

        public VendaModel Get(int ID)
        {
            SqlHelper dataConnection;
            SqlCommand command;
            DataSet dataSet;

            VendaModel Cliente;

            try
            {
                dataConnection = new SqlHelper(_loggerFactory, _config);

                command = new SqlCommand($"SELECT * FROM Venda WHERE ID = @ID");
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

        public List<VendaModel> Get()
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<VendaModel> Venda;
			

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Venda");
			
				dataSet = dataConnection.ExecuteDataSet(command);

				Venda = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return Venda;
		}

		#endregion
	}
}



