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
    public class UserRepository
	{

		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<UserRepository> _log;
		private readonly IConfiguration _config;

		public UserRepository(ILoggerFactory loggerFactory, IConfiguration config)
		{
			_loggerFactory = loggerFactory;
			_log = loggerFactory.CreateLogger<UserRepository>();
			_config = config;
		}

		#region LoadModel

		private List<User> Load(DataSet data)
		{
			List<User> users;
			User user;

			try
			{
				users = new List<User>();

				foreach (DataRow row in data.Tables[0].Rows)
				{
					user = new User();

					user.ID = row.Field<int>("ID");
					user.Name = row.Field<string>("Name");
					user.Type = (UserType) row.Field<int>("Type");

					users.Add(user);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return users;
		}

		#endregion

		#region Change Data

		public User Insert(User user)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"INSERT INTO Users
											(
												 
												Name
												,Type
											
											)
										 OUTPUT inserted.ID 
										 VALUES
											(
												 
												@Name
												,@Type
											
											)");

				
				command.Parameters.AddWithValue("Name", user.Name.AsDbValue());
				command.Parameters.AddWithValue("Type", user.Type.AsDbValue());



				user.ID = (int)dataConnection.ExecuteScalar(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return user;
		}

		public User Update(User user)
		{
			SqlHelper dataConnection;
			SqlCommand command;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"UPDATE Users SET
 
												Name = @Name
												,Type = @Type
											

											WHERE ID = @ID");

				command.Parameters.AddWithValue("ID", user.ID.AsDbValue());
				command.Parameters.AddWithValue("Name", user.Name.AsDbValue());
				command.Parameters.AddWithValue("Type", user.Type.AsDbValue());
			


				dataConnection.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return user;
		}

		public bool Delete(long id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			int result;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($@"DELETE from Users WHERE ID = @ID");

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

		public User Get(int id)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			User user;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"select *from Users where ID = @ID ");
				command.Parameters.AddWithValue("ID", id.AsDbValue());

				dataSet = dataConnection.ExecuteDataSet(command);

				user = Load(dataSet).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return user;
		}

		public List<User> Get(string Name = null)
		{
			SqlHelper dataConnection;
			SqlCommand command;
			DataSet dataSet;

			List<User> users;
			List<string> clauses;

			try
			{
				dataConnection = new SqlHelper(_loggerFactory, _config);

				command = new SqlCommand($"SELECT * FROM Users");

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

				users = Load(dataSet);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return users;
		}

		#endregion
	}

}

