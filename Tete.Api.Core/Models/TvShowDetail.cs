namespace Tete.Api.Core.Models;

/// <summary>
/// Детальная информация о передаче
/// </summary>
public class TvShowDetail
{
	public int Id { get; set; }

	public int ChannelId { get; set; }

	public string ChannelName { get; set; }
	
	public int ShowId { get; set; }

	public string ShowName { get; set; }

	public string ShortDescription { get; set; }

	public string Description { get; set; }
	
	public DateTime StartDateTime { get; set; }

	public DateTime EndDateTime { get; set; }
	
	public string[] Genres { get; set; }
	
	public string Url { get; set; }
}