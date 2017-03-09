using System;
using System.Collections.Generic;
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
        
        DailySheet GetById(int id);

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
               
        public DailySheet GetById(int id)
        {
            return _dailySheetRepository.GetSingleById(id);
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
