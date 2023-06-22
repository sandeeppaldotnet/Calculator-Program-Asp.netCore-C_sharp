var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run(async (HttpContext context) =>
{

    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        int firstNumber = 0, secondNumber = 0;
        string? operaton = null;
        long? result = null;
        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            string firstNumberString = context.Request.Query["firstNumber"][0];
            if (!string.IsNullOrEmpty(firstNumberString))
            {
                firstNumber = Convert.ToInt32(firstNumberString);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'firstnumber'\n");

            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'firstnumber'\n");
            }
        }

        //code for secondNumber input
        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            string secondNumberString = context.Request.Query["secondNumber"][0];
            if (!string.IsNullOrEmpty(secondNumberString))
            {
                secondNumber = Convert.ToInt32(secondNumberString);

            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'secondnumber'\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Invalid input for 'secondnumber'\n");
            }
        }
        //read 'operation'

        if (context.Request.Query.ContainsKey("operation"))
        {
            operaton = Convert.ToString(context.Request.Query["operation"][0]);
            switch (operaton)
            {
                case "add": result = firstNumber + secondNumber; break;
                case "subtract": result = firstNumber - secondNumber; break;
                case "multiply": result = firstNumber * secondNumber; break;
                case "divide": result = (secondNumber != 0) ? firstNumber / secondNumber : 0; break;
                case "mod": result = (secondNumber != 0) ? firstNumber % secondNumber : 0; break;
            }
            //If no case matched above, the "result" remains as 'null'
            if (result.HasValue)
            {
                await context.Response.WriteAsync(result.Value.ToString());
            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'operation'\n");

                }
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'operation'\n");
        }


    }

});

app.Run();
