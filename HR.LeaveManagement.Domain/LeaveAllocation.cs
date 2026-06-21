using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
   /// <summary>
   /// Number of days taken.
   /// </summary>
   public int NumberOfdays { get; set; }

   /// <summary>
   /// LeaveType.
   /// </summary>
   public LeaveType? LeaveType { get; set; }

   /// <summary>
   /// LeaveTypeId.
   /// </summary>
   public int LeaveTypeId { get; set; }

   /// <summary>
   /// Period of time.
   /// </summary>
   public int Period { get; set; }
}
