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
    public interface IDistrictService
    {
        District Add(District district);

        void Update(District district);

        District Delete(int id);

        IEnumerable<District> GetAll(string keyword);
        IEnumerable<District> GetAll();

        District GetById(int id);
        IEnumerable<District> GetByProvinceId(int provinceId);

        void Save();
    }

    public class DistrictService : IDistrictService
    {
        IDistrictRepository _districtRepository;
        IUnitOfWork _unitOfWork;

        public DistrictService(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
        {
            this._districtRepository = districtRepository;
            this._unitOfWork = unitOfWork;
        }

        public District Add(District district)
        {
            return _districtRepository.Add(district);
        }

        public District Delete(int id)
        {
            return _districtRepository.Delete(id);
        }

        public IEnumerable<District> GetAll()
        {
            return _districtRepository.GetAll();
        }
        public IEnumerable<District> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _districtRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _districtRepository.GetAll();
        }
        public District GetById(int id)
        {
            return _districtRepository.GetSingleById(id);
        }
        public IEnumerable<District> GetByProvinceId(int provinceId)
        {
            return _districtRepository.GetMulti(x => x.ProvinceID == provinceId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(District district)
        {
            _districtRepository.Update(district);
        }

    }
   
}
