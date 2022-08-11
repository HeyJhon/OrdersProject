using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Order_Exam.DAL
{
    public static class ContextDB
    {
        private static string stringConection = System.Configuration.ConfigurationManager.ConnectionStrings["ConectionDB"].ToString();
        public static int ExecuteAction(string nombreSp, List<SqlParameter> parametros = null)
        {
            try
            {
                using (SqlConnection conexionBD = new SqlConnection(stringConection))
                {
                    SqlCommand accionBD = new SqlCommand(nombreSp);
                    accionBD.CommandType = CommandType.StoredProcedure;
                    accionBD.Connection = conexionBD;

                    if (parametros != null)
                        foreach (SqlParameter param in parametros)
                        {
                            accionBD.Parameters.Add(param);
                        }

                    if (conexionBD.State != ConnectionState.Open)
                        conexionBD.Open();

                    var resultado = Convert.ToInt32(accionBD.ExecuteScalar());
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static T Search<T>(string nombreStoredProcedure, List<SqlParameter> sqlParameters = null) where T : new()
        {
            try
            {
                using (SqlConnection conexionBD = new SqlConnection(stringConection))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conexionBD;
                        if (sqlParameters != null)
                        {
                            foreach (var sqlParameter in sqlParameters)
                            {
                                cmd.Parameters.Add(sqlParameter);
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = nombreStoredProcedure;
                        if (conexionBD.State != ConnectionState.Open)
                            conexionBD.Open();

                        SqlDataReader dr = cmd.ExecuteReader();
                        return dr.FillSearch<T>();
                    }

                }
            }

            catch
            {
                return default(T);
            }
        }

        private static T FillSearch<T>(this SqlDataReader reader) where T : new()
        {
            T t = new T();
            if (reader.Read())
            {
                for (int inc = 0; inc < reader.FieldCount; inc++)
                {
                    Type type = t.GetType();
                    string name = reader.GetName(inc);
                    PropertyInfo prop = type.GetProperty(name);
                    if (prop != null)
                    {
                        var value = reader.GetValue(inc);
                        if (value != DBNull.Value)
                        {
                            Type tipo = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object safeValue = (value == null) ? null : Convert.ChangeType(value, tipo);
                            prop.SetValue(t, safeValue, null);

                        }
                    }
                }

            }
            reader.Close();

            return t;
        }
        public static List<T> List<T>(string nombreStoredProcedure, List<SqlParameter> sqlParameters = null) where T : new()
        {
            try
            {
                using (SqlConnection conexionBD = new SqlConnection(stringConection))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conexionBD;
                        if (sqlParameters != null)
                        {
                            foreach (var sqlParameter in sqlParameters)
                            {
                                cmd.Parameters.Add(sqlParameter);
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = nombreStoredProcedure;
                        if (conexionBD.State != ConnectionState.Open)
                            conexionBD.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        return dr.FillList<T>();
                    }
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        private static List<T> FillList<T>(this SqlDataReader reader) where T : new()
        {
            List<T> res = new List<T>();
            while (reader.Read())
            {
                T t = new T();
                for (int inc = 0; inc < reader.FieldCount; inc++)
                {
                    Type type = t.GetType();
                    string name = reader.GetName(inc);
                    PropertyInfo prop = type.GetProperty(name);
                    if (prop != null)
                    {
                        var value = reader.GetValue(inc);
                        if (value != DBNull.Value)
                        {
                            Type tipo = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object safeValue = (value == null) ? null : Convert.ChangeType(value, tipo);
                            prop.SetValue(t, safeValue, null);
                        }
                    }
                }
                res.Add(t);
            }
            reader.Close();
            return res;
        }

    }
}
