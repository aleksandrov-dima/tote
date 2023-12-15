namespace Tete.Api.Core.Models;

/// <summary>
/// Передача
/// </summary>
public class TvShow
{
	public int Id { get; set; }

	public int ChannelId { get; set; }

	public string Name { get; set; }

	public string ShortDescription { get; set; }
	
	/// <summary>
	/// Жанры
	/// </summary>
	public string[] Genres { get; set; }

	public DateTime StartDateTime { get; set; }

	public DateTime EndDateTime { get; set; }
}