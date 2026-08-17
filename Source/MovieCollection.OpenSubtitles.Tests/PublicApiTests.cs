using System.Threading.Tasks;
using PublicApiGenerator;
using VerifyXunit;
using Xunit;

namespace MovieCollection.OpenSubtitles.Tests
{
    public class PublicApiTests
    {
        [Fact]
        public Task PublicApiShouldNotChange()
        {
            var publicApi = typeof(OpenSubtitlesService).Assembly
                .GeneratePublicApi();

            return Verifier.Verify(publicApi);
        }
    }
}
