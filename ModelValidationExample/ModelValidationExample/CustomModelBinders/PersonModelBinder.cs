using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationExample.Models;

namespace ModelValidationExample.CustomModelBinders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();

            //FirstName and LastName
            if (bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            {
                person.PersonName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
            }

            if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
            {
                person.PersonName += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
            }

            //email
            if (bindingContext.ValueProvider.GetValue("Email").Length > 0)
            {
                person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            }

            //phone
            if (bindingContext.ValueProvider.GetValue("Phone").Length > 0)
            {
                person.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            }

            //password
            if (bindingContext.ValueProvider.GetValue("Password").Length > 0)
            {
                person.Password = bindingContext.ValueProvider.GetValue("Password").FirstValue;
            }

            //confirm password
            if (bindingContext.ValueProvider.GetValue("ConfirmPassword").Length > 0)
            {
                person.ConfirmPassword = bindingContext.ValueProvider.GetValue("ConfirmPassword").FirstValue;
            }

            //price
            if (bindingContext.ValueProvider.GetValue("Price").Length > 0)
            {
                person.Price = Convert.ToDouble(bindingContext.ValueProvider.GetValue("Price").FirstValue);
            }

            //dateofbirth
            if (bindingContext.ValueProvider.GetValue("DateOfBirth").Length > 0)
            {
                person.DateOfBirth = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("DateOfBirth").FirstValue);
            }

            bindingContext.Result = ModelBindingResult.Success(person);

            return Task.CompletedTask;
        }
    } 
}
