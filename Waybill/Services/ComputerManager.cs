
using System;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp2.Services
{
    class ComputerManager : Manager
    {

        /// <summary>
        /// Gets weight of computer model
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static string GetWeight(string modelName) // returns zip code of localisation
        {
            int weight = 5; // default weight
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText =
                            "SELECT Weight FROM Computers" +
                            " WHERE (ModelName=@ModelName)";
                        sqlCommand.Connection = connection;
                        var sqlModelNameParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = modelName,
                            ParameterName = "@ModelName"
                        };
                        sqlCommand.Parameters.Add(sqlModelNameParam);
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                weight = (int) reader[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return weight.ToString();
        }

        /// <summary>
        /// Gets price of desired computer model
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static string GetPrice(string modelName) // returns zip code of localisation
        {
            int price = 0;
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText =
                            "SELECT Price FROM Computers" +
                            " WHERE ModelName=@ModelName";
                        var sqlModelNameParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = modelName,
                            ParameterName = "@ModelName"
                        };
                        sqlCommand.Parameters.Add(sqlModelNameParam);
                        sqlCommand.Connection = connection;
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                price = (int)reader[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return price.ToString();
        }

        /// <summary>
        /// Checks if computer model already exists in database
        /// </summary>
        /// <param name="computerModel"></param>
        /// <returns></returns>
        private static Boolean CheckIfAlreadyExists(ComputerModel computerModel)
        { 
            bool exists = false;
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText =
                            "SELECT COUNT(*) FROM Computers" +
                            " WHERE ModelName=@ModelName AND Price=@Price AND Weight=@Weight AND HasAdapter=@HasAdapter";
                        var sqlModelNameParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = computerModel.ModelName,
                            ParameterName = "@ModelName"
                        };

                        var sqlPriceParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Double,
                            Value = computerModel.Price,
                            ParameterName = "@Price"
                        };

                        var sqlWeightParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = computerModel.Weight,
                            ParameterName = "@Weight"
                        };

                        var sqlHasAdapterParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Boolean,
                            Value = computerModel.HasAdapter,
                            ParameterName = "@HasAdapter"
                        };
                        sqlCommand.Parameters.Add(sqlModelNameParam);
                        sqlCommand.Parameters.Add(sqlPriceParam);
                        sqlCommand.Parameters.Add(sqlWeightParam);
                        sqlCommand.Parameters.Add(sqlHasAdapterParam);
                        sqlCommand.Connection = connection;
                        if ((int) sqlCommand.ExecuteScalar() > 0)
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            } 
            return exists;
        }

        /// <summary>
        /// Adds computer model to database
        /// </summary>
        /// <param name="computerModel"></param>
        public static void AddComputer(ComputerModel computerModel) // adds new Computer model to database
        {
            if (!CheckIfAlreadyExists(computerModel))
            {
                try
                {
                    using (var connection = Open())
                    {
                        using (SqlCommand sqlCommand = new SqlCommand())
                        {
                            sqlCommand.CommandText = "INSERT Computers (ModelName, Price, Weight, HasAdapter )" +
                                                     " VALUES (@ModelName, @Price, @Weight, @HasAdapter)";
                            var sqlModelNameParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.AnsiString,
                                Value = computerModel.ModelName,
                                ParameterName = "@ModelName"
                            };

                            var sqlPriceParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.Double,
                                Value = computerModel.Price,
                                ParameterName = "@Price"
                            };

                            var sqlWeightParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.Int32,
                                Value = computerModel.Weight,
                                ParameterName = "@Weight"
                            };

                            var sqlHasAdapterParam = new SqlParameter()
                            {
                                DbType = System.Data.DbType.Boolean,
                                Value = computerModel.HasAdapter,
                                ParameterName = "@HasAdapter"
                            };
                            sqlCommand.Parameters.Add(sqlModelNameParam);
                            sqlCommand.Parameters.Add(sqlPriceParam);
                            sqlCommand.Parameters.Add(sqlWeightParam);
                            sqlCommand.Parameters.Add(sqlHasAdapterParam);
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
                MessageBox.Show("Model komputera " + computerModel.ModelName + " o cenie " + computerModel.Price + " i wadze " + computerModel.Weight + " z adapterem: " + computerModel.HasAdapter + " istnieje już w bazie danych");
            }
        }

        /// <summary>
        /// Edits computer model in database
        /// </summary>
        /// <param name="computerModel"></param>
        public static void EditComputer(ComputerModel computerModel) // edits already existing Computer model in database
        {
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "UPDATE Computers" +
                                                 " SET ModelName = @ModelName, Price = @Price, Weight = @Weight, HasAdapter = @HasAdapter" +
                                                 "  WHERE ComputerID = @ComputerID";
                        var sqlComputerIDParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = computerModel.ComputerID,
                            ParameterName = "@ComputerID"
                        };
                        var sqlModelNameParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = computerModel.ModelName,
                            ParameterName = "@ModelName"
                        };

                        var sqlPriceParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Double,
                            Value = computerModel.Price,
                            ParameterName = "@Price"
                        };
                        var sqlWeightParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = computerModel.Weight,
                            ParameterName = "@Weight"
                        };

                        var sqlHasAdapterParam = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Boolean,
                            Value = computerModel.HasAdapter,
                            ParameterName = "@HasAdapter"
                        };
                        sqlCommand.Parameters.Add(sqlComputerIDParam);
                        sqlCommand.Parameters.Add(sqlModelNameParam);
                        sqlCommand.Parameters.Add(sqlPriceParam);
                        sqlCommand.Parameters.Add(sqlWeightParam);
                        sqlCommand.Parameters.Add(sqlHasAdapterParam);                   
                        sqlCommand.Connection = connection;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    
        /// <summary>
        /// Deletes computer model from database
        /// </summary>
        /// <param name="computerID"> </param>
        public static void DeleteComputer(int computerID)
        {
            try
            {
                using (var connection = Open())
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandText = "Delete FROM Computers" +
                                                 " WHERE ComputerID = (@ComputerID)";
                        var sqlLocalisationIDParameter = new SqlParameter()
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = computerID,
                            ParameterName = "@ComputerID"
                        };
                        sqlCommand.Parameters.Add(sqlLocalisationIDParameter);
                        sqlCommand.Connection = connection;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
