using AdminService.Exceptions;
using AdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Helpers
{
    public class CriteriaBuilder : ICriteriaBuilder
    {
        private Criteria _criteria;

        public CriteriaBuilder()
        {
            _criteria = new Criteria();
        }

        public void Build(string criteria, string criteriaValue)
        {
            switch (criteria)
            {
                case "associateid":
                    this.AssociateIdCriteria(criteriaValue);
                    break;
                case "associatename":
                    this.AssociateNameCriteria(criteriaValue);
                    break;
                case "skillname":
                    this.SkillNameCriteria(criteriaValue);
                    break;
                default:
                    throw new CriteriaNotFoundException("Criteria Not Found");
            }
        }
        
        public Criteria getCriteria()
        {
            return this._criteria;
        }

        private void AssociateIdCriteria(string criteriaValue)
        {
            _criteria.CriteriaType = CriteriaType.AssociateId;
            _criteria.CriteraValue = criteriaValue;
        }

        private void AssociateNameCriteria(string criteriaValue)
        {
            _criteria.CriteriaType = CriteriaType.AssociateName;
            _criteria.CriteraValue = criteriaValue;
        }

        private void SkillNameCriteria(string criteriaValue)
        {
            _criteria.CriteriaType = CriteriaType.SkillName;
            _criteria.CriteraValue = criteriaValue;
        }
    }
}
