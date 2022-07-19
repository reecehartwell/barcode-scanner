using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace name_sorter
{
    public class SortList
    {
        public List<string> SortedList(List<string> sortList)
        {
            // Sort the list by surname then first name
            sortList = sortList.OrderBy(x => x.Substring(x.LastIndexOf(' ') + 1) + x).ToList();

            return sortList;
        }
    }
}
