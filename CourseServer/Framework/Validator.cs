using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseServer.Framework
{
    public class Validator
    {
        public const string TAG = "Validator";

        public const char RULE_SEPARATOR = '|';

        public string ErrorDetail { get; set; }

        public List<string> ErrorDetailList;

        private List<string> SingleArugmentErrorList;

        private string[] Arguments;
        private string[] FieldNames;

        public Validator()
        {
            ErrorDetailList = new List<string>();
            SingleArugmentErrorList = new List<string>();
        }

        public bool Make(string[] args, string[] patterns, string[] fields)
        {
            if (args == null || args.Length == 0 ||
                patterns == null || patterns.Length == 0 ||
                fields == null || fields.Length == 0 ||
                args.Length != patterns.Length ||
                args.Length != fields.Length)
            {
                return false;
            }

            ErrorDetailList.Clear();
            // Reference to the arguments and fields name
            Arguments = args;
            FieldNames = fields;
            
            for (int i = 0, len = args.Length; i < len; i++)
            {
                // Default to passed
                if (TextUtils.isEmpty(patterns[i]))
                {
                    continue;
                }

                if (! MatchRule(args[i], patterns[i], fields[i]))
                {
                    ErrorDetailList.AddRange(SingleArugmentErrorList);
                }
            }

            return ErrorDetailList.Count == 0;
        }

        public bool MatchRule(string arg0, string rule, string fieldName)
        {
            if (TextUtils.isEmpty(rule))
                return false;

            SingleArugmentErrorList.Clear();

            string[] patternList = rule.Split('|');
            foreach (var item in patternList)
            {
                if (! matchRule(arg0, item, fieldName))
                {
                    SingleArugmentErrorList.Add(ErrorDetail);
                }
            }

            return SingleArugmentErrorList.Count == 0;
        }

        private bool matchRule(string arg0, string rule, string fieldName)
        {
            try
            {
                int value = 0;
                rule = rule.ToLower();

                if (Regex.IsMatch(rule, @"required"))
                {
                    ErrorDetail = string.Format("{0}.The field is required", fieldName);

                    return ! TextUtils.isEmpty(arg0);
                }
                if (Regex.IsMatch(rule, @"maxlength:[0-9]+"))
                {
                    value = int.Parse(rule.Split(':')[1]);
                    ErrorDetail = string.Format("{0}.The length should less than {1}", fieldName, value);

                    return arg0.Length <= value;
                }
                else if (Regex.IsMatch(rule, @"minlength:[0-9]+"))
                {
                    value = int.Parse(rule.Split(':')[1]);
                    ErrorDetail = string.Format("{0}.The length should great than {1}", fieldName, value);

                    return arg0.Length >= int.Parse(rule.Split(':')[1]);
                }
                else if (Regex.IsMatch(rule, @"max:[0-9]+"))
                {
                    value = int.Parse(rule.Split(':')[1]);
                    ErrorDetail = string.Format("{0}.The value should less than {1}", fieldName, value);

                    return int.Parse(arg0) <= value;
                }
                else if (Regex.IsMatch(rule, @"min:[0-9]+"))
                {
                    value = int.Parse(rule.Split(':')[1]);
                    ErrorDetail = string.Format("{0}.The value should great than {1}", fieldName, value);

                    return int.Parse(arg0) >= value;
                }
                else if (Regex.IsMatch(rule, @"email"))
                {
                    ErrorDetail = string.Format("{0}.Invaid email format", fieldName);
                    // References: http://emailregex.com/
                    return Regex.IsMatch(arg0, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                }
                else if (Regex.IsMatch(rule, @"match:[A-Za-z_]+\w*"))
                {
                    int index = -1;
                    string field = rule.Split(':')[1];

                    ErrorDetail = string.Format("{0}.Both value of field dosen't matched", fieldName);

                    if (FieldNames == null || Arguments == null)
                        return false;
                    // Give the index in list of fields
                    index = TextUtils.Find(FieldNames, field);
                    if (index == -1 || index >= Arguments.Length)
                        return false;

                    return arg0 == Arguments[index];
                }
                else
                {
                    ErrorDetail = fieldName + ".Invalid format";
                    return Regex.IsMatch(arg0, rule);
                }
            }
            catch (Exception e)
            {
                Dumper.Log(TAG, string.Format("An error occured when validate the rule {0}: {1}", 
                    rule, e.Message));
            }

            return false;
        }

        public string[] GetDetail()
        {
            if (ErrorDetailList.Count == 0)
                ErrorDetailList.Add(ErrorDetail);

            return ErrorDetailList.ToArray();
        } 
    }
}
