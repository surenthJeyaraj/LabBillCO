using System;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Domain.Model;
using Web.Models;

namespace Web.ModelBinders
{
    [ModelBinderType(typeof(SearchCriteria))]
    public class SearchCriteriaViewModelBinder : ModelBinderBase
    {
        public SearchCriteriaViewModelBinder(ModelBindingHelper modelBindingHelper)
            : base(modelBindingHelper)
        {

        }
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");


            SearchCriteria userContextSelection = null;
            //ModelBindingHelper.TryGetValueFromBindingContext(bindingContext, "transactionSearchCriteria",
            //    out userContextSelectionViewModel);
          var valueResult = bindingContext.ValueProvider.GetValue("transactionSearchCriteria");
           userContextSelection= Newtonsoft.Json.JsonConvert.DeserializeObject<SearchCriteria>(valueResult.AttemptedValue.ToString());
            
           
            
            return userContextSelection;
        }
    }
}