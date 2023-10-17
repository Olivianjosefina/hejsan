using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Vinlista.Models
{
    public class VinMetod
    {
        public int InsertVin(Vin vin, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";
            {
                String sqlString = "INSERT INTO Vin (VinNamn, VinTyp, Argang, AlkoholHalt, Producent, Land, BildNamn, VinFarg, Pris) " +
                                   "VALUES (@VinNamn, @VinTyp, @Argang, @AlkoholHalt, @Producent, @Land, @BildNamn, @VinFarg, @Pris);";

                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

                dbCommand.Parameters.Add("VinNamn", SqlDbType.NVarChar, 255).Value = vin.VinNamn;
                dbCommand.Parameters.Add("VinTyp", SqlDbType.NVarChar, 255).Value = vin.VinTyp;
                dbCommand.Parameters.Add("Argang", SqlDbType.Int).Value = vin.Argang;
                dbCommand.Parameters.Add("AlkoholHalt", SqlDbType.Float).Value = vin.AlkoholHalt;
                dbCommand.Parameters.Add("Producent", SqlDbType.NVarChar, 255).Value = vin.Producent;
                dbCommand.Parameters.Add("Land", SqlDbType.NVarChar, 255).Value = vin.Land;
                dbCommand.Parameters.Add("BildNamn", SqlDbType.NVarChar, 255).Value = vin.BildNamn;
                dbCommand.Parameters.Add("VinFarg", SqlDbType.NVarChar, 50).Value = vin.VinFarg;
                dbCommand.Parameters.Add("Pris", SqlDbType.Int).Value = vin.Pris;

                try
                {
                    dbConnection.Open();
                    int i = 0;
                    i = dbCommand.ExecuteNonQuery();
                    if (i == 1) { errormsg = ""; }
                    else { errormsg = "Det skapas inte en användare i databasen."; }
                    return (i);
                }
                catch (Exception e)
                {
                    errormsg = e.Message;
                    return 0;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }

       

        public int DeleteVin(int vinID, out string errormsg)
        {
            string connectionString = "Data Source=localhost,1433;Database=OliviaDB;User Id=sa;Password=AAroh12345;";

            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                String sqlString = "DELETE FROM Vin WHERE VinID = @vinID;";

                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
                dbCommand.Parameters.Add("vinID", SqlDbType.Int).Value = vinID;

                try
                {
                    dbConnection.Open();
                    int i = dbCommand.ExecuteNonQuery();
                    if (i == 1)
                    {
                        errormsg = "";
                    }
                    else
                    {
                        errormsg = "Vinet togs inte bort.";
                    }
                    return i;
                }
                catch (Exception e)
                {
                    errormsg = e.Message;
                    return 0;
                }
            }
        }

        public int UpdateVin(int vinID, Vin vin, out string errormsg)
        {
            string connectionString = "Data Source=localhost,1433;Database=OliviaDB;User Id=sa;Password=AAroh12345;";

            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                String sqlString = "UPDATE Vin SET VinNamn = @VinNamn, VinTyp = @VinTyp, Argang = @Argang, AlkoholHalt = @AlkoholHalt, Producent = @Producent, Land = @Land, BildNamn = @BildNamn, VinFarg = @VinFarg, Pris = @Pris WHERE VinID = @vinID;";

                SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
                dbCommand.Parameters.Add("vinID", SqlDbType.Int).Value = vinID;
                dbCommand.Parameters.Add("VinNamn", SqlDbType.NVarChar, 255).Value = vin.VinNamn ?? (object)DBNull.Value;
                dbCommand.Parameters.Add("VinTyp", SqlDbType.NVarChar, 255).Value = vin.VinTyp ?? (object)DBNull.Value;
                dbCommand.Parameters.Add("Argang", SqlDbType.Int).Value = vin.Argang != null ? (object)vin.Argang : DBNull.Value;
                dbCommand.Parameters.Add("AlkoholHalt", SqlDbType.Float).Value = vin.AlkoholHalt != null ? (object)vin.AlkoholHalt : DBNull.Value;
                dbCommand.Parameters.Add("Producent", SqlDbType.NVarChar, 255).Value = vin.Producent ?? (object)DBNull.Value;
                dbCommand.Parameters.Add("Land", SqlDbType.NVarChar, 255).Value = vin.Land ?? (object)DBNull.Value;
                dbCommand.Parameters.Add("BildNamn", SqlDbType.NVarChar, 255).Value = vin.BildNamn ?? (object)DBNull.Value;
                dbCommand.Parameters.Add("VinFarg", SqlDbType.NVarChar, 255).Value = vin.VinFarg ?? (object)DBNull.Value;
                dbCommand.Parameters.Add("Pris", SqlDbType.Int).Value = vin.Pris != null ? (object)vin.Pris : DBNull.Value;

                try
                {
                    dbConnection.Open();
                    int i = dbCommand.ExecuteNonQuery();
                    if (i == 1)
                    {
                        errormsg = "";
                    }
                    else
                    {
                        errormsg = "Vinuppgifterna uppdaterades inte.";
                    }
                    return i;
                }
                catch (Exception e)
                {
                    errormsg = e.Message;
                    return 0;
                }
            }
        }

        public Vin GetVinDetails(int vinID, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database=OliviaDB;User Id=sa;Password=AAroh12345;";

            String sqlstring = "SELECT * FROM Vin WHERE VinID = @vinID;";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("vinID", SqlDbType.Int).Value = vinID;

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();
                if (reader.Read())
                {
                    Vin vin = new Vin
                    {
                        VinID = Convert.ToInt32(reader["VinID"]),
                        VinNamn = reader["VinNamn"].ToString(),
                        VinTyp = reader["VinTyp"].ToString(),
                        Argang = reader["Argang"] != DBNull.Value ? Convert.ToInt32(reader["Argang"]) : (int?)null,
                        AlkoholHalt = reader["AlkoholHalt"] != DBNull.Value ? Convert.ToSingle(reader["AlkoholHalt"]) : (float?)null,
                        Producent = reader["Producent"] != DBNull.Value ? reader["Producent"].ToString() : null,
                        Land = reader["Land"] != DBNull.Value ? reader["Land"].ToString() : null,
                        BildNamn = reader["BildNamn"] != DBNull.Value ? reader["BildNamn"].ToString() : null,
                        VinFarg = reader["VinFarg"] != DBNull.Value ? reader["VinFarg"].ToString() : null,
                        Pris = reader["Pris"] != DBNull.Value ? Convert.ToInt32(reader["Pris"]) : (int?)null
                    };

                    errormsg = "";
                    return vin;
                }
                else
                {
                    errormsg = "Vin hittades inte.";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }




        public List<Vin> GetVin(out string errormsg)
        {
            List<Vin> vinList = new List<Vin>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database=OliviaDB;User Id=sa;Password=AAroh12345;";

            String sqlstring = "SELECT * FROM Vin;";

            try
            {
                dbConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlstring, dbConnection);
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        Vin vin = new Vin
                        {
                            VinID = Convert.ToInt32(row["VinID"]),
                            VinNamn = row["VinNamn"].ToString(),
                            VinTyp = row["VinTyp"].ToString(),
                            Argang = row["Argang"] != DBNull.Value ? Convert.ToInt32(row["Argang"]) : (int?)null,
                            AlkoholHalt = row["AlkoholHalt"] != DBNull.Value ? Convert.ToSingle(row["AlkoholHalt"]) : (float?)null,
                            Producent = row["Producent"] != DBNull.Value ? row["Producent"].ToString() : null,
                            Land = row["Land"] != DBNull.Value ? row["Land"].ToString() : null,
                            BildNamn = row["BildNamn"] != DBNull.Value ? row["BildNamn"].ToString() : null,
                            VinFarg = row["VinFarg"] != DBNull.Value ? row["VinFarg"].ToString() : null,
                            Pris = row["Pris"] != DBNull.Value ? Convert.ToInt32(row["Pris"]) : (int?)null
                        };

                        vinList.Add(vin);
                    }
                }

                errormsg = "";
                return vinList;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }




    }
}
