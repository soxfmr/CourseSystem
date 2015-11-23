using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProvider.Events
{
    public class BaseEventArgs : EventArgs
    {
        #region Initialzed Local Variant
        public int RequestCode;

        public int Status;

        public string Payload;
        #endregion

        #region Common Local Variant
        public bool IsSuccess;

        public List<string> ErrorMessage;
        #endregion

        public void LoadEventArgs(BaseEventArgs e)
        {
            RequestCode = e.RequestCode;
            Status = e.Status;
            Payload = e.Payload;

            IsSuccess = e.IsSuccess;
            ErrorMessage = e.ErrorMessage;
        }
    }
}
