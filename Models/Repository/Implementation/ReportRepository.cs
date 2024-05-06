using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class ReportRepository : IReportRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public ReportRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Report CreateReport(Report report)
        {
            _dbContext.Reports.Add(report);
            return report;
        }

        public Report Get(Expression<Func<Report, bool>> predicate)
        {
            var report = _dbContext.Set<Report>()
                .Include(r => r.Appointment)
                .ThenInclude(r => r.Patient)
                .SingleOrDefault(predicate);
            return report;
        }

        public ICollection<Report> GetAll(Expression<Func<Report, bool>> predicate)
        {
            var report = _dbContext.Set<Report>()
                .Include(r => r.Appointment)
                .ThenInclude(r => r.Patient)
                .Where(predicate).ToList();
            return report;
        }

        public bool Update(Func<Report, bool> predicate, Report newValues)
        {
            var reportToUpdate = _dbContext.Reports.Where(predicate);
            foreach (var report in reportToUpdate)
            {
                report.Appointment = newValues.Appointment;
                report.ReportContent = newValues.ReportContent;
                report.PatientComplain = newValues.PatientComplain;
            }
            return reportToUpdate.Any();
        }
    }
}
