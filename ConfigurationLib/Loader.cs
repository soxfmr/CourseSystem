using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigurationLib
{
    public class Loader
    {
        protected object[] ParseNode(XmlNode node, string[] fields, object[] defaults, Type[] types)
        {
            XmlNode child;

            string value;

            object[] Ret = new object[fields.Length];

            for (int i = 0, len = fields.Length; i < len; i++)
            {
                // Set the default value if the node dosen't exists
                child = node.SelectSingleNode(fields[i]);
                if (child == null)
                {
                    Ret[i] = defaults[i];
                    continue;
                }

                // Set the default value if cannot convert the value properly
                // or the value read from config file is empty
                try
                {
                    value = ((XmlElement) child).InnerText;
                    if (!string.IsNullOrEmpty(value))
                    {
                        Ret[i] = Convert.ChangeType(value, types[i]);
                        continue;
                    }
                }
                catch (Exception) { }

                Ret[i] = defaults[i];
            }

            return Ret;
        }
    }
}
