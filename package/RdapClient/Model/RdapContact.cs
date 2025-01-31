namespace DarkPeakLabs.Rdap
{
    using DarkPeakLabs.Rdap.Values;
    using System.Collections.Generic;

    /// <summary>
    /// Class representing entity contact information
    /// </summary>
    public class RdapContact
    {
        public RdapContact()
        {
            Kind = RdapContactKind.Unknown;
        }

        /// <summary>
        /// The full name(s) of a contact (e.g. the personal name and surname of an individual, the name of an organization)
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The kind of the entity the Contact represents
        /// </summary>
        public RdapContactKind? Kind { get; set; }

        /// <summary>
        /// Components of entity name
        /// </summary>
        public IReadOnlyList<string> Names { get; set; }

        /// <summary>
        /// entity nicknames
        /// </summary>
        public IReadOnlyList<string> Nicknames { get; set; }

        /// <summary>
        /// entity address
        /// </summary>
        public IReadOnlyList<string> Address { get; set; }

        /// <summary>
        /// entity phone numbers
        /// </summary>
        public IReadOnlyList<RdapContactPhoneNumber> PhoneNumbers { get; set; }

        /// <summary>
        /// entity email address
        /// </summary>
        public IReadOnlyList<string> Emails { get; set; }
    }
}
