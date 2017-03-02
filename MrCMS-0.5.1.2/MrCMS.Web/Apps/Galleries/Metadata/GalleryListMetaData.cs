﻿using System;
using System.Collections.Generic;
using MrCMS.Entities.Documents;
using MrCMS.Entities.Documents.Metadata;
using MrCMS.Web.Apps.Galleries.Pages;

namespace MrCMS.Web.Apps.Galleries.Metadata
{
    public class GalleryListMetaData : DocumentMetadataMap<GalleryList>
    {
        public override string IconClass
        {
            get
            {
                return "glyphicon glyphicon-th";
            }
        }
        public override ChildrenListType ChildrenListType
        {
            get { return ChildrenListType.WhiteList; }
        }

        public override IEnumerable<Type> ChildrenList
        {
            get { yield return typeof(Pages.Gallery); }
        }
    }
}

