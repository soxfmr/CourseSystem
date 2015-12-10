using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Domain
{
    public interface INotifiedableView
    {
        /// <summary>
        /// Initialized the data in the child view to ensure that the ViewModel can asscess
        /// the view safety.
        /// </summary>
        void Notify();
    }
}
