using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManageMent.Persistence.DatabaseContext;
using HR.LeaveManageMent.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManageMent.Persistence;

public static class PersistenceServiceRegistration
{
   public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<HrDatabaseContext>(options =>
      {
         options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
      });

      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
      services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
      services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

      return services;
   }
}
