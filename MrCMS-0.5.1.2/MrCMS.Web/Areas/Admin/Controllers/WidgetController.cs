﻿using System;
using System.Web.Mvc;
using MrCMS.ACL;
using MrCMS.Entities.Documents.Layout;
using MrCMS.Entities.Documents.Web;
using MrCMS.Entities.Widget;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Areas.Admin.Helpers;
using MrCMS.Web.Areas.Admin.ModelBinders;
using MrCMS.Web.Areas.Admin.Models;
using MrCMS.Website;
using MrCMS.Website.Binders;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Areas.Admin.Controllers
{
    public class WidgetController : MrCMSAdminController
    {
        private readonly IDocumentService _documentService;
        private readonly IWidgetService _widgetService;

        public WidgetController(IDocumentService documentService, IWidgetService widgetService)
        {
            _documentService = documentService;
            _widgetService = widgetService;
        }

        [HttpGet]
        [ValidateInput(false)]
        public PartialViewResult Add(LayoutArea layoutArea, int pageId = 0, string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            var model = new AddWidgetModel
            {
                LayoutArea = layoutArea,
                Webpage = _documentService.GetDocument<Webpage>(pageId)
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Add")]
        public JsonResult Add_POST([IoCModelBinder(typeof (AddWidgetModelBinder))] Widget widget)
        {
            _widgetService.AddWidget(widget);

            return Json(widget.Id);
        }

        [HttpGet]
        [ValidateInput(false)]
        [ActionName("Edit")]
        [MrCMSTypeACL(typeof (Widget), TypeACLRule.Edit)]
        public ViewResultBase Edit_Get(Widget widget, string returnUrl = null)
        {
            widget.SetViewData(ViewData);

            if (!string.IsNullOrEmpty(returnUrl))
                ViewData["return-url"] = Referrer;
            else
                ViewData["return-url"] = returnUrl;

            return View(widget);
        }

        [HttpPost]
        [ValidateInput(false)]
        [MrCMSTypeACL(typeof (Widget), TypeACLRule.Edit)]
        public ActionResult Edit(Widget widget,
            string returnUrl = null)
        {
            _widgetService.SaveWidget(widget);

            return string.IsNullOrWhiteSpace(returnUrl)
                ? widget.Webpage != null
                    ? RedirectToAction("Edit", "Webpage", new {id = widget.Webpage.Id})
                    : (ActionResult) RedirectToAction("Edit", "LayoutArea", new {id = widget.LayoutArea.Id})
                : Redirect(returnUrl);
        }

        [HttpGet]
        [ActionName("Delete")]
        [MrCMSTypeACL(typeof (Widget), TypeACLRule.Delete)]
        public ActionResult Delete_Get(Widget widget)
        {
            return PartialView(widget);
        }

        [HttpPost]
        [MrCMSTypeACL(typeof (Widget), TypeACLRule.Delete)]
        public ActionResult Delete(Widget widget, string returnUrl)
        {
            int webpageId = 0;
            int layoutAreaId = 0;
            if (widget.Webpage != null)
                webpageId = widget.Webpage.Id;
            if (widget.LayoutArea != null)
                layoutAreaId = widget.LayoutArea.Id;
            _widgetService.DeleteWidget(widget);

            return !string.IsNullOrWhiteSpace(returnUrl) &&
                   !returnUrl.Contains("widget/edit/", StringComparison.OrdinalIgnoreCase)
                ? (ActionResult) Redirect(returnUrl)
                : webpageId > 0
                    ? RedirectToAction("Edit", "Webpage", new {id = webpageId, layoutAreaId})
                    : RedirectToAction("Edit", "LayoutArea", new {id = layoutAreaId});
        }

        [HttpGet]
        public ActionResult AddProperties([IoCModelBinder(typeof(AddWidgetPropertiesModelBinder))] Widget widget)
        {
            if (widget != null)
            {
                widget.SetViewData(ViewData);
                return PartialView(widget);
            }
            return new EmptyResult();
        }
    }
}