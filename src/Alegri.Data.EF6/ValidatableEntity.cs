using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Alegri.Data.EF6
{

    public abstract class ValidatableEntity : TrackedEntity
    {
        /// <summary>
        /// Validates and returns true if no error found
        /// </summary>
        public bool IsValid()
        {
            IEnumerable<ValidationResult> errors;
            return IsValid(out errors);
        }


        /// <summary>
        /// Return true if validation was successfully. <see cref="errors"/> is empty if valid.
        /// </summary>
        public bool IsValid(out IEnumerable<ValidationResult> errors)
        {
            errors = Validate();
            return !errors.Any();
        }


        /// <summary>
        /// Executes the validations with a new, empty validation context
        /// </summary>
        /// <returns>Empty list if no errors found</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            try
            {
                var errors = Validate(new ValidationContext(this, null, null));
                return errors;
            }
            catch(Exception e)
            {
                // TODO: Handle here
                return new List<ValidationResult>();
            }
        }

        /// <summary>
        /// Executes the validations with the given validation context
        /// </summary>
        /// <returns>Empty list if no errors found</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, results, true);
            return results;
        }
    }
}