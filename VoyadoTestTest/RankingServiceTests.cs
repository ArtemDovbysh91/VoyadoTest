using VoyadoTest.Business;
using VoyadoTest.Infrastructure;
using VoyadoTest.Services;

namespace VoyadoTestTest;

public class RankingServiceTests
{
    private RankingService? _rankingService;
    private Mock<IBingSearch>? _bingSearchMock;
    private Mock<IGoogleSearch>? _googleSearchMock;
    
    [SetUp]
    public void Setup()
    {
        _bingSearchMock = new Mock<IBingSearch>();
        _bingSearchMock.Setup(b => b.Search(It.IsAny<string>()))
            .Returns<string>((a) => Task.FromResult((float)a.Length));

        var bingSearchFactoryMock = new Mock<IBingSearchFactory>();
        bingSearchFactoryMock.Setup(bsf => bsf.GetBingSearch())
            .Returns(_bingSearchMock.Object);

        _googleSearchMock = new Mock<IGoogleSearch>();
        _googleSearchMock.Setup(g => g.Search(It.IsAny<string>()))
            .Returns<string>((a) => Task.FromResult((float)a.Length * 2));

        _rankingService = new RankingService(bingSearchFactoryMock.Object, _googleSearchMock.Object);
    }

    [Test]
    public async Task OneWordRankTest()
    {
        // Arrange,
        var queryString = "Voyado";
        
        // Act
        var result = await _rankingService!.RankQuery(queryString);

        // Assert
        result.BingSearchResult.Should().Be((float)queryString.Length);
        result.GoogleSearchResult.Should().Be((float)queryString.Length * 2);
        
        _bingSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Once);
        _googleSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Once);
    }
    
    [Test]
    public async Task NoWordRankTest()
    {
        // Arrange,
        var queryString = string.Empty;
        
        // Act
        var result = await _rankingService!.RankQuery(queryString);

        // Assert
        result.BingSearchResult.Should().Be((float)queryString.Length);
        result.GoogleSearchResult.Should().Be((float)queryString.Length * 2);
        
        _bingSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Never);
        _googleSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Never);
    }
    
    [Test]
    public async Task TwoWordsRankTest()
    {
        // Arrange,
        var queryString = "Voyado company";
        
        // Act
        var result = await _rankingService!.RankQuery(queryString);

        // Assert
        result.BingSearchResult.Should().Be((float)queryString.Length - 1);
        result.GoogleSearchResult.Should().Be(((float)queryString.Length - 1) * 2);
        
        _bingSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(2));
        _googleSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(2));
    }
    
    [Test]
    public async Task TwoWordsRankTest_WithSpaces()
    {
        // Arrange,
        var queryString = "    Voyado    company    ";
        
        // Act
        var result = await _rankingService!.RankQuery(queryString);

        // Assert
        result.BingSearchResult.Should().Be((float)13);
        result.GoogleSearchResult.Should().Be((float)13 * 2);
        
        _bingSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(2));
        _googleSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(2));
    }
    
        
    [Test]
    public async Task TwoWordsRankTest_ShouldWorkAsSum()
    {
        // Arrange,
        var aggregatedQueryString = "Voyado company";
        var compoundQueryString1 = "Voyado";
        var compoundQueryString2 = "company";
        
        // Act
        var aggregatedResult = await _rankingService!.RankQuery(aggregatedQueryString);
        var compoundResult1 = await _rankingService.RankQuery(compoundQueryString1);
        var compoundResult2 = await _rankingService.RankQuery(compoundQueryString2);

        // Assert
        aggregatedResult.BingSearchResult.Should().Be(compoundResult1.BingSearchResult + compoundResult2.BingSearchResult);
        aggregatedResult.GoogleSearchResult.Should().Be(compoundResult1.GoogleSearchResult + compoundResult2.GoogleSearchResult);
        
        _bingSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(4));
        _googleSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(4));
    }
    
    [Test]
    public async Task AlphabetRankTest_ShouldWorkAsSum()
    {
        // Arrange,
        var queryString = "a b c d e f g h i j c l m n o p q r s t u v w x y z";
        var numberOfLetters = 26;

        // Act
        var aggregatedResult = await _rankingService!.RankQuery(queryString);

        // Assert
        aggregatedResult.BingSearchResult.Should().Be(numberOfLetters);
        aggregatedResult.GoogleSearchResult.Should().Be(numberOfLetters * 2);
        
        _bingSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(numberOfLetters));
        _googleSearchMock?.Verify(bs => bs.Search(It.IsAny<string>()), Times.Exactly(numberOfLetters));
    }
}