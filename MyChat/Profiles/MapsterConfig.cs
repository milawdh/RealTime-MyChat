using Domain.Entities;
using DomainShared.Dtos;
using DomainShared.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Profiles
{
	public static class MapsterConfig
	{
		public static void RegisterMapsterConfiguration(this IServiceCollection services)
		{
			var handlers = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => typeof(IHasCustomMap).IsAssignableFrom(p) && p.IsClass);

			foreach (var handler in handlers)
			{
				var handlerInstance = (IHasCustomMap)Activator.CreateInstance(handler);
				handlerInstance.ConfigMap();
			}


			TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
		}
	}
}
