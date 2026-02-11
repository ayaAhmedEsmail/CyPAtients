using CyPatients.DTO;
using CyPatients.Models;
using CyPatients.Service.interfaces;
using Microsoft.EntityFrameworkCore;

namespace CyPatients.Service
{
    public class Validation : IValidation
    {
        public readonly CyhealthCare_dbContext _dbContext;

        public Validation(CyhealthCare_dbContext context)
        {
            _dbContext = context;
        }

        public async Task<PatientValidationFlag> getValidatVisit(int visitTypeID, int entityID)
        {
            var rules = await _dbContext.PatientValidationFlags
                .FirstOrDefaultAsync(r => r.VisitTypeId == visitTypeID && r.MedicalEntityId == entityID);

            if (rules == null) throw new Exception("No Validation for this");

            return rules;
        }
        public List<string> Validat(object _patient, PatientValidationFlag _rules)
        {
            var errors = new List<string>();

            // to get prop 
            var rulesPro = _rules.GetType().GetProperties();
            var patientPro = _patient.GetType().GetProperties();


            foreach (var rule in rulesPro)
            {
                // to skip Visit type Id and Entity ID
                if (rule.PropertyType != typeof(bool)) continue;

                bool required = (bool)rule.GetValue(_rules);

                // skip not required
                if (!required) continue;

                // compare between flag prop and dto prop with the same name
                var patientProp = patientPro.FirstOrDefault(p => p.Name == rule.Name);
                if (patientProp == null) continue; // skip if no match

                var value = patientProp.GetValue(_patient); // get dto value

                if (value == null || string.IsNullOrWhiteSpace(value.ToString())) // compare value with validation 
                {
                    errors.Add(rule.Name + " is required");
                }
            }
            return errors;
        }

    }
}
