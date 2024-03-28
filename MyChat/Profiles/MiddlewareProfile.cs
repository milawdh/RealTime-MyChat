using ElmahCore.Mvc;

namespace MyChat.Profiles
{
	public static class MiddlewareProfile
	{
		public static IApplicationBuilder UseMiddlewareProfile(this IApplicationBuilder app)
		{
			//In Development
			if (WebApplication.Create().Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}


			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
            app.UseAuthentication();
			app.UseAuthorization();

            app.UseElmah();

			return app;
		}
	}
}
