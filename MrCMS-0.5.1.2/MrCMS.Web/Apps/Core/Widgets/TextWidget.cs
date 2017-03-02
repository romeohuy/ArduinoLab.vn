﻿using System.Web.Mvc;
using MrCMS.Entities.Widget;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Core.Widgets
{
    [OutputCacheable]
    public class TextWidget : Widget
    {
        [AllowHtml]
        public virtual string Text { get; set; }
    }
}
