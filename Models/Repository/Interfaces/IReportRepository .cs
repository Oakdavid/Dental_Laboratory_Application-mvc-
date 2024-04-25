using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IReportRepository
    {
        Report AddReport(Report report);
        Report Get(Expression<Func<Report, bool>> predicate);
        ICollection<Report> GetAll(Expression<Func<Report, bool>> predicate);
        bool Update(Func<Report, bool> predicate,Report newValues);
    }
}
