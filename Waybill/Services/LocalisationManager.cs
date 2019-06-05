using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using WpfApp2.CustomDialogs;

namespace WpfApp2.Services
{
    class LocalisationManager : Manager
    {
        /// <summary>
        /// Gets zip code of localisation
        /// </summary>
        /// <param name="street"> street address of desired localisation</param>
        /// <param name="city"> city of desired localisation</param>
        /// <returns></returns>
        public static string GetLocalisationZipCode(string street, string city) // returns zip code of localisation
        {
            string zipCode = "";
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText =
                            "SELECT ZipCode FROM Localisations" +
                            " WHERE (Street=@Street AND City=@City)";
                        var sqlStreetParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = street,
                            ParameterName = "@Street"
                        };

                        var sqlCityParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = city,
                            ParameterName = "@City"
                        };
                        sqlCommand.Parameters.Add(sqlStreetParam);
                        sqlCommand.Parameters.Add(sqlCityParam);
                        sqlCommand.Connection = connection;
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                zipCode = (string) reader[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return zipCode;
        }

        /// <summary>
        /// Checks if same localisation already exists in database 
        /// </summary>
        /// <param name="localisationModel"> model of localisation</param>
        /// <returns></returns>
        private static Boolean CheckIfAlreadyExists(LocalisationModel localisationModel) 
        {
            bool exists = false;
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText =
                            "SELECT COUNT(*) FROM Localisations" +
                            " WHERE Street=@Street AND City=@City AND ZipCode=@ZipCode";
                        var sqlStreetParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = localisationModel.Street,
                            ParameterName = "@Street"
                        };

                        var sqlCityParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = localisationModel.City,
                            ParameterName = "@City"
                        };

                        var sqlZipCodeParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = localisationModel.ZipCode,
                            ParameterName = "@ZipCode"
                        };
                        sqlCommand.Parameters.Add(sqlStreetParam);
                        sqlCommand.Parameters.Add(sqlCityParam);
                        sqlCommand.Parameters.Add(sqlZipCodeParam);
                        sqlCommand.Connection = connection;
                        if ((int) sqlCommand.ExecuteScalar() > 0)
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return exists;
        }

        /// <summary>
        /// Takes source file and checks if there are some localisation which arent in database, if yes asks user for zip code and city then adds it to table "Localisations" in database
        /// </summary>
        /// <param name="filePath"> path to the file with localisations </param>
        public static void UpdateLocalisations(String filePath)
        {
            List<String> uniqueStreetsList = ExcelFile.getStreetsFromFile(filePath);
            LocalisationModel localisationModel;
            foreach (var street in uniqueStreetsList)
            {
                AddLocalisationDialogFromUpdate addLocalisationDialog = new AddLocalisationDialogFromUpdate(street);
                if (addLocalisationDialog.ShowDialog() == false && addLocalisationDialog.IsAddClicked == true) // getting info of city and zipcode from dialog and assigning it with already get street to LocalisationModel
                {
                    localisationModel = new LocalisationModel()
                    {
                        City = addLocalisationDialog.City,
                        Street = street,
                        ZipCode = addLocalisationDialog.ZipCode
                    };
                    AddLocalisation(localisationModel);
                }
            }
        }

        /// <summary>
        /// Adds localisation to database
        /// </summary>
        /// <param name="localisationModel"> model of localisation to be added </param>
        public static void AddLocalisation(LocalisationModel localisationModel) // adds localisation to database, used by UpdateLocalisations method
        {
            if (!CheckIfAlreadyExists(localisationModel))
            {
                try
                {
                    using (var connection = Open())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.CommandText = "INSERT Localisations (Street ,City, ZipCode )" +
                                                     " VALUES (@Street, @City, @ZipCode)";
                            var sqlStreetParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.AnsiString,
                                Value = localisationModel.Street,
                                ParameterName = "@Street"
                            };

                            var sqlCityParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.AnsiString,
                                Value = localisationModel.City,
                                ParameterName = "@City"
                            };

                            var sqlZipCodeParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.AnsiString,
                                Value = localisationModel.ZipCode,
                                ParameterName = "@ZipCode"
                            };
                            sqlCommand.Parameters.Add(sqlStreetParam);
                            sqlCommand.Parameters.Add(sqlCityParam);
                            sqlCommand.Parameters.Add(sqlZipCodeParam);
                            sqlCommand.Connection = connection;
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                MessageBox.Show("Lokalizacja w " + localisationModel.City + " " + localisationModel.ZipCode + " na ulicy " + localisationModel.Street + " istnieje już w bazie danych");
            }
        }

        /// <summary>
        /// Deletes localisation from database
        /// </summary>
        /// <param name="id">id of localisation to be deleted </param>
        public static void DeleteLocalisation(int id)
        {
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "Delete FROM Localisations" +
                                                 " WHERE LocalisationID = (@LocalisationID)";
                        var sqlLocalisationIDParameter = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = id,
                            ParameterName = "@LocalisationID"
                        };
                        sqlCommand.Parameters.Add(sqlLocalisationIDParameter);
                        sqlCommand.Connection = connection;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Edits localisation in database
        /// </summary>
        /// <param name="localisationModel"> model of lococalisation to be edited </param>
        public static void EditLocalisation(LocalisationModel localisationModel)
        {
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "UPDATE Localisations" +
                                                 " SET Street = @Street, City = @City, ZipCode = @ZipCode" +
                                                 "  WHERE LocalisationID = @LocalisationID";
                        var sqlLocalisationIDParameter = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = localisationModel.LocalisationID,
                            ParameterName = "@LocalisationID"
                        };
                        var sqlStreetParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = localisationModel.Street,
                            ParameterName = "@Street"
                        };

                        var sqlCityParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = localisationModel.City,
                            ParameterName = "@City"
                        };

                        var sqlZipCodeParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = localisationModel.ZipCode,
                            ParameterName = "@ZipCode"
                        };
                        sqlCommand.Parameters.Add(sqlStreetParam);
                        sqlCommand.Parameters.Add(sqlCityParam);
                        sqlCommand.Parameters.Add(sqlZipCodeParam);
                        sqlCommand.Parameters.Add(sqlLocalisationIDParameter);
                        sqlCommand.Connection = connection; 
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}