using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
namespace EmployeesService.Data
{
    public class Connection
    {
        //Codigo de Conexion configurada en la BD
        private string _ConnectionString = null;
        private int _CommandTimeout = 0;

        private SqlCommand _sqlComand = null;
        private DataSet _DataSet = null;
        private SqlDataAdapter _sqlDataAdapter = null;

        private SqlConnection _Cnn = null;
        private SqlTransaction _sqlTransaction = null;


        private SqlConnection Cnn
        {
            get
            {

                //si es null se crea la instancia la primera vez
                if (_Cnn == null)
                    _Cnn = ObtenerConexion();

                //se abre la connexion en caso que este cerrada
                if (_Cnn.State != ConnectionState.Open)
                    _Cnn.Open();

                return _Cnn;
            }
        }

        //Constructor que inicializa el codigo de conexion 
        public Connection(string ConnectionString, int CommandTimeout = 0)
        {
            _ConnectionString = ConnectionString;
            _CommandTimeout = CommandTimeout;
        }

        //Metodo Privado que generará la nueva conection string
        private SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_ConnectionString);
        }

        private void CloseConnection()
        {
                if (_Cnn != null)
                    if (_Cnn.State == ConnectionState.Open)
                    {
                        _Cnn.Close();
                        _Cnn.Dispose();
                    }
        }
        private List<T> GetList<T>(string JSONString)
        {
            List<T> Result = new List<T>();

            Result = JsonConvert.DeserializeObject<List<T>>(JSONString);

            return Result;
        }

        //Metodo generico que retorna una string Json desde una consuta o un proceso sin parametros

        public List<T> SqlQuery<T>(string Query)
        {

            try
            {

                _sqlComand = new SqlCommand(Query, Cnn, _sqlTransaction);
                _sqlComand.CommandTimeout = _CommandTimeout;

                _DataSet = new DataSet();
                _sqlDataAdapter = new SqlDataAdapter(_sqlComand);
                _sqlDataAdapter.Fill(_DataSet);

                string result = JsonConvert.SerializeObject(_DataSet.Tables[0]);

                return GetList<T>(result);


            }
            catch (Exception ex)
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();

                throw ex;
            }
            finally
            {
                //Se cierra la conexion si esta no hay transaccion abierta
                CloseConnection();
            }

        }

    }
}
