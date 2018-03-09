using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.BLL.Interfaces
{
    public interface IJsonProcessor
    {
        string GetJson(IEnumerable<int> idList);
        void SetJson(string IdList);
    }
}
