namespace DarkPeakLabs.Rdap.Values
{
    /// <summary>
    /// Phone number types
    /// <see cref="https://tools.ietf.org/html/rfc6350#section-6.4.1">RFC 6350</see>
    /// </summary>
    public enum RdapPhoneNumberType
    {
        Unknown = -1,

        /// <summary>
        /// Indicates that the telephone number supports text messages (SMS)
        /// </summary>
        Text,

        /// <summary>
        /// Indicates a voice telephone number
        /// </summary>
        Voice,

        /// <summary>
        /// Indicates a facsimile telephone number
        /// </summary>
        Fax,

        /// <summary>
        /// Indicates a cellular or mobile telephone number
        /// </summary>
        Cell,

        /// <summary>
        /// Indicates a video conferencing telephone number
        /// </summary>
        Video,

        /// <summary>
        /// Indicates a paging device telephone number
        /// </summary>
        Pager,

        /// <summary>
        /// Indicates a telecommunication device for people with hearing or speech difficulties
        /// </summary>
        TextPhone,

        /// <summary>
        /// a primary or main contact number, typically of an enterprise, as opposed to a direct-dial number of an individual
        /// <see cref="https://tools.ietf.org/html/rfc7852">RFC 7852</see>
        /// </summary>
        MainNumber
    }
}