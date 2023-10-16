using System;
using System.Data;
using System.Data.SqlClient;
using VinApp.Models;

namespace VinApp.Models
{
	public class ListMetod
	{
        public ListMetod(){ }

            //public int InsertListDetaljer(ListDetaljer listDetaljer, out string errormsg)
            //{
            //    SqlConnection dbConnection = new SqlConnection();
            //    dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            //    String sqlstring = "INSERT INTO ListDetaljer (InkopslistaID, VinID, Antal) " +
            //                       "VALUES (@inkopslistaID, @vinID, @antal);";

            //    SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            //    dbCommand.Parameters.Add("inkopslistaID", SqlDbType.Int).Value = listDetaljer.InkopslistaID;
            //    dbCommand.Parameters.Add("vinID", SqlDbType.Int).Value = listDetaljer.VinID;
            //    dbCommand.Parameters.Add("antal", SqlDbType.Int).Value = listDetaljer.Antal;

            //    try
            //    {
            //        dbConnection.Open();
            //        int i = 0;
            //        i = dbCommand.ExecuteNonQuery();
            //        if (i == 1)
            //        {
            //            errormsg = "";
            //        }
            //        else
            //        {
            //            errormsg = "Det skapades inte en post i databasen.";
            //        }
            //        return i;
            //    }
            //    catch (Exception e)
            //    {
            //        errormsg = e.Message;
            //        return 0;
            //    }
            //    finally
            //    {
            //        dbConnection.Close();
            //    }
            //}


        //public List<ListDetaljer> GetListDetaljer (int inkopslistaID, int vinID, out string errormsg)
        //{
        //    List<ListDetaljer> listDetaljerList = new List<ListDetaljer>();

        //    SqlConnection dbConnection = new SqlConnection();
        //    dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

        //    String sqlstring = "SELECT * FROM ListDetaljer;";

        //    errormsg = "";

        //    try
        //    {
        //        dbConnection.Open();

        //        SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
        //        SqlDataReader reader = dbCommand.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            ListDetaljer listDetaljer = new ListDetaljer
        //            {
        //                InkopslistaID = Convert.ToInt32(reader["InkopslistaID"]),
        //                VinID = Convert.ToInt32(reader["VinID"]),
        //                Antal = Convert.ToInt32(reader["Antal"])
        //            };

        //            listDetaljerList.Add(listDetaljer);
        //        }

        //        return listDetaljerList;
        //    }
        //    catch (Exception e)
        //    {
        //        errormsg = e.Message;
        //        return null;
        //    }
        //    finally
        //    {
        //        dbConnection.Close();
        //    }
        //}


        //public ListDetaljer GetListaById(int id, out string errormsg)
        //{
        //    SqlConnection dbConnection = new SqlConnection();
        //    dbConnection.ConnectionString = "Data Source=localhost,1433;Database=OliviaDB;User Id=sa;Password=AAroh12345;";

        //    string sqlstring = "SELECT * FROM ListDetaljer WHERE InkopslistaID = @id;";

        //    SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
        //    dbCommand.Parameters.Add("id", SqlDbType.Int).Value = id;

        //    errormsg = "";

        //    try
        //    {
        //        dbConnection.Open();
        //        SqlDataReader reader = dbCommand.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            ListDetaljer listDetaljer = new ListDetaljer
        //            {
        //                InkopslistaID = Convert.ToInt32(reader["InkopslistaID"]),
        //                VinID = Convert.ToInt32(reader["VinID"]),
        //                Antal = Convert.ToInt32(reader["Antal"])
        //            };

        //            return listDetaljer;
        //        }

        //        errormsg = "Listan hittades inte.";
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        errormsg = e.Message;
        //        return null;
        //    }
        //    finally
        //    {
        //        dbConnection.Close();
        //    }
        //}

        //public int DeleteListDetaljer(int inkopslistaID, int vinID, out string errormsg)
        //{
        //    SqlConnection dbConnection = new SqlConnection();
        //    dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

        //    String sqlstring = "DELETE FROM ListDetaljer WHERE InkopslistaID = @inkopslistaID AND VinID = @vinID;";

        //    SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
        //    dbCommand.Parameters.Add("inkopslistaID", SqlDbType.Int).Value = inkopslistaID;
        //    dbCommand.Parameters.Add("vinID", SqlDbType.Int).Value = vinID;

        //    try
        //    {
        //        dbConnection.Open();
        //        int i = 0;
        //        i = dbCommand.ExecuteNonQuery();
        //        if (i == 1)
        //        {
        //            errormsg = "";
        //        }
        //        else
        //        {
        //            errormsg = "Posten togs inte bort från inköpslistan.";
        //        }
        //        return i;
        //    }
        //    catch (Exception e)
        //    {
        //        errormsg = e.Message;
        //        return 0;
        //    }
        //    finally
        //    {
        //        dbConnection.Close();
        //    }
        //}


        //public int UpdateListDetaljer(int inkopslistaID, int vinID, ListDetaljer listDetaljer, out string errormsg)
        //{
        //    SqlConnection dbConnection = new SqlConnection();
        //    dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

        //    String sqlstring = "UPDATE ListDetaljer SET Antal = @antal WHERE InkopslistaID = @inkopslistaID AND VinID = @vinID;";

        //    SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
        //    dbCommand.Parameters.Add("inkopslistaID", SqlDbType.Int).Value = inkopslistaID;
        //    dbCommand.Parameters.Add("vinID", SqlDbType.Int).Value = vinID;
        //    dbCommand.Parameters.Add("antal", SqlDbType.Int).Value = listDetaljer.Antal;

        //    try
        //    {
        //        dbConnection.Open();
        //        int i = 0;
        //        i = dbCommand.ExecuteNonQuery();
        //        if (i == 1)
        //        {
        //            errormsg = "";
        //        }
        //        else
        //        {
        //            errormsg = "Posten uppdaterades inte i inköpslistan.";
        //        }
        //        return i;
        //    }
        //    catch (Exception e)
        //    {
        //        errormsg = e.Message;
        //        return 0;
        //    }
        //    finally
        //    {
        //        dbConnection.Close();
        //    }
        //}

    }
}

