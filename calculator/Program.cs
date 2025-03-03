var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();



app.Run(async (HttpContext context) =>
{

		if (context.Request.Method == "GET")
		{
				double firstNumber = 0;
				double secondNumber = 0;
				try
				{
						if (context.Request.Query.ContainsKey(("firstNumber")))
						{
								firstNumber = double.Parse(context.Request.Query["firstNumber"][0]);
						}
						if (context.Request.Query.ContainsKey(("secondNumber")))
						{
								secondNumber = double.Parse(context.Request.Query["secondNumber"][0]);
						}
						if (context.Request.Query.ContainsKey(("operation")))
						{
								string op = (context.Request.Query["operation"][0]);
								if (op == "add")
								{
										await context.Response.WriteAsync((firstNumber + secondNumber).ToString());
								}
								else if (op == "sub")
								{
										await context.Response.WriteAsync((firstNumber - secondNumber).ToString());
								}
								else if (op == "mul")
								{
										await context.Response.WriteAsync((firstNumber * secondNumber).ToString());
								}
								else if (op == "div")
								{
										await context.Response.WriteAsync((firstNumber / secondNumber).ToString());
								}
								else if (op == "mod")
								{
										await context.Response.WriteAsync((firstNumber % secondNumber).ToString());
								}
								else
								{

										await context.Response.WriteAsync("Invalid Input!");
								}
						}
				}
				catch (Exception e)
				{
						await context.Response.WriteAsync("Invalid Input!");
				}

		}
});
app.Run();
