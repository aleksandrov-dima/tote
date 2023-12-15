using System.Collections;
using Tete.Api.Core.Models;

namespace Tete.Api.Core.Services;

/// <summary>
/// Определяет методы дял работы с телевизионными каналами
/// </summary>
public interface ITvProgrammLogic
{
	/// <summary>
	/// Получить список каналов
	/// </summary>
	public ICollection<TvChannel> GetChannels();

	/// <summary>
	/// Получить список передач для канала
	/// </summary>
	public ICollection<TvShow> GetTvShows(int channelId);

	/// <summary>
	/// Получить детальню информацию о передаче
	/// </summary>
	public TvShowDetail GetTvShowDetail(int channelId, int tvShowId);

	/// <summary>
	/// Поиск каналов и передач
	/// </summary>
	public ICollection<SearchResult> Search(string searchTerm);
}