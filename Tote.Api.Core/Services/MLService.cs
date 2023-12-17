using Microsoft.ML;
using Microsoft.ML.Trainers;
using Tote.Api.Core.Models.ML;

namespace Tote.Api.Core.Services;

public class MLService
{
	/// <summary>
	/// Запуск процесса обучения модели, оценки и тестирования, сохранения обученной модели
	/// </summary>
	public void RunMlProcess()
	{
		MLContext mlContext = new MLContext();

		// Load data
		(IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);

		// Build & train model
		ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

		// Evaluate quality of model
		EvaluateModel(mlContext, testDataView, model);

		// Use model to try a single prediction (one row of data)
		UseModelForSinglePrediction(mlContext, model);

		// Save model
		SaveModel(mlContext, trainingDataView.Schema, model);
	}
	
	private (IDataView training, IDataView test) LoadData(MLContext mlContext)
	{
		var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-train.csv");
		var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-test.csv");

		//Загружаем данные из CSV файлов (пока что, в будущем из БД) и получаем наборы данных Train и Test
		IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ShowRating>(trainingDataPath, hasHeader: true, separatorChar: ',');
		IDataView testDataView = mlContext.Data.LoadFromTextFile<ShowRating>(testDataPath, hasHeader: true, separatorChar: ',');

		return (trainingDataView, testDataView);
	}
	
	/// <summary>
	/// Создание и обучение модели
	/// </summary>
	public static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
	{
		// Преобразование данных
		IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: "userId")
													.Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "showIdEncoded", inputColumnName: "mshowId"));

		// Настройка и выбор алгоритма
		var options = new MatrixFactorizationTrainer.Options
		{
			MatrixColumnIndexColumnName = "userIdEncoded",
			MatrixRowIndexColumnName = "showIdEncoded",
			LabelColumnName = "Label",
			NumberOfIterations = 20,
			ApproximationRank = 100
		};

		var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

		//Обучите модель на основе данных Train и получить обученную модель
		Console.WriteLine("=============== Training the model ===============");
		ITransformer model = trainerEstimator.Fit(trainingDataView);

		return model;
	}
	
	/// <summary>
	/// Оценка модели
	/// </summary>
    public static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
    {
        // EПреобразование тестовых данных
		//Transform делает прогнозы для нескольких входных строк тестового набора данных.
        Console.WriteLine("=============== Evaluating the model ===============");
        var prediction = model.Transform(testDataView);

		//оценка модели
		//сравнивая спрогнозированные значения с фактическими метками (Labels) в тестовом наборе данных, а затем возвращает метрики эффективности модели
        var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

        Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
        Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
    }

	/// <summary>
	/// использование модели для прогнозирования на основе новых данных.
	/// </summary>
    public static void UseModelForSinglePrediction(MLContext mlContext, ITransformer model)
    {
		//Прогнозирование
        Console.WriteLine("=============== Making a prediction ===============");
        var predictionEngine = mlContext.Model.CreatePredictionEngine<ShowRating, ShowRatingPrediction>(model);

        // Create test input & make single prediction
        var testInput = new ShowRating { userId = 6, showId = 10 };

        var movieRatingPrediction = predictionEngine.Predict(testInput);

        if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
        {
            Console.WriteLine("Movie " + testInput.showId + " is recommended for user " + testInput.userId);
        }
        else
        {
            Console.WriteLine("Movie " + testInput.showId + " is not recommended for user " + testInput.userId);
        }
    }

    /// <summary>
    /// Сохранение модели чтобы  использовать для прогнозирования
    /// </summary>
    /// <param name="mlContext"></param>
    /// <param name="trainingDataViewSchema"></param>
    /// <param name="model"></param>
    public static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
    {
        // Save the trained model to .zip file
        var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "ShowRecommenderModel.zip");

        Console.WriteLine("=============== Saving the model to a file ===============");
        mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
    }
}