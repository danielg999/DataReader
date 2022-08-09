namespace ConsoleApp
{

    /// <summary>
    /// Class to imported object.
    /// </summary>
    class ImportedObject : ImportedObjectBaseClass
    {

        /// <summary>
        /// The schema.
        /// </summary>
        public string Schema;

        /// <summary>
        /// The parent name.
        /// </summary>
        public string ParentName;

        /// <summary>
        /// The parent type.
        /// </summary>
        public string ParentType { get; set; }

        /// <summary>
        /// The data type.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Is nullable.
        /// </summary>
        public bool? IsNullable { get; set; }

        /// <summary>
        /// Number od children.
        /// </summary>
        public double NumberOfChildren;

        /// <summary>
        /// Constructor to imported data.
        /// </summary>
        /// <param name="values"></param>
        public ImportedObject(string[] values) : base(values[0], values[1])
        {
            Schema = values[2];
            ParentName = values[3];
            ParentType = values[4];
            DataType = values[5];

            if (values.Length > 6 && values[6] == "1")
            {
                IsNullable = true;
            }
            else if (values.Length > 6 && values[6] == "0")
            {
                IsNullable = false;
            }
        }
    }
}
