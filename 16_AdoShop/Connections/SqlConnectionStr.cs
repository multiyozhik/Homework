using System.Data.SqlClient;

namespace Connections
{
	internal static class SqlConnectionStr
	{
		public static SqlConnectionStringBuilder ConnectionString { get; }
		static SqlConnectionStr()
		{
			ConnectionString = new SqlConnectionStringBuilder()
			{
				// DataSource = "(localdb)\\MSSQLLocalDB", либо экранирование слеша @"(localdb)\MSSQLLocalDB"
				DataSource = @"(localdb)\MSSQLLocalDB",
				InitialCatalog = "HouseholdAppliancesStore",
				IntegratedSecurity = false,
				UserID = "Login",
				Password = "12345"
			};
		}
	}
}
