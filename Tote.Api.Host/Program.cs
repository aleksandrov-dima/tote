using Microsoft.AspNetCore.Mvc;
using Tote.Api.Core.Models;
using Tote.Api.Core.Services;
using Tote.Api.Core.Services.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//LogicServices
builder.Services.AddSingleton<ITvProgramService, TvProgramServiceMock>();
builder.Services.AddSingleton<IRecommendationService, RecommendationServiceMock>();
builder.Services.AddSingleton<IDataRepository, DataRepositoryMock>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var tvProgramService = app.Services.GetRequiredService<ITvProgramService>();
var recommendationService = app.Services.GetRequiredService<IRecommendationService>();
var dataRepository = app.Services.GetRequiredService<IDataRepository>();

//список каналов
app.MapGet("/tvchanels", () => tvProgramService.GetChannels())
	.WithName("GetTvChannels")
	.WithOpenApi();

//список передач
app.MapGet("/{idchanel}/tvshows", ([FromRoute] int idchanel) => tvProgramService.GetTvShows(idchanel))
	.WithName("GetTvShows")
	.WithOpenApi();

//описание передачи
app.MapGet("/{idchanel}/{idshow}/tvshowdetail", ([FromRoute] int idchanel, int idshow) 
		=> tvProgramService.GetTvShowDetail(idchanel, idshow))
	.WithName("GetTvShowDetail")
	.WithOpenApi();

//поиск каналов и передач
app.MapGet("/search", ([FromQuery] string searchTerm) => tvProgramService.Search(searchTerm))
	.WithName("Search")
	.WithOpenApi();

//ПОлучить профиль пользователя
app.MapGet("/{userIdentifier}/userprofile", ([FromRoute] string userIdentifier) 
		=> dataRepository.GetUserProfileAsync(userIdentifier))
	.WithName("GetUserProfile")
	.WithOpenApi();

//Добавление информации о пользователе
app.MapPost("/userprofile", async (UserProfile userProfile) =>
{
	await dataRepository.AddUserProfileAsync(userProfile);

	return Results.Accepted();
}).WithOpenApi();

//Добавление информации о предпочтениях пользователя
app.MapPost("/userpreferences", async (UserPreference userPreference) =>
{
	await dataRepository.AddUserPreferencesAsync(userPreference);

	return Results.Accepted();
}).WithOpenApi();

//Добавление информации о действиях пользователя
app.MapPost("/useractionsinfo", async (UserActionsInfo actionsInfo) =>
{
	await dataRepository.AddUserActionsInfoAsync(actionsInfo);

	return Results.Accepted();
}).WithOpenApi();

//Добавление информации о сессии пользователя
app.MapPost("/usersessioninfo", async (UserSessionInfo sessionInfo) =>
{
	await dataRepository.AddUserSessionInfoAsync(sessionInfo);

	return Results.Accepted();
}).WithOpenApi();

//Добавление информации о поисковом запросе пользователя
app.MapPost("/usersearchinfo", async (UserSearchInfo searchInfo) =>
{
	await dataRepository.AddUserSearchInfoAsync(searchInfo);

	return Results.Accepted();
}).WithOpenApi();


//Получить рекомендуемый список передач на день
app.MapGet("/{userIdentifier}/recommendationshows/today", ([FromRoute] string userIdentifier)  =>
	recommendationService.GetRecommendationShowsByTodayAsync(userIdentifier))
	.WithName("GetRecommendationShowsByToday")
	.WithOpenApi();

//Получить рекомендуемый список передач на неделю
app.MapGet("/{userIdentifier}/recommendationshows/week", ([FromRoute] string userIdentifier)  =>
	recommendationService.GetRecommendationShowsByWeekAsync(userIdentifier))
	.WithName("GetRecommendationShowsByWeek")
	.WithOpenApi();


app.Run();