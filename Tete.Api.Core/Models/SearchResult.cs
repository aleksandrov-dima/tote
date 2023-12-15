namespace Tete.Api.Core.Models;

/// <summary>
/// Результат поиска
/// </summary>
public class SearchResult
{
	public int ChannelId { get; set; }

	public string ChannelName { get; set; }
	
	public int ShowId { get; set; }

	public string ShowName { get; set; }

	public string ShortDescription { get; set; }
	
	public DateTime StartDateTime { get; set; }

	public DateTime EndDateTime { get; set; }
	
	public string Url { get; set; }
}