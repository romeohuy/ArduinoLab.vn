using System.Collections.Generic;
using System.Linq;
using MrCMS.Entities.Documents.Web;
using MrCMS.Paging;

namespace MrCMS.Web.Areas.Admin.Models
{
    public class PostingsModel : AsyncListModel<FormPosting>
    {
        public string Search { get; set; }
        public PostingsModel(IPagedList<FormPosting> items, int id)
            : base(items, id)
        {
        }

        public IEnumerable<string> Headings
        {
            get { return Items.SelectMany(posting => posting.FormValues).Select(value => value.Key).Distinct().Take(7); }
        }
    }
}