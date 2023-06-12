using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Connections
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		readonly string connectionString = SqlConnectionStr.ConnectionString.ToString(); //захардкоденная строка подключения

		private void MSSQLLocalDB_Click(object sender, RoutedEventArgs e)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				DisplayConnectionInformation<SqlConnection>(connection);
			}
		}

		private void MSAccess_Click(object sender, RoutedEventArgs e)
		{
			using (var connection = new OleDbConnection(AccessConnectionStr.ConnectionString))
			{
				DisplayConnectionInformation<OleDbConnection>(connection);
			}
		}

		private void DisplayConnectionInformation<T>(T connection) where T : DbConnection
		{
			try
			{
				connection.Open();
				MessageBox.Show(
					$"Строка подключения: {connection.ConnectionString}.\n" +
					$"Статус подключения: {connection.State}",
					"Сведения о подключении",
					MessageBoxButton.OK,
					MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ClientsData_Click(object sender, RoutedEventArgs e) //показать отсорт.базу клиентов по фамилии
		{
			RunCommandAndShow(new SqlCommand("SELECT * FROM Clients ORDER BY Clients.FamilyName"));
		}

		private void ShoppingData_Click(object sender, RoutedEventArgs e) //показать отсорт.базу покупок по Email
		{
			RunCommandAndShow(new SqlCommand("SELECT * FROM Shopping ORDER BY Shopping.Email"));
		}

		private void RunCommandAndShow(SqlCommand command) //выполнить команду
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				command.Connection = connection;
				var dataAdapter = new SqlDataAdapter(command);
				var dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
				DataGrid.DataContext = dataTable.DefaultView; // dataTable.DefaultView возвращает объект DataView
			}
		}

		private void ShoppingInfo_Click(object sender, RoutedEventArgs e) //выборка покупок для выбранного клиента
		{
			var selectedClient = (DataRowView)DataGrid.SelectedItem;
			if (selectedClient is null)
				return;
			var command = new SqlCommand(@"SELECT * FROM Shopping WHERE Email = @email");
			command.Parameters.Add("@email", SqlDbType.NVarChar).Value = selectedClient.Row["Email"].ToString();
			RunCommandAndShow(command);
		}

		private void AddClient_Click(object sender, RoutedEventArgs e) // добавить нового клиента
		{
			var clientDataWindow = new ClientDataWindow();
			clientDataWindow.ShowDialog();
			var newClientData = clientDataWindow.GetNewClientData();
			if (newClientData is null)
				return;
			var command = new SqlCommand(@"INSERT INTO Clients (FamilyName, FirstName, MiddleName, PhoneNumber, Email) " +
							$"VALUES (@familyName, @firstName, @middleName, @phoneNumber, @email);" +
							$"SELECT * FROM Clients ORDER BY Clients.Id");
			command.Parameters.AddRange(SetCommandParameters(newClientData));
			RunCommandAndShow(command);
		}
		
		private void ChangeClientData_Click(object sender, RoutedEventArgs e) //изменить данные выбранного клиента
		{
			var selectedClient = (DataRowView)DataGrid.SelectedItem;
			if (selectedClient is null)
				return;
			var clientDataWindow = new ClientDataWindow();
			clientDataWindow.FamilyNameTexBox.Text = selectedClient.Row["FamilyName"].ToString();
			clientDataWindow.FirstNameTextBox.Text = selectedClient.Row["FirstName"].ToString();
			clientDataWindow.MiddleNameTextBox.Text = selectedClient.Row["MiddleName"].ToString();
			clientDataWindow.PhoneNumberTextBox.Text = selectedClient.Row["PhoneNumber"].ToString();
			clientDataWindow.EmailTextBox.Text = selectedClient.Row["Email"].ToString(); ;
			clientDataWindow.ShowDialog();
			var newClientData = clientDataWindow.GetNewClientData();
			if (newClientData is null)
				return;
			var command = new SqlCommand(@"UPDATE Clients SET 
				FamilyName=@familyName, FirstName=@firstName, MiddleName=@middleName, PhoneNumber=@phoneNumber 
				WHERE Email=@email; SELECT * FROM Clients ORDER BY Clients.Id");
			command.Parameters.AddRange(SetCommandParameters(newClientData));
			RunCommandAndShow(command);
		}

		//установить параметры для параметризированного запроса для базы клиентов
		private SqlParameter[] SetCommandParameters(ClientData newClientData)
		{
			var commandParameters = new SqlParameter[]
			{
				new SqlParameter("@familyName", newClientData.FamilyName),
				new SqlParameter("@firstName", newClientData.FirstName),
				new SqlParameter("@middleName", newClientData.MiddleName),
				new SqlParameter("@phoneNumber", newClientData.PhoneNumber),
				new SqlParameter("@email", newClientData.Email)
			};
			foreach (var parameter in commandParameters)
				parameter.SqlDbType = SqlDbType.NVarChar;
			return commandParameters;
		}

		private void DoShopping_Click(object sender, RoutedEventArgs e) // совершить новую покупку клиентом
		{
			var selectedClient = (DataRowView)DataGrid.SelectedItem;
			var newProductWindow = new NewProductWindow();
			newProductWindow.ShowDialog();
			var newProductData = newProductWindow.GetNewProductData();
			if (newProductData is null)
				return;
			var command = new SqlCommand(@"INSERT INTO Shopping (Email, ProductCode, ProductName) " +
								$"VALUES (@email, @productCode, @productName);" +
								$"SELECT * FROM Shopping ORDER BY Shopping.Id");
			var commandParameters = new SqlParameter[]
			{
				new SqlParameter("@email", selectedClient.Row["Email"].ToString()),
				new SqlParameter("@productCode", newProductData.ProductCode),
				new SqlParameter("@productName", newProductData.ProductName)
			};
			foreach (var parameter in commandParameters)
				parameter.SqlDbType = SqlDbType.NVarChar;
			command.Parameters.AddRange(commandParameters);
			RunCommandAndShow(command);
		}

		private void DeleteClient_Click(object sender, RoutedEventArgs e) //удалить клиента и его покупки
		{
			var selectedClient = (DataRowView)DataGrid.SelectedItem;
			var command = new SqlCommand(@"DELETE FROM Clients WHERE Email=@email;
												DELETE FROM Shopping WHERE Email=@email;
												SELECT * FROM Clients ORDER BY Clients.Id");
			command.Parameters.Add("@email", SqlDbType.NVarChar).Value = selectedClient.Row["Email"];
			RunCommandAndShow(command);
		}

		private void ClearDatabase_Click(object sender, RoutedEventArgs e) // очистить базу клиентов и базу покупок
		{
			RunCommandAndShow(new SqlCommand("DELETE FROM Clients; DELETE FROM Shopping"));
		}
	}
}
