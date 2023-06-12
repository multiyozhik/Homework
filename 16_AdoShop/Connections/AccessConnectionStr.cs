namespace Connections
{
	internal static class AccessConnectionStr
	{
		public static string ConnectionString { get; }
		static AccessConnectionStr()
		{
			ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; " +
				"Data Source=C:\\!Programming\\AccessDB.accdb;" +
				"Jet OLEDB:Database Password=Login;";
		}
	}
}
