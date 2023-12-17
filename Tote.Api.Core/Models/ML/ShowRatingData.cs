using Microsoft.ML.Data;

namespace Tote.Api.Core.Models.ML;

/// <summary>
/// Входные данные о рейтинге телепередач
/// </summary>
public class ShowRating
{
	[LoadColumn(0)]
	public float userId;

	[LoadColumn(1)]
	public float showId;

	/// <summary>
	/// Прогнозируемая метка (рейтинг)
	/// </summary>
	[LoadColumn(2)]
	public float Label;
}


/// <summary>
/// результаты прогнозирования 
/// </summary>
public class ShowRatingPrediction
{
	public float Label;

	public float Score;
}