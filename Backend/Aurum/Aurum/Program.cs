using Aurum.Repositories.Income.Income;
using Aurum.Repositories.Income.IncomeCategory;
using Aurum.Repositories.Income.RegularIncome;
using Aurum.Models.CustomJsonConverter;
using Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;
using Aurum.Repositories.ExpenseCategoryRepository;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Repositories.RegularExpenseRepository;
using Aurum.Services.AccountService;
using Aurum.Services.BalanceService;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;
using Aurum.Services.Income;
using Aurum.Services.RegularExpenseService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options => 
	{ 
		options.JsonSerializerOptions.Converters.Add(new CaseInsensitiveEnumConverter<Regularity>()); 
	});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
builder.Services.AddScoped<IRegularIncomeRepo, RegularIncomeRepo>();
builder.Services.AddScoped<IIncomeCategoryRepo, IncomeCategoryRepo>();
builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IRegularExpenseRepository, RegularExpenseRepository>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IRegularExpenseService, RegularExpenseService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontEnd",
		policy =>
		{
			policy.WithOrigins("http://localhost:3000")
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("AllowFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();