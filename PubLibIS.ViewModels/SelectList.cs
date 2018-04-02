using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.ViewModels
{
    public class SelectList
    {
        public List<SelectListItem> Items { get; set; }
        public SelectList()
        {
            Items = new List<SelectListItem>();
        }
        public List<System.Web.Mvc.SelectListItem> GetMvcSelectList()
        {
            var newSelectList = new List<System.Web.Mvc.SelectListItem>();
            foreach(var item in this.Items){
                newSelectList.Add(new System.Web.Mvc.SelectListItem { Text = item.Text, Value = item.Value.ToString(), Selected = item.Selected });
            }
            return newSelectList;
        }
    }

    
    public class SelectListItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
