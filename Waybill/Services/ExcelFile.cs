using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using WpfApp2.Services;

namespace WpfApp2
{
    public class ExcelFile
    {
        private readonly String _firstFilePath;
        private readonly String _secondFilePath;
        private readonly String _savingDirectory;
        private readonly List<int> listOfWrongFilledRows = new List<int>(); 
        private int[] _Rows;
        private readonly List<ShipmentModel> Shipments = new List<ShipmentModel>();


        public ExcelFile(String firstFilePath, String secondFilePath, String savingDirectory, int[] Rows)
        {
            _firstFilePath = firstFilePath;
            _secondFilePath = secondFilePath;
            _savingDirectory = savingDirectory;
            _Rows = Rows;
            ReadFile(_firstFilePath);
            SaveToFile(_secondFilePath);
        }

        /// <summary>
        /// Checks if user gave rows in right format
        /// </summary>
        /// <param name="stringRows"></param>
        private static void CheckRows(String stringRows)
        {
            String[] rows = stringRows.Split('-');
            if (rows[0] == String.Empty || rows[1] == String.Empty)
                throw new WrongNumberOfRowsException("Źle napisany zakres wierszy");
        }

        /// <summary>
        /// Checks if data given has right length
        /// </summary>
        /// <param name="dataToBeChecked"></param>
        private void CheckData(String[] dataToBeChecked, int row)
        {
            foreach (var data in dataToBeChecked)
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    listOfWrongFilledRows.Add(row);
                    break;
                }
            }
        }

        /// <summary>
        /// converts rows range from string format to int
        /// </summary>
        /// <param name="stringRows"></param>
        /// <returns></returns>
        public static int[] ReadRows(String stringRows)
        {
            CheckRows(stringRows);
            String[] rows = stringRows.Split('-');
            int[] intRows = new int[2];
            intRows[0] = Convert.ToInt32(rows[0]);
            intRows[1] = Convert.ToInt32(rows[1]);
            if (intRows[1] - intRows[0] < 0) throw new WrongNumberOfRowsException("Źle napisany zakres wierszy");
            return intRows;
        }

        /// <summary>
        /// Gets list of unique often used localisations from source file and returns it
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<String> getStreetsFromFile(String filePath)
        {
            List<String>uniqueLocalisationsList = new List<String>();
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets[4]; // 4th worksheet contains updated often used streets of localisations
                int rowIndex = 2; // because first index is header
                while (!String.IsNullOrWhiteSpace( ws.GetValue<String>(rowIndex, 7) ))
                {
                    uniqueLocalisationsList.Add (ws.GetValue<String>(rowIndex, 7) );
                    rowIndex++;
                }
            }
            return uniqueLocalisationsList;
        }

        /// <summary>
        /// Reads source file 
        /// </summary>
        /// <param name="path"> path of source file </param>
        private void ReadFile(String path)
        {
            var fileInfo = new FileInfo(path);
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet ws = excelPackage.Workbook.Worksheets[1];
                    for (var i = _Rows[0]; i <= _Rows[1]; i++)
                    {
               
                        
                            String[] splittedData = // assigns data to array which will be checked
                            {
                                ws.GetValue<string>(i, 17), // company name
                                ws.GetValue<string>(i,13) + " " + ws.GetValue<string>(i,12), // user name
                                ws.GetValue<string>(i,15), // telephone number
                                ws.GetValue<string>(i,19), // street address
                                LocalisationManager.GetLocalisationZipCode(ws.GetValue<string>(i,19),ws.GetValue<string>(i,18)), // zip code
                                ws.GetValue<string>(i,18), // city
                                ComputerManager.GetWeight(ws.GetValue<string>(i,11)), // weight
                                ws.GetValue<string>(i,11), // model name
                                ComputerManager.GetPrice(ws.GetValue<string>(i,11)), // price
                                ws.GetValue<string>(i,8) // serial number
                            };  
                            CheckData(splittedData, i);
                            Shipments.Add(new ShipmentModel()
                            {
                                CompanyName = splittedData[0],
                                UserName = splittedData[1],
                                TelephoneNumber = splittedData[2],
                                StreetAddress = splittedData[3],
                                ZipCode = splittedData[4],
                                City = splittedData[5],
                                Weight = int.Parse(splittedData[6]),
                                ModelName = splittedData[7],
                                Price = int.Parse(splittedData[8]),
                                SerialNumber = splittedData[9]
                            }
                            );
                        }
                    excelPackage.Dispose();
                }
        }

        /// <summary>
        /// saves the read data to the destination file
        /// </summary>
        /// <param name="path"> path of destination file </param>
        private void SaveToFile(String path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets[1];
                int index = 4; // first three rows are header rows
                foreach (ShipmentModel shipment in Shipments)
                {
                    ws.SetValue(index, 1, shipment.CompanyName);
                    ws.SetValue(index, 2, shipment.UserName);
                    ws.SetValue(index, 3, shipment.TelephoneNumber);
                    ws.SetValue(index, 4, shipment.StreetAddress);
                    ws.SetValue(index, 5, shipment.ZipCode);
                    ws.SetValue(index, 6, shipment.City);
                    ws.SetValue(index, 7, "1");
                    ws.SetValue(index, 8, shipment.Weight);
                    ws.SetValue(index, 9, shipment.ModelName);
                    ws.SetValue(index,10,shipment.Price);
                    ws.SetValue(index, 11, shipment.SerialNumber);
                    ws.SetValue(index, 14, "Produkcja komputerów");
                    if(listOfWrongFilledRows.Contains(index + _Rows[0] - 4) == true) // Sets color of wrong filled rows to avoid mistake. Subtracts by 4 because index variable is set to 4 at start
                    {
                        ws.Row(index).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Row(index).Style.Fill.BackgroundColor.SetColor(Color.MediumVioletRed);
                    }
                    index++;
                }

                excelPackage.SaveAs(new FileInfo(_savingDirectory + "\\" + Shipments[0].UserName + ".xlsx"));
                DeleteShipments();
            }
        }
        private void DeleteShipments()
        {
            Shipments.Clear();
        }
    }
}