using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MultipliqueV2.API.Data.Base;
using MultipliqueV2.API.Model;


namespace MultipliqueV2.API.Data.Repository
{
    public class CostumerRepository
    {

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<CostumerRepository> _log;
		private readonly IConfiguration _config;

		public CostumerRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<CostumerRepository>();
			_config = config;
		}

		#region LoadModel

		private List<Customer> Load(DataSet data)
		{
			List<Customer> customers;
			Customer customer;

			try
			{
				customers = new List<Customer>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					customer = new Customer();

					customer.ID = row.Field<int>("ID");
					customer.UserID = row.Field<int>("UserID");
					customer.Name = row.Field<string>("Name");
					customer.Phone = row.Field<string>("Phone");
					customer.Email = row.Field<string>("Email");




					customers.Add(customer);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customers;
		}

		#endregion

		#region Change Data

		public Customer Insert(Customer customer)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO Customers
											(
												 
												UserID
												,Name
												,Phone
												,Email
											
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 
												@UserID
												,@Name
												,@Phone
												,@Email
											
											)");

				
				command.Parameters.AddWithValue("UserID", customer.UserID.AsDbValue());
				command.Parameters.AddWithValue("Name", customer.Name.AsDbValue());
				command.Parameters.AddWithValue("Phone", customer.Phone.AsDbValue());
				command.Parameters.AddWithValue("Email", customer.Email.AsDbValue());



				customer.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customer;
		}

		public Customer Update(Customer customer)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE Customers SET
 
												UserID = @UserID
												,Name = @Name
												,Phone = @Phone
												,Email = @Email

											

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", customer.ID.AsDbValue());
				command.Parameters.AddWithValue("UserID", customer.UserID.AsDbValue());
				command.Parameters.AddWithValue("Name", customer.Name.AsDbValue());
				command.Parameters.AddWithValue("Phone", customer.Phone.AsDbValue());
				command.Parameters.AddWithValue("Email", customer.Email.AsDbValue());



				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customer;
		}

		public bool Delete(long id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from Customers WHERE ID = @ID");

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

		public Customer Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			Customer customer;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Customers WHERE ID = @ID");
				command.Parameters.AddWithValue("ID", id.AsDbValue());

				dataSet = dataConnection.ExecuteDataSet(command);

				customer = Load(dataSet).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customer;
		}

		public List<Customer> Get(string Name = null)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<Customer> customers;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Customers");

				clauses = new List<string>();
				if (!string.IsNullOrEmpty(Name))
				{
					clauses.Add($"Name like '%' + @Name + '%'");
					command.Parameters.AddWithValue("Name", Name.AsDbValue());
				}

				if (clauses.Count > 0)
				{
					command.CommandText += $" WHERE {string.Join(" and ", clauses)}";
				}

				dataSet = dataConnection.ExecuteDataSet(command);

				customers = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return customers;
		}

		#endregion
	}

}

