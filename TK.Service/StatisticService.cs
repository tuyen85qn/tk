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
    public interface IStatisticService
    {
        void Add(Statistic statistic);

        void Update(Statistic statistic);

        void Delete(int id);

        IEnumerable<Statistic> GetAll(string keyword);
        IEnumerable<Statistic> GetAll();

        Statistic GetById(int id);

        void SaveChanges();
    }

    public class StatisticService : IStatisticService
    {
        IStatisticRepository _statisticRepository;
        IUnitOfWork _unitOfWork;

        public StatisticService(IStatisticRepository statisticRepository, IUnitOfWork unitOfWork)
        {
            this._statisticRepository = statisticRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Statistic statistic)
        {
            _statisticRepository.Add(statistic);
        }

        public void Delete(int id)
        {
            _statisticRepository.Delete(id);
        }

        public IEnumerable<Statistic> GetAll()
        {
            return _statisticRepository.GetAll();
        }
        public IEnumerable<Statistic> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _statisticRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _statisticRepository.GetAll();
        }
        public Statistic GetById(int id)
        {
            return _statisticRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Statistic statistic)
        {
            _statisticRepository.Update(statistic);
        }

    }
}
