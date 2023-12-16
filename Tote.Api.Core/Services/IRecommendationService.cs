using Tote.Api.Core.Models;

namespace Tote.Api.Core.Services;

/// <summary>
/// Сервис для работы с рекомендациями
/// </summary>
public interface IRecommendationService
{
	/// <summary>
	/// Получить рекомендуемый список передач на день
	/// </summary>
	/// <param name="userIdentifier">Идентификатор пользователя</param>
	/// <returns></returns>
	public Task<ICollection<RecommendationShow>> GetRecommendationShowsByTodayAsync(string userIdentifier);

	/// <summary>
	/// Получить рекомендуемый список передач на неделю
	/// </summary>
	/// <param name="userIdentifier">Идентификатор пользователя</param>
	/// <returns></returns>
	public Task<ICollection<RecommendationShow>> GetRecommendationShowsByWeekAsync(string userIdentifier);
}