using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Linq;

namespace AllReady.ModelBinding
{
    public class AdjustToTimezoneModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsComplexType && context.Metadata.MetadataKind == ModelMetadataKind.Property)
            {
                var propName = context.Metadata.PropertyName;

                if (propName == null)
                {
                    return null;
                }
                
                var propInfo = context.Metadata.ContainerType.GetProperty(propName);

                if (propInfo == null)
                {
                    return null;
                }

                if (propInfo.GetCustomAttributes(typeof(AdjustToTimezoneAttribute), false).FirstOrDefault() is AdjustToTimezoneAttribute attribute)
                {
                    return new AdjustToTimeZoneModelBinder(context.Metadata.ModelType, attribute.TimeZoneIdPropertyName);
                }
            }

            return null;
        }
    }
}
