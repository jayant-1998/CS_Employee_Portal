using Employee_Portal.DAL.DBContexts;
using Employee_Portal.DAL.Repositories.Implementations;
using Employee_Portal.DAL.Repositories.Interfaces;
using Employee_Portal.Services.Implementations;
using Employee_Portal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer("name=DefaultConnection"));


//this will authentication for users
//builder.Services.AddAuthentication(t =>
//{
//    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    t.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

//})
//    .AddJwtBearer(option =>
//    {
//        option.SaveToken = true;
//        option.RequireHttpsMetadata = false;
//        option.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidIssuer = builder.Configuration["Jwt:ValidAudience"],
//            ValidAudience = builder.Configuration["Jwt:ValidIssuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
//        };
//    });

builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//this is for taking jwt token from the user
//builder.Services.AddSwaggerGen(c => {
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "JWTToken_Auth_API",
//        Version = "v1"
//    });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//        {
//            new OpenApiSecurityScheme {
//                Reference = new OpenApiReference {
//                    Type = ReferenceType.SecurityScheme,
//                        Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
