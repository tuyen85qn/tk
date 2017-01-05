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
    public interface IWardService
    {
        Ward Add(Ward ward);

        void Update(Ward ward);

        Ward Delete(int id);

        IEnumerable<Ward> GetAll(string keyword);
        IEnumerable<Ward> GetAll();

        Ward GetById(int id);
        IEnumerable<Ward> GetByDistrictId(int districtId);
        IEnumerable<Ward> GetByProvinceId(int provinceId);

        void Save();
    }

    public class WardService : IWardService
    {
        IWardRepository _wardRepository;
        IUnitOfWork _unitOfWork;

        public WardService(IWardRepository wardRepository, IUnitOfWork unitOfWork)
        {
            this._wardRepository = wardRepository;
            this._unitOfWork = unitOfWork;
        }

        public Ward Add(Ward ward)
        {
            return _wardRepository.Add(ward);
        }

        public Ward Delete(int id)
        {
            return _wardRepository.Delete(id);
        }

        public IEnumerable<Ward> GetAll()
        {
            return _wardRepository.GetAll();
        }
        public IEnumerable<Ward> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _wardRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _wardRepository.GetAll();
        }
        public Ward GetById(int id)
        {
            return _wardRepository.GetSingleById(id);
        }
        public IEnumerable<Ward> GetByDistrictId(int districtId)
        {
            return _wardRepository.GetMulti(x => x.DistrictID == districtId);
        }
        public IEnumerable<Ward> GetByProvinceId(int provinceId)
        {
            return _wardRepository.GetMulti(x => x.ProvinceID == provinceId);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Ward ward)
        {
            _wardRepository.Update(ward);
        }

    }
  
}
