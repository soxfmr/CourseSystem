using CourseServer.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Views
{
    public class UserView : View<UserEntity>
    {
        public override string Show(List<UserEntity> recordSet)
        {
            if (Empty(recordSet)) return Error();

            UserEntity entity = recordSet[0];

            JArray jArray = new JArray();
            JObject obj = new JObject();
            obj.Add(new JProperty("Email", entity.Email));
            obj.Add(new JProperty("Avatar", entity.Avatar));
            obj.Add(new JProperty("Name", entity.Name));
            obj.Add(new JProperty("Cellphone", entity.Cellphone));
            jArray.Add(obj);

            return Success(jArray);
        }
    }
}
