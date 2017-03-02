﻿using System.Collections.Generic;
using System.Web.Mvc;
using FakeItEasy;
using FakeItEasy.Core;
using FluentAssertions;
using MrCMS.Entities.Documents.Web;
using MrCMS.Entities.Documents.Web.FormProperties;
using MrCMS.Settings;
using MrCMS.Shortcodes.Forms;
using MrCMS.Tests.Stubs;
using MrCMS.Website;
using Ninject.MockingKernel;
using Xunit;
using System.Linq;

namespace MrCMS.Tests.Shortcodes.Forms
{
    public class DefaultFormRendererTests
    {
        private readonly DefaultFormRenderer _defaultFormRenderer;
        private readonly IElementRendererManager _elementRendererManager;
        private readonly ILabelRenderer _labelRenderer;
        private readonly IValidationMessaageRenderer _validationMessageRenderer;
        private string _existingValue = null;
        private readonly FormCollection _formCollection;
        private readonly ISubmittedMessageRenderer _submittedMessageRenderer;
        private readonly SiteSettings _siteSettings;

        public DefaultFormRendererTests()
        {
            _formCollection = new FormCollection();
            var mockingKernel = new MockingKernel();
            MrCMSKernel.OverrideKernel(mockingKernel);
            _elementRendererManager = A.Fake<IElementRendererManager>();
            _labelRenderer= A.Fake<ILabelRenderer>();
            _validationMessageRenderer= A.Fake<IValidationMessaageRenderer>();
            _submittedMessageRenderer = A.Fake<ISubmittedMessageRenderer>();
            _siteSettings = new SiteSettings();
            _defaultFormRenderer = new DefaultFormRenderer(_elementRendererManager, _labelRenderer,
                                                           _validationMessageRenderer,_submittedMessageRenderer, _siteSettings);
        }

        [Fact]
        public void DefaultFormRenderer_GetDefault_ShouldReturnAnEmptyStringIfThereAreNoProperties()
        {
            var stubWebpage = new StubWebpage();

            var @default = _defaultFormRenderer.GetDefault(stubWebpage, new FormSubmittedStatus(false, null, null));

            @default.Should().Be(string.Empty);
        }

        [Fact]
        public void DefaultFormRenderer_GetDefault_ShouldReturnAnEmptyStringIfWebpageIsNull()
        {
            var @default = _defaultFormRenderer.GetDefault(null, new FormSubmittedStatus(false, null, null));

            @default.Should().Be(string.Empty);
        }

        [Fact]
        public void DefaultFormRenderer_GetDefault_ShouldCallGetElementRendererOnEachProperty()
        {
            var textBox = new TextBox { Name = "test-1" };
            var stubWebpage = new StubWebpage
                                  {
                                      FormProperties = new List<FormProperty> { textBox }
                                  };
            var formElementRenderer = A.Fake<IFormElementRenderer>();
            A.CallTo(() => _elementRendererManager.GetElementRenderer<FormProperty>(textBox))
             .Returns(formElementRenderer);
            A.CallTo(() => formElementRenderer.AppendElement(textBox, _existingValue, _siteSettings.FormRendererType)).Returns(new TagBuilder("input"));

            _defaultFormRenderer.GetDefault(stubWebpage, new FormSubmittedStatus(false, null, _formCollection));

            A.CallTo(() => _elementRendererManager.GetElementRenderer<FormProperty>(textBox)).MustHaveHappened();
        }


        [Fact]
        public void DefaultFormRenderer_GetDefault_ShouldCallAppendLabelOnLabelRendererForEachProperty()
        {
            var textBox = new TextBox { Name = "test-1" };
            var stubWebpage = new StubWebpage
                                  {
                                      FormProperties = new List<FormProperty> { textBox }
                                  };
            var formElementRenderer = A.Fake<IFormElementRenderer>();
            A.CallTo(() => _elementRendererManager.GetElementRenderer<FormProperty>(textBox))
             .Returns(formElementRenderer);
            A.CallTo(() => formElementRenderer.AppendElement(textBox, _existingValue, _siteSettings.FormRendererType)).Returns(new TagBuilder("input"));

            _defaultFormRenderer.GetDefault(stubWebpage, new FormSubmittedStatus(false, null, _formCollection));

            A.CallTo(() => _labelRenderer.AppendLabel(textBox)).MustHaveHappened();
        }

