using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveType : BaseEntity
{
   /// <summary>
   /// Name property.
   /// </summary>
   public string Name { get; set; } = string.Empty;

   /// <summary>
   /// Name property.
   /// </summary>
   public int DefaultDays { get; set; }
}
