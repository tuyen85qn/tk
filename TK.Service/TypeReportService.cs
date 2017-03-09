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
    public interface ITypeReportService
    {
        TypeReport Add(TypeReport typeReport);

        void Update(TypeReport typeReport);

        TypeReport Delete(int id);

        IEnumerable<TypeReport> GetAll(string keyword);
        IEnumerable<TypeReport> GetAll();

        TypeReport GetById(int id);
   
        void Save();
    }

    public class TypeReportService : ITypeReportService
    {
        ITypeReportRepository _typeReportRepository;
        IUnitOfWork _unitOfWork;

        public TypeReportService(ITypeReportRepository typeReportRepository, IUnitOfWork unitOfWork)
        {
            this._typeReportRepository = typeReportRepository;
            this._unitOfWork = unitOfWork;
        }

        public TypeReport Add(TypeReport typeReport)
        {
            return _typeReportRepository.Add(typeReport);
        }

        public TypeReport Delete(int id)
        {
            return _typeReportRepository.Delete(id);
        }

        public IEnumerable<TypeReport> GetAll()
        {
            return _typeReportRepository.GetAll();
        }
        public IEnumerable<TypeReport> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _typeReportRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _typeReportRepository.GetAll();
        }
        public TypeReport GetById(int id)
        {
            return _typeReportRepository.GetSingleById(id);
        }
     
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(TypeReport typeReport)
        {
            _typeReportRepository.Update(typeReport);
        }

    }
   
}
