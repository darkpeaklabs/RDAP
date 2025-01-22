// Generated file from IANA registry (2/20/2022 10:53:44 AM)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values.Json
{
	public enum RdapNoticeAndRemarkType
	{
		/// <summary>
		/// Unknown value
		/// </summary>
		[Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
		Unknown = -1,

		/// <summary>
		/// The list of results does not contain all results due to lack of authorization. This may indicate to some clients that proper authorization will yield a longer result set.
		/// </summary>
		[Display(Name = "Result Set Truncated Due To Authorization", Description = "The list of results does not contain all results due to lack of authorization. This may indicate to some clients that proper authorization will yield a longer result set.")]
		ResultSetTruncatedDueToAuthorization,

		/// <summary>
		/// The list of results does not contain all results due to an excessively heavy load on the server. This may indicate to some clients that requerying at a later time will yield a longer result set.
		/// </summary>
		[Display(Name = "Result Set Truncated Due To Excessive Load", Description = "The list of results does not contain all results due to an excessively heavy load on the server. This may indicate to some clients that requerying at a later time will yield a longer result set.")]
		ResultSetTruncatedDueToExcessiveLoad,

		/// <summary>
		/// The list of results does not contain all results for an unexplainable reason. This may indicate to some clients that requerying for any reason will not yield a longer result set.
		/// </summary>
		[Display(Name = "Result Set Truncated Due To Unexplainable Reasons", Description = "The list of results does not contain all results for an unexplainable reason. This may indicate to some clients that requerying for any reason will not yield a longer result set.")]
		ResultSetTruncatedDueToUnexplainableReasons,

		/// <summary>
		/// The object does not contain all data due to lack of authorization.
		/// </summary>
		[Display(Name = "Object Truncated Due To Authorization", Description = "The object does not contain all data due to lack of authorization.")]
		ObjectTruncatedDueToAuthorization,

		/// <summary>
		/// The object does not contain all data due to an excessively heavy load on the server. This may indicate to some clients that requerying at a later time will yield all data of the object.
		/// </summary>
		[Display(Name = "Object Truncated Due To Excessive Load", Description = "The object does not contain all data due to an excessively heavy load on the server. This may indicate to some clients that requerying at a later time will yield all data of the object.")]
		ObjectTruncatedDueToExcessiveLoad,

		/// <summary>
		/// The object does not contain all data for an unexplainable reason.
		/// </summary>
		[Display(Name = "Object Truncated Due To Unexplainable Reasons", Description = "The object does not contain all data for an unexplainable reason.")]
		ObjectTruncatedDueToUnexplainableReasons,

		/// <summary>
		/// The object contains redacted data due to lack of authorization.
		/// </summary>
		[Display(Name = "Object Redacted Due To Authorization", Description = "The object contains redacted data due to lack of authorization.")]
		ObjectRedactedDueToAuthorization,
	}
}
