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
        Statistic Add(Statistic statistic);

        void Update(Statistic statistic);

        Statistic Delete(int id);

        IEnumerable<Statistic> GetAll(string keyword);
        IEnumerable<Statistic> GetAll();

        Statistic GetById(int id);

        void Save();
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

        public Statistic Add(Statistic statistic)
        {
            return _statisticRepository.Add(statistic);
        }

        public Statistic Delete(int id)
        {
            return _statisticRepository.Delete(id);
        }

        public IEnumerable<Statistic> GetAll()
        {
            return _statisticRepository.GetAll();
        }
        public IEnumerable<Statistic> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _statisticRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _statisticRepository.GetAll();
        }
        public Statistic GetById(int id)
        {
            return _statisticRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Statistic statistic)
        {
            _statisticRepository.Update(statistic);
        }

    }
}
