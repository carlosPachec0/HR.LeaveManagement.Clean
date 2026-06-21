using HR.LeaveManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
   /// <summary>
   /// StartDate.
   /// </summary>
   public DateTime StartDate { get; set; }

   /// <summary>
   /// EndDate.
   /// </summary>
   public DateTime EndDate { get; set; }

   /// <summary>
   /// LeaveType.
   /// </summary>
   [ForeignKey("LeaveTypeId")]
   public LeaveType? LeaveType { get; set; }

   /// <summary>
   /// LeaveTypeId.
   /// </summary>
   public int LeaveTypeId { get; set; }

   /// <summary>
   /// Date requested.
   /// </summary>
   public DateTime DateRequested { get; set; }

   /// <summary>
   /// Request comments.
   /// </summary>
   public string? RequestComments { get; set; }

   /// <summary>
   /// Flag for approved.
   /// </summary>
   public bool? Approved { get; set; }

   /// <summary>
   /// Flag for canceled.
   /// </summary>
   public bool Cancelled { get; set; }

   /// <summary>
   /// RequestingEmployeeId.
   /// </summary>
   public string RequestingEmployeeId { get; set; } = string.Empty;
}