        [Fact]
        public void DefaultFormRenderer_GetDefault_ShouldCallAppendControlOnElementRenderer()
        {
            var textBox = new TextBox { Name = "test-1" };
            var stubWebpage = new StubWebpage
            {
                FormProperties = new List<FormProperty> { textBox }
            };
            var formElementRenderer = A.Fake<IFormElementRenderer>();
            A.CallTo(() => _elementRendererManager.GetElementRenderer<FormProperty>(textBox))
             .Returns(formElementRenderer);
            A.CallTo(() => formElementRenderer.AppendElement(textBox, _existingValue, _siteSettings.FormRendererType)).Returns(new TagBuilder("input"));

            _defaultFormRenderer.GetDefault(stubWebpage, new FormSubmittedStatus(false, null, _formCollection));

            A.CallTo(() => formElementRenderer.AppendElement(textBox, _existingValue, _siteSettings.FormRendererType)).MustHaveHappened();
        }

        [Fact]
        public void DefaultFormRenderer_GetDefault_ShouldCallRenderLabelThenRenderElementForEachProperty()
        {
            var textBox1 = new TextBox { Name = "test-1" };
            var textBox2 = new TextBox { Name = "test-2" };

            var stubWebpage = new StubWebpage
            {
                FormProperties = new List<FormProperty> { textBox1, textBox2 }
            };
            var formElementRenderer = A.Fake<IFormElementRenderer>();
            A.CallTo(() => formElementRenderer.AppendElement(textBox1, _existingValue, _siteSettings.FormRendererType)).Returns(new TagBuilder("input"));
            A.CallTo(() => formElementRenderer.AppendElement(textBox2, _existingValue, _siteSettings.FormRendererType)).Returns(new TagBuilder("input"));
            A.CallTo(() => _elementRendererManager.GetElementRenderer<FormProperty>(textBox1))
             .Returns(formElementRenderer);
            A.CallTo(() => _elementRendererManager.GetElementRenderer<FormProperty>(textBox2))
             .Returns(formElementRenderer);

            _defaultFormRenderer.GetDefault(stubWebpage, new FormSubmittedStatus(false, null, _formCollection));

            List<ICompletedFakeObjectCall> elementRendererCalls = Fake.GetCalls(formElementRenderer).ToList();
            List<ICompletedFakeObjectCall> labelRendererCalls = Fake.GetCalls(_labelRenderer).ToList();

            labelRendererCalls.Where(x => x.Method.Name == "AppendLabel").Should().HaveCount(2);
            elementRendererCalls.Where(x => x.Method.Name == "AppendElement").Should().HaveCount(2);
        }

        [Fact]
        public void DefaultFormRenderer_GetForm_ShouldHaveTagTypeOfForm()
        {
            var tagBuilder = _defaultFormRenderer.GetForm(new StubWebpage());

            tagBuilder.TagName.Should().Be("form");
        }

        [Fact]
        public void DefaultFormRenderer_GetForm_ShouldHaveMethodPost()
        {
            var tagBuilder = _defaultFormRenderer.GetForm(new StubWebpage());
            tagBuilder.Attributes["method"].Should().Be("POST");
        }

        [Fact]
        public void DefaultFormRenderer_GetForm_ShouldHaveActionSaveFormWithTheIdPassed()
        {
            var tagBuilder = _defaultFormRenderer.GetForm(new StubWebpage
                                                              {
                                                                  Id = 123
                                                              });
            tagBuilder.Attributes["action"].Should().Be("/save-form/123");
        }

        [Fact]
        public void DefaultFormRenderer_GetSubmitButton_ShouldReturnAnInput()
        {
            var submitButton = _defaultFormRenderer.GetSubmitButton(new StubWebpage
            {
            });

            submitButton.TagName.Should().Be("input");
        }

        [Fact]
        public void DefaultFormRenderer_GetSubmitButton_ShouldBeOfTypeSubmit()
        {
            var submitButton = _defaultFormRenderer.GetSubmitButton(new StubWebpage
            {
            });

            submitButton.Attributes["type"].Should().Be("submit");
        }

        [Fact]
        public void DefaultFormRenderer_GetSubmitButton_ValueShouldBeSubmitForm()
        {
            var submitButton = _defaultFormRenderer.GetSubmitButton(new StubWebpage
            {
            });

            submitButton.Attributes["value"].Should().Be("Submit");
        }

        [Fact]
        public void DefaultFormRenderer_GetSubmitButton_CssClassShouldBeCustomIfSet()
        {
            var submitButton = _defaultFormRenderer.GetSubmitButton(new StubWebpage
            {
                SubmitButtonCssClass = "my-css-button-class"
            });

            submitButton.Attributes["class"].Should().Be("my-css-button-class");
        }
    }
}