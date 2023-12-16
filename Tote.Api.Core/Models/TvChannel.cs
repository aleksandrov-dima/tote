namespace Tote.Api.Core.Models;

/// <summary>
/// Канал
/// </summary>
public class TvChannel
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string Region { get; set; }

	public string Language { get; set; }

	public string ShortDescription { get; set; }

	public string Url { get; set; }

	public string IconImg { get; set; }

	public bool IsPaid { get; set; }
}