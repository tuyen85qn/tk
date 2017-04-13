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
    public interface IPoliceOrganizationService
    {
        PoliceOrganization Add(PoliceOrganization policeOrganization);

        void Update(PoliceOrganization policeOrganization);

        PoliceOrganization Delete(int id);

        IEnumerable<PoliceOrganization> GetAll(string keyword);
        IEnumerable<PoliceOrganization> GetAll();

        PoliceOrganization GetById(int id);
        IEnumerable<PoliceOrganization> GetListByType(string type);

        void Save();
    }

    public class PoliceOrganizationService : IPoliceOrganizationService
    {
        IPoliceOrganizationRepository _policeOrganizationRepository;
        IUnitOfWork _unitOfWork;

        public PoliceOrganizationService(IPoliceOrganizationRepository policeOrganizationRepository, IUnitOfWork unitOfWork)
        {
            this._policeOrganizationRepository = policeOrganizationRepository;
            this._unitOfWork = unitOfWork;
        }

        public PoliceOrganization Add(PoliceOrganization policeOrganization)
        {
            return _policeOrganizationRepository.Add(policeOrganization);
        }

        public PoliceOrganization Delete(int id)
        {
            return _policeOrganizationRepository.Delete(id);
        }

        public IEnumerable<PoliceOrganization> GetAll()
        {
            return _policeOrganizationRepository.GetAll();
        }
        public IEnumerable<PoliceOrganization> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _policeOrganizationRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _policeOrganizationRepository.GetAll();
        }
        public PoliceOrganization GetById(int id)
        {
            return _policeOrganizationRepository.GetSingleById(id);
        }

        public IEnumerable<PoliceOrganization> GetListByType(string type)
        {
            if (!string.IsNullOrEmpty(type))
                return _policeOrganizationRepository.GetMulti(x => type.Contains(x.Type));
            else
                return null;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PoliceOrganization policeOrganization)
        {
            _policeOrganizationRepository.Update(policeOrganization);
        }

    }
 
}
