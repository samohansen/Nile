using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nile.Infrastructure
{
    public static class SessionExtension
    {
		// turn object into JSON for storage in a session
		public static void SetJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}
		// turn JSON back into an object
		public static T GetJson<T>(this ISession session, string key)
		{
			var sessionData = session.GetString(key);

			return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
		}
	}
}
