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
    public interface ISituationService
    {
        Situation Add(Situation situation);

        void Update(Situation situation);

        Situation Delete(int id);       

        IEnumerable<Situation> GetAll(string keyword);
        IEnumerable<Situation> GetAll();    
      
        Situation GetById(int id);     

        void Save();        
    }

    public class SituationService : ISituationService
    {
        ISituationRepository _situationRepository;
        IUnitOfWork _unitOfWork;

        public SituationService(ISituationRepository situationRepository, IUnitOfWork unitOfWork)
        {
            this._situationRepository = situationRepository;
            this._unitOfWork = unitOfWork;
        }

        public Situation Add(Situation situation)
        {
            return _situationRepository.Add(situation);
        }

        public Situation Delete(int id)
        {
            return _situationRepository.Delete(id);
        }

        public IEnumerable<Situation> GetAll()
        {
            return _situationRepository.GetAll();
        }
        public IEnumerable<Situation> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _situationRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _situationRepository.GetAll();
        }
        public Situation GetById(int id)
        {
            return _situationRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Situation situation)
        {
            _situationRepository.Update(situation);
        }
        
    }
    
}
