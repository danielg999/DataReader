namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Class to read data.
    /// </summary>
    public class DataReader
    {

        /// <summary>
        /// Imported objects.
        /// </summary>
        private IEnumerable<ImportedObject> ImportedObjects;

        /// <summary>
        /// Method to import and print data.
        /// </summary>
        /// <param name="fileToImport"></param>
        /// <param name="printData"></param>
        public void ImportAndPrintData(string fileToImport, bool printData = true)
        {
            ImportData(fileToImport);

            if (printData)
            {
                PrintDatabases();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Method to import data.
        /// </summary>
        /// <param name="fileToImport"></param>
        private void ImportData(string fileToImport)
        {
            var importedLines = ReadDataFromFile(fileToImport);

            InsertDataToObject(importedLines);
        }

        /// <summary>
        /// Reading data from file.
        /// </summary>
        /// <param name="fileToImport"></param>
        /// <returns></returns>
        private List<string> ReadDataFromFile(string fileToImport)
        {
            ImportedObjects = new List<ImportedObject>();

            var importedLines = new List<string>();

            using (StreamReader streamReader = new StreamReader(fileToImport))
            {
                string headerLine = streamReader.ReadLine();

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    importedLines.Add(line);
                }
            }

            return importedLines;
        }

        /// <summary>
        /// Inserting data to object.
        /// </summary>
        /// <param name="importedLines"></param>
        private void InsertDataToObject(List<string> importedLines)
        {
            for (int i = 0; i < importedLines.Count; i++)
            {
                var importedLine = importedLines[i];

                if (string.IsNullOrWhiteSpace(importedLine))
                {
                    continue;
                }

                var values = importedLine.Split(';');
                var importedObject = new ImportedObject(values);
                ((List<ImportedObject>)ImportedObjects).Add(importedObject);
            }

            ClearAndCorrectImportedData();
            AssignNumberOfChildren();
        }

        /// <summary>
        /// Clearing and correcting imported data.
        /// </summary>
        private void ClearAndCorrectImportedData()
        {
            foreach (var importedObject in ImportedObjects)
            {
                importedObject.Type = importedObject.Type.FormatPropertyImportedObject().ToUpper();
                importedObject.Name = importedObject.Name.FormatPropertyImportedObject();
                importedObject.Schema = importedObject.Schema.FormatPropertyImportedObject();
                importedObject.ParentName = importedObject.ParentName.FormatPropertyImportedObject();
                importedObject.ParentType = importedObject.ParentType.FormatPropertyImportedObject();
            }
        }

        /// <summary>
        /// Assigning number of children.
        /// </summary>
        private void AssignNumberOfChildren()
        {
            for (int i = 0; i < ImportedObjects.Count(); i++)
            {
                var importedObject = ImportedObjects.ToArray()[i];
                foreach (var impObj in ImportedObjects)
                {
                    if (impObj.ParentType == importedObject.Type && impObj.ParentName == importedObject.Name)
                    {
                        importedObject.NumberOfChildren = 1 + importedObject.NumberOfChildren;
                    }
                }
            }
        }

        /// <summary>
        /// Printing all databases.
        /// </summary>
        private void PrintDatabases()
        {
            foreach (var database in ImportedObjects)
            {
                if (database.Type == "DATABASE")
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    PrintTables(database);
                }
            }
        }

        /// <summary>
        /// Printing all database's tables.
        /// </summary>
        /// <param name="database"></param>
        private void PrintTables(ImportedObject database)
        {
            foreach (var table in ImportedObjects)
            {
                if (table.ParentType.ToUpper() == database.Type && table.ParentName == database.Name)
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                    PrintColumns(table);
                }
            }
        }

        /// <summary>
        /// Printing all table's columns.
        /// </summary>
        /// <param name="table"></param>
        private void PrintColumns(ImportedObject table)
        {
            foreach (var column in ImportedObjects)
            {
                if (column.ParentType.ToUpper() == table.Type && column.ParentName == table.Name)
                {
                    Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {((bool)column.IsNullable ? "accepts nulls" : "with no nulls")}");
                }
            }
        }
    }
}
