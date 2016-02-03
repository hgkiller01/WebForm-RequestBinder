using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Web;

namespace BinderLibrary
{
    public class BinderByRequest
    {
        public HttpRequest Request { get; set; }
        public NameValueCollection Collections { get; set; }
        public object BindedObject { get; set; }
        public enum RequestMethod
        {
            post,
            get,
        }
        public BinderByRequest(object InputObject, HttpRequest request, RequestMethod method = RequestMethod.get)
        {
            Request = request;
            BindedObject = InputObject;
            if (method == RequestMethod.post)
            {
                Collections = Request.Form;
            }
            else
            {
                Collections = Request.QueryString;
            }

        }
        public void ModelBinding()
        {
            var Properties = BindedObject.GetType().GetProperties();
            foreach (var key in Collections.Keys)
            {
                var item2 = Properties.Where(x => x.Name.ToLower() == key.ToString().ToLower()).FirstOrDefault();

                if (item2 != null && item2.CanRead && item2.CanWrite)
                {
                    if (item2.PropertyType == typeof(HttpPostedFile) || item2.PropertyType == typeof(HttpPostedFile[]))
                        continue;
                    var item3 = Collections.GetValues(key.ToString());
                    if (item3.Count() > 1)
                    {
                        if (item3 != null)
                        {
                            item2.SetValue(BindedObject, ChangeType(item2.PropertyType, item3));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item3.FirstOrDefault()))
                        {
                            item2.SetValue(BindedObject, ChangeType(item2.PropertyType, item3.FirstOrDefault()));
                        }
                    }
                }
            }
            var files = Request.Files;
            if (files.Count < 0) return;
            foreach (var key in files.Keys)
            {
                var item2 = Properties.Where(x => x.Name.ToLower() == key.ToString().ToLower()).FirstOrDefault();
                if (item2 != null && item2.CanRead && item2.CanWrite)
                {
                    var item3 = files.GetMultiple(key.ToString());
                    if (item3.Count() >= 1)
                    {
                        if (item2.PropertyType == typeof(HttpPostedFile))
                            item2.SetValue(BindedObject, item3.FirstOrDefault());
                    }
                    else
                    {
                        if (item2.PropertyType == typeof(HttpPostedFile[]))
                            item2.SetValue(BindedObject, item3.ToArray());
                    }
                }
            }
        }
        private object ChangeType(Type inputType, object value)
        {

            if (inputType == typeof(int) || inputType == typeof(int?))
            {
                return value.TryToInt(0);
            }
            else if (inputType == typeof(double) || inputType == typeof(double?))
            {
                return value.TryToDouble(0.0);
            }
            else if (inputType == typeof(DateTime) || inputType == typeof(DateTime?))
            {
                return value.TryToDateTime(new DateTime());
            }
            else if (inputType == typeof(float) || inputType == typeof(float?))
            {
                return value.TryToFloat(0.0f);
            }
            else if (inputType == typeof(Guid) || inputType == typeof(Guid?))
            {
                return value.TryToGuid(new Guid());
            }
            else if (inputType == typeof(bool) || inputType == typeof(bool?))
            {
                return value.TryToBool(true);
            }
            else
            {

                if (inputType == typeof(string[]))
                    return value;
                else
                    return null;

            }
        }
        private object ChangeType(Type inputType, string[] values)
        {
            values = values.Where(p => !string.IsNullOrEmpty(p)).ToArray();
            if (inputType == typeof(int[]))
            {
                return values.Select(x => x.TryToInt(0)).ToArray();
            }
            else if (inputType == typeof(double[]))
            {
                return values.Select(x => x.TryToDouble(0.0)).ToArray();
            }
            else if (inputType == typeof(DateTime[]))
            {
                return values.Select(x => x.TryToDateTime(new DateTime())).ToArray();
            }
            else if (inputType == typeof(float[]))
            {
                return values.Select(x => x.TryToFloat(0f));
            }
            else if (inputType == typeof(Guid[]))
            {
                return values.Select(x => x.TryToGuid(new Guid()));
            }
            else if (inputType == typeof(bool[]))
            {
                return values.Select(x => x.TryToBool(true));
            }
            else
            {

                if (inputType == typeof(string[]))
                    return values;
                else
                    return null;

            }
        }
    }
}
