using System.Web.Mvc;

namespace Web.ModelBinders
{
    /// <summary>
    /// Base class for model binders.
    /// </summary>
    public abstract class ModelBinderBase : IModelBinder
    {
        /// <summary>
        /// A model binding helper.
        /// </summary>
        protected ModelBindingHelper ModelBindingHelper { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ModelBindingHelper"/>.
        /// </summary>
        protected ModelBinderBase(ModelBindingHelper modelBindingHelper)
        {
            ModelBindingHelper = modelBindingHelper;
        }

        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        public abstract object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
    }
}