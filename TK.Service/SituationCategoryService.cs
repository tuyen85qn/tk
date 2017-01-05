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
    public interface ISituationCategoryService
    {
        SituationCategory Add(SituationCategory situationCategory);

        void Update(SituationCategory situationCategory);

        SituationCategory Delete(int id);

        IEnumerable<SituationCategory> GetAll(string keyword);
        IEnumerable<SituationCategory> GetAll();

        SituationCategory GetById(int id);

        void Save();
    }

    public class SituationCategoryService : ISituationCategoryService
    {
        ISituationCategoryRepository _situationCategoryRepository;
        IUnitOfWork _unitOfWork;

        public SituationCategoryService(ISituationCategoryRepository situationCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._situationCategoryRepository = situationCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public SituationCategory Add(SituationCategory situationCategory)
        {
            return _situationCategoryRepository.Add(situationCategory);
        }

        public SituationCategory Delete(int id)
        {
            return _situationCategoryRepository.Delete(id);
        }

        public IEnumerable<SituationCategory> GetAll()
        {
            return _situationCategoryRepository.GetAll();
        }
        public IEnumerable<SituationCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _situationCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _situationCategoryRepository.GetAll();
        }
        public SituationCategory GetById(int id)
        {
            return _situationCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(SituationCategory situationCategory)
        {
            _situationCategoryRepository.Update(situationCategory);
        }

    }
   
}
