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
    public interface IStatisticCategoryService
    {
        void Add(StatisticCategory statisticCategory);

        void Update(StatisticCategory statisticCategory);

        void Delete(int id);

        IEnumerable<StatisticCategory> GetAll(string keyword);
        IEnumerable<StatisticCategory> GetAll();

        StatisticCategory GetById(int id);

        void SaveChanges();
    }

    public class StatisticCategoryService : IStatisticCategoryService
    {
        IStatisticCategoryRepository _statisticCategoryRepository;
        IUnitOfWork _unitOfWork;

        public StatisticCategoryService(IStatisticCategoryRepository statisticCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._statisticCategoryRepository = statisticCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(StatisticCategory statisticCategory)
        {
            _statisticCategoryRepository.Add(statisticCategory);
        }

        public void Delete(int id)
        {
            _statisticCategoryRepository.Delete(id);
        }

        public IEnumerable<StatisticCategory> GetAll()
        {
            return _statisticCategoryRepository.GetAll();
        }
        public IEnumerable<StatisticCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _statisticCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _statisticCategoryRepository.GetAll();
        }
        public StatisticCategory GetById(int id)
        {
            return _statisticCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(StatisticCategory statisticCategory)
        {
            _statisticCategoryRepository.Update(statisticCategory);
        }

    }
    
}
