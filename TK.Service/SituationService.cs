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
        void Add(Situation situation);

        void Update(Situation situation);

        void Delete(int id);       

        IEnumerable<Situation> GetAll(string keyword);
        IEnumerable<Situation> GetAll();    
      
        Situation GetById(int id);     

        void SaveChanges();
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

        public void Add(Situation situation)
        {
            _situationRepository.Add(situation);
        }

        public void Delete(int id)
        {
            _situationRepository.Delete(id);
        }

        public IEnumerable<Situation> GetAll()
        {
            return _situationRepository.GetAll();
        }
        public IEnumerable<Situation> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _situationRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _situationRepository.GetAll();
        }
        public Situation GetById(int id)
        {
            return _situationRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Situation situation)
        {
            _situationRepository.Update(situation);
        }
        
    }
    
}
