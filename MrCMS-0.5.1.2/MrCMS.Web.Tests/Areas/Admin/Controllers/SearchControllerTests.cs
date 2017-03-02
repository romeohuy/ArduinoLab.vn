namespace MrCMS.Web.Tests.Areas.Admin.Controllers
{
    public class SearchControllerTests
    {
        //private static IAdminWebpageSearchService searchService;
        //private static ISiteMapService navigationService;

        //[Fact]
        //public void SearchController_GetSearchResults_NullStringShouldReturnEmptyObject()
        //{
        //    SearchController searchController = GetSearchController();

        //    JsonResult result = searchController.GetSearchResults(null, null);

        //    result.Data.Should().BeOfType<object>();
        //}

        //private static SearchController GetSearchController()
        //{
        //    searchService = A.Fake<IAdminWebpageSearchService>();
        //    navigationService = A.Fake<ISiteMapService>();
        //    var searchController = new SearchController(searchService, navigationService, A.Fake<IDocumentService>());
        //    return searchController;
        //}

        //[Fact]
        //public void SearchController_GetSearchResults_EmptyStringShouldReturnEmptyObject()
        //{
        //    SearchController searchController = GetSearchController();

        //    JsonResult result = searchController.GetSearchResults("", null);

        //    result.Data.Should().BeOfType<object>();
        //}

        //[Fact]
        //public void SearchController_GetSearchResults_WhiteSpaceStringgShouldReturnEmptyObject()
        //{
        //    SearchController searchController = GetSearchController();

        //    JsonResult result = searchController.GetSearchResults("  ", null);

        //    result.Data.Should().BeOfType<object>();
        //}

        //[Fact]
        //public void SearchController_GetSearchResults_CallsDocumentServiceSearchDocuments()
        //{
        //    SearchController searchController = GetSearchController();

        //    searchController.GetSearchResults("test", null);

        //    // this assertation is to replace the following, which seems to be unreliable with some test runners
        //    //A.CallTo(() => searchService.SearchDocuments<Document>("test")).MustHaveHappened();
        //    var calls = Fake.GetCalls(searchService);

        //    calls.Where(
        //        call =>
        //        call.Method.Name == "SearchDocuments" && call.Method.GetGenericArguments()[0] == typeof (Document) &&
        //        call.Arguments[0] == "test").Should().HaveCount(1);
        //}

        //[Fact]
        //public void SearchController_GetSearchResults_ReturnsIEnumerableSearchResultModels()
        //{
        //    SearchController searchController = GetSearchController();

        //    IEnumerable<SearchResultModel> searchResultModels = A.CollectionOfFake<SearchResultModel>(1);
        //    A.CallTo(() => searchService.SearchDocuments<Document>("test")).Returns(
        //        searchResultModels);

        //    JsonResult searchResults = searchController.GetSearchResults("test", null);

        //    searchResults.Data.As<IEnumerable<SearchResultModel>>().Should().BeEquivalentTo(searchResultModels);
        //}

        //[Fact]
        //public void SearchController_GetSearchResultsDetailed_TypeSetCallsSearchDocumentsWithCorrectGenericType()
        //{
        //    SearchController searchController = GetSearchController();

        //    searchController.GetSearchResults("test", "TextPage");

        //    // this assertation is to replace the following, which seems to be unreliable with some test runners
        //    //A.CallTo(() => searchService.SearchDocuments<TextPage>("test")).MustHaveHappened();
        //    var calls = Fake.GetCalls(searchService);

        //    calls.Where(
        //        call =>
        //        call.Method.Name == "SearchDocuments" && call.Method.GetGenericArguments()[0] == typeof(TextPage) &&
        //        call.Arguments[0] == "test").Should().HaveCount(1);
        //}

        //[Fact]
        //public void SearchController_Index_ReturnsViewResult()
        //{
        //    SearchController searchController = GetSearchController();

        //    searchController.Index("searchterm", "TextPage", 1, 1).Should().BeOfType<ViewResult>();
        //}

        //[Fact]
        //public void SearchController_Index_SetsViewData()
        //{
        //    SearchController searchController = GetSearchController();

        //    var selectListItems = new List<SelectListItem>();
        //    var documentTypes = new List<SelectListItem>();

        //    A.CallTo(() => navigationService.GetParentsList()).Returns(selectListItems);
        //    A.CallTo(() => navigationService.GetDocumentTypes("TextPage")).Returns(documentTypes);

        //    var result = searchController.Index("searchterm", "TextPage", 1, 1).As<ViewResult>();

        //    result.ViewData["term"].Should().Be("searchterm");
        //    result.ViewData["type"].Should().Be("TextPage");
        //    result.ViewData["parent-val"].Should().Be(1);
        //    result.ViewData["parents"].Should().Be(selectListItems);
        //    result.ViewData["doc-types"].Should().Be(documentTypes);
        //}

        //[Fact]
        //public void SearchController_Index_IfTypeIsSetUsesTypePassedAsGenericArgument()
        //{
        //    SearchController searchController = GetSearchController();

        //    searchController.Index("searchterm", "TextPage", 1, 1).As<ViewResult>();

        //    A.CallTo(() => searchService.SearchDocumentsDetailed<TextPage>("searchterm", 1, 1)).MustHaveHappened();
        //}

        //[Fact]
        //public void SearchController_Index_IfTypeIsNotSetUsesDocument()
        //{
        //    SearchController searchController = GetSearchController();

        //    searchController.Index("searchterm", "", 1, 1).As<ViewResult>();

        //    A.CallTo(() => searchService.SearchDocumentsDetailed<Document>("searchterm", 1, 1)).MustHaveHappened();
        //}

        //[Fact]
        //public void SearchController_IndexPost_ReturnsRedirectToRoute()
        //{
        //    SearchController searchController = GetSearchController();

        //    searchController.IndexPost("test", "TextPage", 1).Should().BeOfType<RedirectToRouteResult>();
        //}

        //[Fact]
        //public void SearchController_IndexPost_PassesArgumentsAsRouteValues()
        //{
        //    SearchController searchController = GetSearchController();

        //    var result = searchController.IndexPost("test", "TextPage", 1).As<RedirectToRouteResult>();

        //    result.RouteValues["action"].Should().Be("Index");
        //    result.RouteValues["term"].Should().Be("test");
        //    result.RouteValues["type"].Should().Be("TextPage");
        //    result.RouteValues["parent"].Should().Be(1);
        //}
    }
}