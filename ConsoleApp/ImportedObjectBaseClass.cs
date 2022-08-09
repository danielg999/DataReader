namespace ConsoleApp
{

    /// <summary>
    /// Base class of imported object.
    /// </summary>
    class ImportedObjectBaseClass
    {

        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Constructor to base class of imported object.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public ImportedObjectBaseClass(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
