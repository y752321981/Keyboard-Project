using Newtonsoft.Json;
using System;
using System.Text;

public static class Codec
{
	
		/// <summary>
		/// 把对象转换为JSON字符串
		/// </summary>
		/// <param name="o">对象</param>
		/// <returns>JSON字符串</returns>
		public static byte[] ToJSON(this object o)
		{
			if (o == null)
			{
				return null;
			}

			string json = JsonConvert.SerializeObject(o);


			return Encoding.UTF8.GetBytes(json);
		}
		/// <summary>
		/// 把Json文本转为实体
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		public static T FromJSON<T>(this byte[] input)
		{
			string o = Encoding.UTF8.GetString(input);
			try
			{
				return JsonConvert.DeserializeObject<T>(o);
			}
			catch (Exception ex)
			{
				return default(T);
			}
		}
	
}
