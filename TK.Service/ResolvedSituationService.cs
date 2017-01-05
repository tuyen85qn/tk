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
    public interface IResolvedSituationService
    {
        ResolvedSituation Add(ResolvedSituation resolvedSituation);

        void Update(ResolvedSituation resolvedSituation);

        ResolvedSituation Delete(int id);

        IEnumerable<ResolvedSituation> GetAll(string keyword);
        IEnumerable<ResolvedSituation> GetAll();

        ResolvedSituation GetById(int id);     

        void Save();
    }

    public class ResolvedSituationService : IResolvedSituationService
    {
        IResolvedSituationRepository _resolvedSituationRepository;
        IUnitOfWork _unitOfWork;

        public ResolvedSituationService(IResolvedSituationRepository resolvedSituationRepository, IUnitOfWork unitOfWork)
        {
            this._resolvedSituationRepository = resolvedSituationRepository;
            this._unitOfWork = unitOfWork;
        }

        public ResolvedSituation Add(ResolvedSituation resolvedSituation)
        {
            return _resolvedSituationRepository.Add(resolvedSituation);
        }

        public ResolvedSituation Delete(int id)
        {
            return _resolvedSituationRepository.Delete(id);
        }

        public IEnumerable<ResolvedSituation> GetAll()
        {
            return _resolvedSituationRepository.GetAll();
        }
        public IEnumerable<ResolvedSituation> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _resolvedSituationRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _resolvedSituationRepository.GetAll();
        }
        public ResolvedSituation GetById(int id)
        {
            return _resolvedSituationRepository.GetSingleById(id);
        }
      
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ResolvedSituation resolvedSituation)
        {
            _resolvedSituationRepository.Update(resolvedSituation);
        }

    }
}
