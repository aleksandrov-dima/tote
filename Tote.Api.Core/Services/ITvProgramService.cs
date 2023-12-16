using System.Collections;
using Tote.Api.Core.Models;

namespace Tote.Api.Core.Services;

/// <summary>
/// Сервис для работы с телевизионной программой
/// </summary>
public interface ITvProgramService
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