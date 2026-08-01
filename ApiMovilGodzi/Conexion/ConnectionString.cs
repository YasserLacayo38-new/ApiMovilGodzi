namespace ApiMovilGodzi.Conexion
{
    public class ConnectionString
    {
        private readonly string _connection;
        public ConnectionString(string connection)
        {
            _connection = connection;
        }
        public string Connection => _connection;
    }
}
