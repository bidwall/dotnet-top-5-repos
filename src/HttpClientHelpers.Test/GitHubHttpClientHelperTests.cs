using System;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;

namespace HttpClientHelpers.Test
{
    [TestFixture]
    public class GitHubHttpClientHelperTests
    {
        private Mock<IHttpResponseProvider> _mockHttpReponseProvider;
        private GitHubHttpClientHelper _gitHubHttpClientHelper;
        private const string Url = "http://github.com/";

        [SetUp]
        public void TestSetup()
        {
            _mockHttpReponseProvider = new Mock<IHttpResponseProvider>();
            _gitHubHttpClientHelper = new GitHubHttpClientHelper(_mockHttpReponseProvider.Object);
        }

        [Test]
        public void GetDataFromUrl_GiveUrl_SetsBaseAddress()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.BaseAddress.AbsoluteUri == Url;
            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }

        [Test]
        public void GetDataFromUrl_SetsRequestUriToEmptyString()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.RequestUri == string.Empty;
            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }

        [Test]
        public void GetDataFromUrl_SetsAcceptsHeaders()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.AcceptHeaders.Count == 1 && y.AcceptHeaders[0].MediaType == Constants.JsonContentType;
            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }

        [Test]
        public void GetDataFromUrl_SetsUserAgentHeaders()
        {
            //Act
            _gitHubHttpClientHelper.GetDataFromUrl<string>(Url);

            //Assert
            Expression<Func<HttpClientConfig, bool>> expression = y => y.UserAgentHeaders.Count == 1 &&
                                                                       y.UserAgentHeaders[0].Key == Constants.UserAgentHeaderKey &&
                                                                       y.UserAgentHeaders[0].Value == string.Empty;

            _mockHttpReponseProvider.Verify(x => x.GetResponse<string>(It.Is(expression)), Times.Once);
        }
    }
}