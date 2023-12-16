using Bogus;
using Bogus.DataSets;
using Tote.Api.Core.Models;

namespace Tote.Api.Core.Services.Mocks;

public class TvProgramServiceMock : ITvProgramService
{
	private readonly Randomizer _random = new();
	private readonly Lorem _lorem = new("ru");
	private readonly Internet _internet = new("en");
	private readonly DateTime currentDateTimeNow = DateTime.Now;

	public ICollection<TvChannel> GetChannels()
	{
		return Enumerable.Range(1, 30).Select(i =>
			new TvChannel
			{
				Id = _random.UShort(),
				Name = _lorem.Word(),
				Region = _lorem.Word(),
				Language = _lorem.Word(),
				ShortDescription = _lorem.Paragraph(),
				Url = _internet.Url(),
				IconImg = _internet.Url(),
				IsPaid = _random.Bool()
			}).ToList();
	}

	public ICollection<TvShow> GetTvShows(int channelId)
	{
		return Enumerable.Range(1, 10).Select(i =>
			new TvShow
			{
				Id = _random.UShort(),
				ChannelId = channelId,
				Name = _lorem.Word(),
				ShortDescription = _lorem.Paragraph(),
				Genres = _lorem.Words(_random.UShort(1, 4)),
				StartDateTime = currentDateTimeNow.AddMinutes(_random.Short(-30, 120)),
				EndDateTime = currentDateTimeNow.AddMinutes(120).AddMinutes(_random.Short(-30, 120)),

			}).ToList();
	}

	public TvShowDetail GetTvShowDetail(int channelId, int tvShowId) =>
		new()
		{
			Id = _random.UShort(),
			ChannelId = channelId,
			ChannelName = _lorem.Word(),
			ShowId = tvShowId,
			ShowName = _lorem.Word(),
			ShortDescription = _lorem.Paragraph(),
			Description = _lorem.Sentences(),
			Genres = _lorem.Words(_random.UShort(1, 4)),
			Url = _internet.Url(),
			StartDateTime = currentDateTimeNow.AddMinutes(_random.Short(-30, 120)),
			EndDateTime = currentDateTimeNow.AddMinutes(120).AddMinutes(_random.Short(-30, 120))
		};

	public ICollection<SearchResult> Search(string searchTerm)
	{
		return Enumerable.Range(1, 10).Select(i =>
			new SearchResult
			{
				ChannelId = _random.UShort(),
				ChannelName = _lorem.Word(),
				ShowId = _random.UShort(),
				ShowName = _lorem.Word(),
				ShortDescription = _lorem.Paragraph(),
				Url = _internet.Url(),
				StartDateTime = currentDateTimeNow.AddMinutes(_random.Short(-30, 120)),
				EndDateTime = currentDateTimeNow.AddMinutes(120).AddMinutes(_random.Short(-30, 120))
			}).ToList();
	}
}