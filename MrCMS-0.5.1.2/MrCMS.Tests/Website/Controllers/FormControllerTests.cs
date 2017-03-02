using System.Web;
using System.Web.Mvc;
using FakeItEasy;
using FluentAssertions;
using MrCMS.Entities.Documents.Web;
using MrCMS.Services;
using MrCMS.Tests.Stubs;
using MrCMS.Website.Controllers;
using Xunit;

namespace MrCMS.Tests.Website.Controllers
{
    public class FormControllerTests
    {
        private readonly IDocumentService _documentService;
        private readonly FormController _formController;
        private readonly IFormPostingHandler _formPostingHandler;

        public FormControllerTests()
        {
            _documentService = A.Fake<IDocumentService>();
            _formPostingHandler = A.Fake<IFormPostingHandler>();
            _formController = new FormController(_documentService, _formPostingHandler)
            {
                RequestMock =
                    A.Fake<HttpRequestBase>(),
                ReferrerOverride = "http://www.example.com/test-redirect"
            };
        }

        [Fact]
        public void FormController_Save_CallsFormServiceSaveFormDataWithPassedObjects()
        {
            var stubWebpage = new StubWebpage();
            A.CallTo(() => _documentService.GetDocument<Webpage>(1)).Returns(stubWebpage);
            ActionResult result = _formController.Save(1);

            A.CallTo(() => _formPostingHandler.SaveFormData(stubWebpage, _formController.Request)).MustHaveHappened();
        }

        [Fact]
        public void FormController_Save_SetsTempDataFormSubmittedToTrue()
        {
            var stubWebpage = new StubWebpage();

            A.CallTo(() => _documentService.GetDocument<Webpage>(1)).Returns(stubWebpage);
            ActionResult result = _formController.Save(1);

            _formController.TempData["form-submitted"].Should().Be(true);
        }

        [Fact]
        public void FormController_Save_ReturnsRedirectToTheReferrer()
        {
            var stubWebpage = new StubWebpage();

            A.CallTo(() => _documentService.GetDocument<Webpage>(1)).Returns(stubWebpage);
            ActionResult result = _formController.Save(1);

            result.Should().BeOfType<RedirectResult>();
            result.As<RedirectResult>().Url.Should().Be("http://www.example.com/test-redirect");
        }
    }
}