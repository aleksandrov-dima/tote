namespace Tote.Api.Core.Models;

/// <summary>
/// ИНформация о предпочтениях пользователя
/// </summary>
public class UserPreference
{
	public int ChannelId { get; set; }
	
	/// <summary>
	/// Жанр
	/// </summary>
	public string Category { get; set; }
}