using System;
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

			//This works (of course), but I need to call this by reflection. I don't know the type at design time.
			//var r = mediator.Send(request);

			//I tried this, but it doesn't work
			var type = request.GetType();
			var method = mediator.GetType().GetMethod("Send");
			var generic = method.MakeGenericMethod(type);
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
