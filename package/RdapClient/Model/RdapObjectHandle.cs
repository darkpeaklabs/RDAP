namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Represents object handle
    /// </summary>
    public class RdapObjectHandle
    {
        /// <summary>
        /// Full string handle value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Object tag which is the portion of the value after the last '-' character. It can be null
        /// </summary>
        public string Tag { get; set; }
    }
}