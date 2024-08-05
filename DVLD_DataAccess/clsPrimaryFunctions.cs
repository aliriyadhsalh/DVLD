using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccess
{
    internal class clsPrimaryFunctions
    {

        public static void EntireInfoToEventLoge(string Information)
        {
            string SourceName ="DVLD";
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }

            EventLog.WriteEntry(SourceName, Information, EventLogEntryType.Error);

        }
        public static int Add(SqlCommand command)
        {
            int ID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (command.Connection = connection)
                {

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ID = insertedID;
                        }

                    }

                    catch (Exception ex)
                    {
                    EntireInfoToEventLoge(ex.Message);

                        ID = -1;
                    }



                    return ID;
                }
            }

        }
        public static DataTable Get(SqlCommand command)
        {
            DataTable GetData = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (command.Connection = connection)
                {

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)

                            {
                                GetData.Load(reader);

                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        EntireInfoToEventLoge(ex.Message);

                    }
                }
            }


            return GetData;
        
        }
        public static bool Update(SqlCommand command)
        {
            int RowsAffected = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (command.Connection = connection)
                {

                    try
                    {
                        connection.Open();

                        RowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        RowsAffected = -1;
                        EntireInfoToEventLoge(ex.Message);

                    }
                }

                return (RowsAffected > 0);
            }
        }
        public static bool Delete(SqlCommand command)
        {
            int RowsAffected = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (command.Connection = connection)
                {

                    try
                    {
                        connection.Open();

                        RowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        EntireInfoToEventLoge(ex.Message);
                        RowsAffected = -1;
                    }

                    return (RowsAffected > 0);
                }
            }
        }
        public static bool Exist(SqlCommand command)
        {

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (command.Connection = connection)
                {


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            isFound = reader.HasRows;

                        }
                    }
                    catch (Exception ex)
                    {
                        EntireInfoToEventLoge( ex.Message);
                        isFound = false;
                    }
                }}
                return isFound;
            }
    
    }


}


    

