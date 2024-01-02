using ParameterBindingIssue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

Task.Run(async () =>
{
	await Task.Delay(1000);
	using var client = new HttpClient();
	var forecasts = new List<WeatherForecast>() { new() };
	var requestUris = new List<string>()
	 {
		  "http://localhost:5030/WeatherForecast/IEnumerable",
		  "http://localhost:5030/WeatherForecast/IReadOnlyList",
		  "http://localhost:5030/WeatherForecast/Array",
		  "http://localhost:5030/WeatherForecast/List",
		  "http://localhost:5030/WeatherForecast/WithAttribute",
	 };

	foreach (var uri in requestUris)
	{
		Console.WriteLine();
		Console.WriteLine($"POSTing to {uri}");
		using var response = await client.PostAsJsonAsync(uri, forecasts);
		if (response.IsSuccessStatusCode)
		{
			Console.WriteLine("List of WeatherForecast POSTed successfully.");
		}
		else
		{
			Console.WriteLine("List of WeatherForecast was empty at controller.");
		}
	}
});

app.Run();
