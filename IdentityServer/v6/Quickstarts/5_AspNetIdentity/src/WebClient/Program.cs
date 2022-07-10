using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = "Cookies";
			options.DefaultChallengeScheme = "oidc";
		})
		.AddCookie("Cookies")
		.AddOpenIdConnect("oidc", options =>
		{
			options.Authority = "https://localhost:5001";

			options.ClientId = "mvc.code";
			options.ClientSecret = "secret";
			options.ResponseType = "code";

			options.SaveTokens = true;

			options.Scope.Clear();
			options.Scope.Add("openid");
			options.Scope.Add("profile");
			options.Scope.Add("offline_access");
			//options.Scope.Add("api1");

			options.GetClaimsFromUserInfoEndpoint = true;
		})
		.AddJwtBearer("Bearer", options =>
		{
			options.Authority = "https://localhost:5001";
			options.Audience = "urn:lctAdmin";                //表：ApiResources，字段Name
			options.TokenValidationParameters.ValidateAudience = true;
			options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(5);
		});

//builder.Services.AddMvc();
//自定义claim（声明）转换......开始
builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();
//自定义claim（声明）转换......结束
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages().RequireAuthorization();

app.Run();
