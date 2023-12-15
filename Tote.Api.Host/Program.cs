using Microsoft.AspNetCore.Mvc;
using Tete.Api.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//LogicServices
builder.Services.AddSingleton<ITvProgrammLogic, TvProgrammLogicMock>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var tvLogic = app.Services.GetRequiredService<ITvProgrammLogic>();

//список каналов
app.MapGet("/tvchanels", () => tvLogic.GetChannels())
	.WithName("GetTvChannels")
	.WithOpenApi();

//список передач
app.MapGet("/{idchanel}/tvshows", ([FromRoute] int idchanel) => tvLogic.GetTvShows(idchanel))
	.WithName("GetTvShows")
	.WithOpenApi();

//описание передачи
app.MapGet("/{idchanel}/{idshow}/tvshowdetail", ([FromRoute] int idchanel, int idshow) 
		=> tvLogic.GetTvShowDetail(idchanel, idshow))
	.WithName("GetTvShowDetail")
	.WithOpenApi();

//поиск каналов и передач
app.MapGet("/search", ([FromQuery] string searchTerm) => tvLogic.Search(searchTerm))
	.WithName("Search")
	.WithOpenApi();

app.Run();