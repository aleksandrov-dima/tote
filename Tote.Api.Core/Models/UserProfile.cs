namespace Tote.Api.Core.Models;

/// <summary>
/// Информация о пользователе
/// </summary>
public class UserProfile
{
	public string UserIdentifier { get; set; }

	public List<UserPreference> Preferences { get; set; }
}