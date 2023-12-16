using Tote.Api.Core.Models;

namespace Tote.Api.Core.Services;

/// <summary>
/// Определяет методы для работы с хранилищем данных
/// </summary>
public interface IDataRepository
{
	/// <summary>
	/// Добавление информации о пользователе
	/// </summary>
	/// <param name="userProfile"></param>
	/// <returns></returns>
	public Task AddUserProfileAsync(UserProfile userProfile, CancellationToken token = default);

	/// <summary>
	/// Добавление информации о предпочтениях пользователя
	/// </summary>
	/// <param name="userPreference"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task AddUserPreferencesAsync(UserPreference userPreference, CancellationToken token = default);

	/// <summary>
	/// Добавление информации о действиях пользователя
	/// </summary>
	/// <returns></returns>
	public Task AddUserActionsInfoAsync(UserActionsInfo actionsInfo, CancellationToken token = default);

	/// <summary>
	/// Добавление информации о сессии пользователя
	/// </summary>
	public Task AddUserSessionInfoAsync(UserSessionInfo sessionInfo, CancellationToken token = default);

	/// <summary>
	/// Добавление информации о поисковом запросе пользователя
	/// </summary>
	/// <param name="searchInfo"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task AddUserSearchInfoAsync(UserSearchInfo searchInfo, CancellationToken token = default);

	/// <summary>
	/// ПОлучить профиль пользователя
	/// </summary>
	/// <param name="userIdentifier">Идентификатор пользователя</param>
	/// <param name="token"></param>
	public UserProfile GetUserProfileAsync(string userIdentifier, CancellationToken token = default);
}