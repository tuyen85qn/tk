using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Data.Infrastructure;
using TK.Data.Repositories;
using TK.Model.Models;

namespace TK.Service
{
    public interface IDailySheetService
    {
        DailySheet Add(DailySheet dailySheet);

        void Update(DailySheet dailySheet);

        DailySheet Delete(int id);

       
        IEnumerable<DailySheet> GetAll();
        IEnumerable<DailySheet> GetAll(String filterDate);

        DailySheet GetById(int id);

        IEnumerable<DailySheet> GetListDoNotDailySheet(string fromDate, string toDate, int policeOrganizationID);
        void Save();
    }

    public class DailySheetService : IDailySheetService
    {
        IDailySheetRepository _dailySheetRepository;
        IUnitOfWork _unitOfWork;

        public DailySheetService(IDailySheetRepository dailySheetRepository, IUnitOfWork unitOfWork)
        {
            this._dailySheetRepository = dailySheetRepository;
            this._unitOfWork = unitOfWork;
        }

        public DailySheet Add(DailySheet dailySheet)
        {
            return _dailySheetRepository.Add(dailySheet);
        }

        public DailySheet Delete(int id)
        {
            return _dailySheetRepository.Delete(id);
        }

        public IEnumerable<DailySheet> GetAll()
        {
            return _dailySheetRepository.GetAll();
        }
        public IEnumerable<DailySheet> GetAll(String filterDate)
        {
            if(!String.IsNullOrEmpty(filterDate))
            {
                DateTime ftDate = DateTime.ParseExact(filterDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return _dailySheetRepository.GetMulti(x => DbFunctions.TruncateTime(x.DayReport) == DbFunctions.TruncateTime(ftDate));
            }
            else
            {
                return _dailySheetRepository.GetAll();
            }
            
        }

        public DailySheet GetById(int id)
        {
            return _dailySheetRepository.GetSingleById(id);
        }

        public IEnumerable<DailySheet> GetListDoNotDailySheet(string fromDate, string toDate, int policeOrganizationID)
        {
            DateTime fDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime tDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1);
            return _dailySheetRepository.GetMulti(x => DbFunctions.TruncateTime(x.DayReport)> DbFunctions.TruncateTime(fDate) &&
            DbFunctions.TruncateTime(x.DayReport) < DbFunctions.TruncateTime(tDate) 
            && x.PoliceOrganizationID == policeOrganizationID
            && x.Status == false);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(DailySheet dailySheet)
        {
            _dailySheetRepository.Update(dailySheet);
        }

    }
    
}
