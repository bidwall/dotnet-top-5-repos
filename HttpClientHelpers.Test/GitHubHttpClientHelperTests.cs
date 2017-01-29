using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace HttpClientHelpers.Test
{
    [TestFixture]
    public class GitHubHttpClientHelperTests
    {
        private IHttpReponseProvider _mockHttpReponseProvider;
        private GitHubHttpClientHelper _gitHubHttpClientHelper;
        private const string Url = "http://github.com/";

        [SetUp]
        public void TestSetup()
        {
            _mockHttpReponseProvider = Mock.Create<IHttpReponseProvider>(Behavior.Strict);
            _gitHubHttpClientHelper = new GitHubHttpClientHelper(_mockHttpReponseProvider);
        }

        [Test]
        public void GetDataFromUrl_GiveUrl_SetsBaseAddress()
        {
            //Arrange
            Expression<Predicate<HttpClientConfig>> expression = y => y.BaseAddress.AbsoluteUri == Url;
            _mockHttpReponseProvider.Arrange(x => x.GetResponse<string>(Arg.Matches(expression))).Returns(Task.FromResult(string.Empty));
            
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);
        }

        [Test]
        public void GetDataFromUrl_SetsRequestUriToEmptyString()
        {
            //Arrange
            Expression<Predicate<HttpClientConfig>> expression = y => y.RequestUri == string.Empty;
            _mockHttpReponseProvider.Arrange(x => x.GetResponse<string>(Arg.Matches(expression))).Returns(Task.FromResult(string.Empty));

            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);
        }

        [Test]
        public void GetDataFromUrl_SetsAcceptsHeaders()
        {
            //Arrange
            Expression<Predicate<HttpClientConfig>> expression = y => y.AcceptHeaders.Count == 1 && y.AcceptHeaders[0].MediaType == Constants.JsonContentType;
            _mockHttpReponseProvider.Arrange(x => x.GetResponse<string>(Arg.Matches(expression))).Returns(Task.FromResult(string.Empty));

            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);
        }

        [Test]
        public void GetDataFromUrl_SetsUserAgentHeaders()
        {
            //Arrange
            Expression<Predicate<HttpClientConfig>> expression = y => y.UserAgentHeaders.Count == 1 && 
                                                                      y.UserAgentHeaders[0].Key == Constants.UserAgentHeaderKey && 
                                                                      y.UserAgentHeaders[0].Value == string.Empty;
            _mockHttpReponseProvider.Arrange(x => x.GetResponse<string>(Arg.Matches(expression))).Returns(Task.FromResult(string.Empty));

            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);
        }
    }
}