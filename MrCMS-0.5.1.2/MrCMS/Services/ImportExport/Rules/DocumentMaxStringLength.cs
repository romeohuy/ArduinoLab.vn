﻿using System;
using System.Collections.Generic;
using MrCMS.Services.ImportExport.DTOs;

namespace MrCMS.Services.ImportExport.Rules
{
    public abstract class DocumentMaxStringLength : IDocumentImportValidationRule
    {
        protected string DisplayName { get; private set; }
        protected Func<DocumentImportDTO, string> Selector { get; private set; }
        protected int Length { get; private set; }

        protected DocumentMaxStringLength(string displayName, Func<DocumentImportDTO,string> selector, int length)
        {
            DisplayName = displayName;
            Selector = selector;
            Length = length;
        }

        public IEnumerable<string> GetErrors(DocumentImportDTO item, IList<DocumentImportDTO> allItems)
        {
            var value = Selector(item);
            if (!String.IsNullOrWhiteSpace(value))
            {
                if (value.Length > Length)
                    yield return
                        string.Format(
                            "{0} is too long - max length is {1} characters and your value is {2} characters in length.",
                            DisplayName, Length, value.Length);
            }
        }
    }
}