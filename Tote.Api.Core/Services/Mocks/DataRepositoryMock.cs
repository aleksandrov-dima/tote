using Bogus;
using Bogus.DataSets;
using Tote.Api.Core.Models;

namespace Tote.Api.Core.Services.Mocks;

public class DataRepositoryMock : IDataRepository
{
	private readonly Randomizer _random = new();
	private readonly Lorem _lorem = new("ru");
	private readonly Internet _internet = new("en");
	private readonly DateTime currentDateTimeNow = DateTime.Now;

	public Task AddUserProfileAsync(UserProfile userProfile, CancellationToken token = default)
	{
		return Task.CompletedTask;
	}

	public Task AddUserPreferencesAsync(UserPreference userPreference, CancellationToken token = default)
	{
		return Task.FromResult(Task.CompletedTask);
	}

	public Task AddUserActionsInfoAsync(UserActionsInfo actionsInfo, CancellationToken token = default)
	{
		return Task.FromResult(Task.CompletedTask);
	}

	public Task AddUserSessionInfoAsync(UserSessionInfo sessionInfo, CancellationToken token = default)
	{
		return Task.FromResult(Task.CompletedTask);
	}

	public Task AddUserSearchInfoAsync(UserSearchInfo searchInfo, CancellationToken token = default)
	{
		return Task.FromResult(Task.CompletedTask);
	}

	public UserProfile GetUserProfileAsync(string userIdentifier, CancellationToken token = default)
	{
		return new Faker<UserProfile>()
				.RuleFor(x => x.UserIdentifier, userIdentifier)
				.RuleFor(x => x.Preferences, () => new Faker<UserPreference>().Generate(_random.UShort()));
	}
}