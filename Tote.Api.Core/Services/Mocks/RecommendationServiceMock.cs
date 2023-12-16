using Bogus;
using Tote.Api.Core.Models;

namespace Tote.Api.Core.Services.Mocks;

public class RecommendationServiceMock : IRecommendationService
{

	public Task<ICollection<RecommendationShow>> GetRecommendationShowsByTodayAsync(string userIdentifier)
	{
		var genrecommendations = new Faker<RecommendationShow>().RuleFor(r => r.Show, () => new Faker<TvShow>());
		return Task.FromResult<ICollection<RecommendationShow>>(genrecommendations.Generate(10));
	}

	public Task<ICollection<RecommendationShow>> GetRecommendationShowsByWeekAsync(string userIdentifier)
	{
		var genrecommendations = new Faker<RecommendationShow>().RuleFor(r => r.Show, () => new Faker<TvShow>());
		return Task.FromResult<ICollection<RecommendationShow>>(genrecommendations.Generate(30));
	}
}