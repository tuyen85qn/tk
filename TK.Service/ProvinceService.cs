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
    public interface IProvinceService
    {
        void Add(Province province);

        void Update(Province province);

        Province Delete(int id);

        IEnumerable<Province> GetAll(string keyword);
        IEnumerable<Province> GetAll();

        Province GetById(int id);

        void Save();
    }

    public class ProvinceService : IProvinceService
    {
        IProvinceRepository _provinceRepository;
        IUnitOfWork _unitOfWork;

        public ProvinceService(IProvinceRepository provinceRepository, IUnitOfWork unitOfWork)
        {
            this._provinceRepository = provinceRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Province provinceRepository)
        {
            _provinceRepository.Add(provinceRepository);
        }

        public Province Delete(int id)
        {
            return _provinceRepository.Delete(id);
        }

        public IEnumerable<Province> GetAll()
        {
            return _provinceRepository.GetAll();
        }
        public IEnumerable<Province> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _provinceRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _provinceRepository.GetAll();
        }
        public Province GetById(int id)
        {
            return _provinceRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Province provinceRepository)
        {
            _provinceRepository.Update(provinceRepository);
        }

    }
    
}
