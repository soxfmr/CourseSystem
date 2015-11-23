using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider
{
    public class CourseProviderContract
    {
        public const string KEY_ROUTE = "_route";

        public const string KEY_PARAM = "_param";

        public const string KET_GENERIC = "_generic";

        public const string KEY_ERROR = "_err";

        public const string KEY_VALIDATOR = "_msg";

        public const string KEY_AUTH = "_auth";

        public const string KEY_PAYLOAD = "_payload";

        public const int RESULT_FAILED = -1;

        public const int RESULT_SUCCESS = 0;

        public const int MODE_STUDENT = 0;

        public const int MODE_TEACHER = 1;

        public const int MODE_MANAGER = 2;

        public const int REG_ALREADY_EXISTS = 1;
    }
}
