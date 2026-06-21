using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
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
