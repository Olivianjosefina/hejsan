using System.Data;
using VinApp.Models;
using System.Data.SqlClient;
using System.Linq;

namespace VinApp.Models
{
    public class AnvandarMetod
    {
        public AnvandarMetod() { }

        public int InsertAnvandare(AnvandarDetalj ad, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "INSERT INTO Anvandare (AnvdandarNamn, Epost, Losenord, Alder) " +
                               "VALUES (@anvandarnamn, @epost, @losenord, @alder);";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("anvandarnamn", SqlDbType.NVarChar, 255).Value = ad.AnvandarNamn;
            dbCommand.Parameters.Add("epost", SqlDbType.NVarChar, 255).Value = ad.Epost;
            dbCommand.Parameters.Add("losenord", SqlDbType.NVarChar, 255).Value = ad.Losenord;
            dbCommand.Parameters.Add("alder", SqlDbType.Int).Value = ad.Alder;

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

        public List<AnvandarDetalj> GetAnvandare(out string errormsg)
        {
            List<AnvandarDetalj> anvandareList = new List<AnvandarDetalj>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "SELECT * FROM Anvandare;";

            try
            {                dbConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlstring, dbConnection);
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        AnvandarDetalj anvandare = new AnvandarDetalj
                        {
                            AnvandarID = Convert.ToInt32(row["AnvandarID"]),
                            AnvandarNamn = row["AnvdandarNamn"].ToString(),
                            Epost = row["Epost"].ToString(),
                            Losenord = row["Losenord"].ToString(),
                            Alder = Convert.ToInt32(row["Alder"])
                        };

                        anvandareList.Add(anvandare);
                    }
                }

                errormsg = "";
                return anvandareList;
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



        public int DeleteAnvandare(string anvdandarNamn, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "DELETE FROM Anvandare WHERE AnvdandarNamn = @anvdandarNamn;";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("anvdandarNamn", SqlDbType.NVarChar, 255).Value = anvdandarNamn;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "Användaren togs inte bort.";
                }
                return i;
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




        public int UpdateAnvandare(string gamlaAnvandarNamn, AnvandarDetalj ad, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "UPDATE Anvandare SET AnvdandarNamn = @nyttAnvandarNamn, Epost = @epost, Losenord = @losenord, Alder = @alder WHERE AnvdandarNamn = @gamlaAnvandarNamn;";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("nyttAnvandarNamn", SqlDbType.NVarChar, 255).Value = ad.AnvandarNamn;
            dbCommand.Parameters.Add("epost", SqlDbType.NVarChar, 255).Value = ad.Epost;
            dbCommand.Parameters.Add("losenord", SqlDbType.NVarChar, 255).Value = ad.Losenord;
            dbCommand.Parameters.Add("alder", SqlDbType.Int).Value = ad.Alder;
            dbCommand.Parameters.Add("gamlaAnvandarNamn", SqlDbType.NVarChar, 255).Value = gamlaAnvandarNamn;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "Användaren uppdaterades inte.";
                }
                return i;
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




        public AnvandarDetalj GetAnvandareById(int id)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "SELECT * FROM Anvandare WHERE AnvdandarNamn = @id;";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = id;

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();

                if (reader.Read())
                {
                    AnvandarDetalj anvandare = new AnvandarDetalj
                    {
                        AnvandarID = Convert.ToInt32(reader["AnvandarID"]),
                        AnvandarNamn = reader["AnvdandarNamn"].ToString(),
                        Epost = reader["Epost"].ToString(),
                        Losenord = reader["Losenord"].ToString(),
                        Alder = Convert.ToInt32(reader["Alder"])
                    };

                    return anvandare;
                }

                return null; // Returnera null om användaren inte hittades
            }
            catch (Exception e)
            {
             
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public List<AnvandarDetalj> FilterAnvandare(string filterText, out string errormsg)
        {
            List<AnvandarDetalj> filteredUsers = new List<AnvandarDetalj>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "SELECT * FROM Anvandare WHERE AnvdandarNamn LIKE @filterText;";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("filterText", SqlDbType.NVarChar, 255).Value = "%" + filterText + "%";

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    AnvandarDetalj anvandare = new AnvandarDetalj
                    {
                        AnvandarID = Convert.ToInt32(reader["AnvandarID"]),
                        AnvandarNamn = reader["AnvdandarNamn"].ToString(),
                        Epost = reader["Epost"].ToString(),
                        Losenord = reader["Losenord"].ToString(),
                        Alder = Convert.ToInt32(reader["Alder"])
                    };

                    filteredUsers.Add(anvandare);
                }

                errormsg = "";
                return filteredUsers;
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

        public List<AnvandarDetalj> SortAnvandare(string column, string direction, out string errormsg)
        {
            List<AnvandarDetalj> sortedUsers = new List<AnvandarDetalj>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            string sqlstring = $"SELECT * FROM Anvandare ORDER BY {column} {(direction == "asc" ? "ASC" : "DESC")};";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    AnvandarDetalj anvandare = new AnvandarDetalj
                    {
                        AnvandarID = Convert.ToInt32(reader["AnvandarID"]),
                        AnvandarNamn = reader["AnvdandarNamn"].ToString(),
                        Epost = reader["Epost"].ToString(),
                        Losenord = reader["Losenord"].ToString(),
                        Alder = Convert.ToInt32(reader["Alder"])
                    };

                    sortedUsers.Add(anvandare);
                }

                errormsg = "";
                return sortedUsers;
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

        public List<AnvandarDetalj> FilterAnvandareByAge(int filterAge, out string errormsg)
        {
            List<AnvandarDetalj> filteredUsers = new List<AnvandarDetalj>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "SELECT * FROM Anvandare WHERE Alder > @filterAge;";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("filterAge", SqlDbType.Int).Value = filterAge;

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    AnvandarDetalj anvandare = new AnvandarDetalj
                    {
                        AnvandarID = Convert.ToInt32(reader["AnvandarID"]),
                        AnvandarNamn = reader["AnvdandarNamn"].ToString(),
                        Epost = reader["Epost"].ToString(),
                        Losenord = reader["Losenord"].ToString(),
                        Alder = Convert.ToInt32(reader["Alder"])
                    };

                    filteredUsers.Add(anvandare);
                }

                errormsg = "";
                return filteredUsers;
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

        public List<AnvandarDetalj> SokFunktion(string sokText, out string errormsg)
        {
            List<AnvandarDetalj> sokFunktion = new List<AnvandarDetalj>();

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source=localhost,1433;Database = OliviaDB; User Id = sa; Password = AAroh12345; ";

            String sqlstring = "SELECT * FROM Anvandare WHERE AnvdandarNamn LIKE @sokText;";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("sokText", SqlDbType.NVarChar, 255).Value = "%" + sokText + "%";

            try
            {
                dbConnection.Open();
                SqlDataReader reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    AnvandarDetalj anvandare = new AnvandarDetalj
                    {
                        AnvandarID = Convert.ToInt32(reader["AnvandarID"]),
                        AnvandarNamn = reader["AnvdandarNamn"].ToString(),
                        Epost = reader["Epost"].ToString(),
                        Losenord = reader["Losenord"].ToString(),
                        Alder = Convert.ToInt32(reader["Alder"])
                    };

                    sokFunktion.Add(anvandare);
                }

                errormsg = "";
                return sokFunktion;
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

