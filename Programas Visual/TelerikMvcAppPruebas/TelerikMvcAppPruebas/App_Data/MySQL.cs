using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace TelerikMvcApp1.App_Data
{
    public class MySql
    {
        //public static Sql SqlConn = new Sql();
        public string ConStringMaquinas = "Database=popcornonline;server=hostingmysql318.nominalia.com;user id=airpopcornonline;password=Pop8c5o42rN;";

        public string ConStringMaquinasNew = "Database=popcornonlineNew;server=hostingmysql318.nominalia.com;user id=airpopcornonline;password=Pop8c5o42rN;";

        public String ConStringGestion = "Database=airpopcorn;server=hostingmysql318.nominalia.com;user id=airpopcornonline;password=Pop8c5o42rN;";

        private MySqlConnection _cnn;
        private MySqlCommand _cmd;
        private MySqlDataAdapter _da;
        private MySqlDataReader _rd;

        public MySql(string strConn, out bool ok, out string err)
        {
            err = "";
            try
            {
                ok = Conectar(strConn, out err);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                throw;
            }
        }

        private bool Conectar(string strConn, out string err)
        {
            err = "";
            bool bRet = true;
            try
            {
                _cnn = new MySqlConnection(strConn);
                _cnn.Open();
                _cnn.Close();
            }
            catch (Exception ex)
            {
                bRet = false;
                err = ex.Message;
            }
            return bRet;
        }

        private bool Conectado()
        {
            return _cnn.State == ConnectionState.Open;
        }

        public bool Insertar(string nomTabla, DataTable dt, out string err)
        {
            bool bRet = true;
            try
            {
                err = "";
                _cnn.Open();
                _cmd = new MySqlCommand("SELECT * FROM  " + nomTabla, _cnn);
                _da = new MySqlDataAdapter(_cmd);
                DataSet ds = new DataSet();
                _da.Fill(ds, nomTabla);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(_da);

                foreach (DataRow rw in dt.Rows)
                {
                    ds.Tables[nomTabla].Rows.Add(rw.ItemArray);
                }
                _da.Update(ds, nomTabla);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                bRet = false;
            }
            finally
            {
                _cmd.Dispose();
                _cnn.Close();
            }
            return bRet;
        }

        public bool EliminarLinea(string tabla, string where, out string err)
        {
            bool bRet = true;
            err = "";
            try
            {
                if (where != string.Empty)
                {
                    _cnn.Open();
                    _cmd = new MySqlCommand("DELETE FROM " + tabla + " " + where);
                    _cmd.Connection = _cnn;
                    _cmd.ExecuteNonQuery();
                }
                else
                {
                    err = "No se han pasado parámetros WHERE";
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                err = ex.Message;
            }
            finally
            {
                _cmd.Dispose();
                _cnn.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Actualiza la tabla filtrando únicamente por un nombre de columna
        /// </summary>
        /// <param name="dt">datatable con la información</param>
        /// <param name="tabla">tabla a la que se hace referencia</param>
        /// <param name="nombreColId">Nombre de la columna a filtrar</param>
        /// <param name="err">OUT devolución de un string con errores, si lo hay</param>
        /// <returns></returns>
        public bool Actualizar(DataTable dt, string tabla, string nombreColId, out string err)
        {

            bool bRet = true;
            err = "";
            try
            {

                foreach (DataRow rw in dt.Rows)
                {
                    string cadena = "";
                    _cmd = new MySqlCommand();
                    _cmd.Parameters.AddWithValue("@" + nombreColId, rw[nombreColId].GetType()).Value = rw[nombreColId];
                    foreach (DataColumn item in rw.Table.Columns)
                    {
                        if (item.ColumnName != nombreColId)
                        {
                            _cmd.Parameters.AddWithValue("@" + item.ColumnName, rw[item].GetType()).Value = rw[item];
                            if (cadena != "")
                            {
                                cadena = cadena + "," + item.ColumnName + "= @" + item.ColumnName;
                            }
                            else
                            {
                                cadena = item.ColumnName + "= @" + item.ColumnName;
                            }
                        }
                    }
                    if (_cnn.State != ConnectionState.Open)
                    {
                        _cnn.Open();
                    }
                    _cmd.CommandText = "UPDATE " + tabla + " SET " + cadena + " WHERE " + nombreColId +
                                       "= @" + nombreColId;
                    _cmd.Connection = _cnn;
                    _cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                err = ex.Message;
                throw;
            }
            finally
            {
                _cmd.Dispose();
                _cnn.Close();
            }
            return bRet;
        }

        public bool Existe(string sentencia, out string err)
        {
            bool bRet = false;
            err = "";
            try
            {
                _cnn.Open();
                _cmd = new MySqlCommand(sentencia);
                _cmd.Connection = _cnn;
                _rd = _cmd.ExecuteReader();
                if (_rd.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(_rd);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                _cmd.Dispose();
                _cnn.Close();
            }
            return bRet;
        }

        public bool EjecutaSql(string sentencia, out string err)
        {
            bool bRet = true;
            err = "";
            try
            {
                _cnn.Open();
                _cmd = new MySqlCommand(sentencia);
                _cmd.Connection = _cnn;
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                bRet = false;
                err = ex.Message;
            }
            finally
            {
                _cmd.Dispose();
                _cnn.Close();
            }
            return bRet;
        }

        public DataTable GetDatatableSql(string sentencia, out string err)
        {
            DataTable dt = new DataTable();
            dt.TableName = "dt";
            dt.Namespace = "dt";
            err = "";
            try
            {
                _cnn.Open();
                _cmd = new MySqlCommand(sentencia);
                _cmd.Connection = _cnn;
                _rd = _cmd.ExecuteReader();
                dt.Load(_rd);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                _cmd.Dispose();
                _cnn.Close();
            }
            return dt;
        }

        public DataTable GetStructureDatatableSql(string tabla, out string err, bool conDatos, string where)
        {
            DataTable dt = new DataTable();
            err = "";
            try
            {
                _cnn.Open();
                _cmd = conDatos ? new MySqlCommand("SELECT * FROM " + tabla + " " + where) : new MySqlCommand("SELECT * FROM " + tabla + " WHERE 1=2");
                _cmd.Connection = _cnn;
                _rd = _cmd.ExecuteReader();
                dt.Load(_rd);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                if (_cmd != null)
                {
                    _cmd.Dispose();
                }
                _cnn.Close();
            }
            return dt;
        }
    }
}
