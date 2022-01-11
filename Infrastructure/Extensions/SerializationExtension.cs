using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class SerializationExtension
    {
        public static bool Deserialize(this string json, out ToDoDto dto)
        {
            if (json == null)
            {
                dto = new ToDoDto();

                return false;
            }
            else
            {
                try
                {
                    dto = JsonConvert.DeserializeObject<ToDoDto>(json);

                    return true;
                }
                catch
                {
                    dto = new ToDoDto();

                    return false;
                }
            }
        }
    }
}
