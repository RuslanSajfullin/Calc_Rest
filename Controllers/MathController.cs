namespace WebApplication3.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    using WebApplication3.Context;
    using WebApplication3.Entity;

    public class MathController : ApiController
    {

        [Route("api/math/{operation}/{arg1}/{arg2}")]
        public decimal? Get(string operation, decimal arg1, decimal arg2)
        {
            decimal? rv = null;

            string operationMesage = string.Empty;

            try
            {
                switch (operation)
                {
                    case "add":
                        rv = arg1 + arg2;
                        break;
                    case "div":
                        rv = arg1 / arg2;
                        break;
                    case "mul":
                        rv = arg1 * arg2;
                        break;
                    case "sub":
                        rv = arg1 - arg2;
                        break;
                    default:
                        operationMesage = $"Операция \"{operation}\" не поддеерживается";
                        break;
                }
            }
            catch (Exception ex)
            {
                operationMesage = ex.Message;
                throw;
            }
            var ip = GetClientIp(Request);

            log(operation, arg1, arg2, rv, ip, operationMesage);

            Debug.WriteLine(operationMesage);
            if (rv.HasValue)
            {
                return rv;
            }
            else
            {

                throw new HttpRequestValidationException(operationMesage);
            }
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            //else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            //{
            //    RemoteEndpointMessageProperty prop;
            //    prop = (RemoteEndpointMessageProperty) this.Request.Properties[RemoteEndpointMessageProperty.Name];
            //    return prop.Address;
            //}
            else
            {
                return null;
            }
        }

        private void log(string operation, decimal arg1, decimal arg2, decimal? result, string ip, string operationMesage)
        {
            using (LogContext db = new LogContext())
            {

                LogItem logItem = new LogItem { NameOperation = operation, Arg1 = arg1, Arg2 = arg2, Result = result, IpClient = ip, OperationMesage = operationMesage };

                // добавляем их в бд
                db.Log.Add(logItem);
                db.SaveChanges();
            }

        }
    }
}
