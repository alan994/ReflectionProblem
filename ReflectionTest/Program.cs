using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReflectionTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var request = new MyRequest();
			IMediator mediator = new Mediator();
						
			var type = request.GetType();
			var method = mediator.GetType().GetMethod("Send");

			var responseType = type.GetInterfaces()
				.Single(i => i.GetGenericTypeDefinition() == typeof(IRequest<>))
				.GetGenericArguments()
				.Single();

			var generic = method.MakeGenericMethod(responseType);
			//Exception
			var response = generic.Invoke(mediator, new object[] { request });

		}
	}

	public interface IRequest<out TResponse>
	{

	}

	public interface IMediator
	{
		TResponse Send<TResponse>(IRequest<TResponse> requests);
	}

	public class MyRequest : IRequest<MyResponse>
	{
	}

	public class MyResponse
	{
	}

	public class Mediator : IMediator
	{
		public TResponse Send<TResponse>(IRequest<TResponse> requests)
		{
			Console.WriteLine("Processing...");
			return default(TResponse);
		}
	}
}
