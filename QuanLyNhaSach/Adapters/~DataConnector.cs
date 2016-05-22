using QuanLyNhaSach.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuanLyNhaSach.Adapters
{
    public class DataConnector : IDisposable
    {
        public static string ConnectionString { get { return System.Configuration.ConfigurationManager.ConnectionStrings["sql_connection_string"].ConnectionString; } }

        private DataConnector()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            Remover = new DispatcherTimer();
            Remover.Interval = new TimeSpan(0, 0, 0, 1);
            Remover.Tick += Remover_Tick;
        }

        private DispatcherTimer Remover;

        private SqlConnection Connection;

        void Remover_Tick(object sender, EventArgs e)
        {
            if (Connection.State == System.Data.ConnectionState.Fetching)
                return;
            if (Connection.State == System.Data.ConnectionState.Executing)
                return;
            if (Connection.State == System.Data.ConnectionState.Connecting)
                return;
            Connection.Close();
            Remover.Stop();
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public static int ExecuteNonQuery(string query)
        {
            try
            {
                var command = new SqlCommand(query, (new DataConnector()).Connection);
                var result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                ErrorManager.Current.QueryError.Call(ex.Message);
            }
            return -1;
        }

        public static SqlDataReader ExecuteQuery(string query)
        {
            try
            {
                var command = new SqlCommand(query, (new DataConnector()).Connection);
                var reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                ErrorManager.Current.QueryError.Call(ex.Message);
            }
            return null;
        }
    }
}
