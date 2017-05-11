using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityTimetable.Common.Validators
{
    public class RegisterViewModelCustomBinder : DefaultModelBinder
    {

        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);
            IDataErrorInfo errorProvider = bindingContext.Model as IDataErrorInfo;
            if (errorProvider != null)
            {
                string errorText = errorProvider.Error;
                if (!String.IsNullOrEmpty(errorText))
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, errorText);
                }
            }


            foreach (ModelValidator validator in bindingContext.ModelMetadata.GetValidators(controllerContext))
            {
                foreach (ModelValidationResult validationResult in validator.Validate(null))
                {
                    bindingContext.ModelState.AddModelError(CreateSubPropertyName(bindingContext.ModelName, validationResult.MemberName), validationResult.Message);
                }
            }


        }

    }
}