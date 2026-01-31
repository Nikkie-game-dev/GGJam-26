using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Service
{
    public interface IService
    {
        internal bool IsPersistance { get; }
    }
}
