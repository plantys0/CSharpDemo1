using Microsoft.Extensions.DependencyInjection;
namespace _DependencyInjectionIOC
{
    /*Dependency Injection (DI) is a design pattern that helps in achieving loose coupling between classes and their dependencies. Inversion of Control (IOC) is a principle where the control of creating and managing objects is inverted and delegated to a container or a framework.
In C#, you can use the built-in Microsoft.Extensions.DependencyInjection to manage dependencies. It is a container for registering and configuring services in your application. By using ServiceCollection, you can define the dependencies for your application and their lifetimes (singleton, scoped, or transient).
    
    It provides three methods for registering services:
                AddSingleton: Creates a single instance for the entire application lifetime.
                AddScoped: Creates a new instance for each request or scope.
                AddTransient: Creates a new instance every time the service is requested.
Application with work without DI, DI provides folloiwng benefits:
                Loose coupling: Promotes dependency on abstractions, not concrete implementations.
                Easier testing: Facilitates using mock objects or test doubles.
                Centralized lifetime management: Automatically manages service instances and lifetimes.
                Better code organization: Provides clear overview of dependencies and their relationships.
In this example, we have two car classes (Toyota and Ford) implementing the ICar interface and two engine classes (V6Engine and V8Engine) implementing the IEngine interface. The Driver class has dependencies on ICar and IEngine.
We register the dependencies using AddSingleton, AddScoped, and AddTransient as appropriate. Then, we create a ServiceProvider and use it to resolve dependencies and create instances of the Driver class in different scopes.
When you run the code, you will see the following output:
    - Ford with V8Engine
    - Ford with V8Engine
    - New scope:
    - Ford with V8Engine
This output demonstrates the behavior of the three registration methods. The car dependency (Ford) is resolved as a scoped service, so it's the same instance within a scope but different instances in different scopes. The engine dependency (V8Engine) is resolved as a transient service, so a new instance is created every time it's requested, even within the same scope.
    without DI---------------------
           var car = new Toyota(); var engine = new V6Engine(); var driver1 = new Driver(car, engine); var driver2 = new Driver(car, engine);
           Console.WriteLine("New scope:");
           var driver3 = new Driver(car, engine);
*/
    public interface IMessageSender { void SendMessage(string message); }

    public class EmailSender : IMessageSender { public void SendMessage(string message) { Console.WriteLine($"Sending email: {message}"); } }

    public class SMSSender : IMessageSender { public void SendMessage(string message) { Console.WriteLine($"Sending SMS: {message}"); } }

    public class NotificationService
    {
        private readonly IMessageSender _messageSender;
        public NotificationService(IMessageSender messageSender) { _messageSender = messageSender; }
        public void Notify(string message) { _messageSender.SendMessage(message); }
    }

    class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            //AddTransient was chosen because the services in the example are stateless and don't require any shared resources or lifetime management based on a scope. 
            services.AddTransient<IMessageSender, EmailSender>(); // Change to SMSSender for SMS notifications
            services.AddTransient<NotificationService>();

            var provider = services.BuildServiceProvider();
            var notificationService = provider.GetService<NotificationService>();

            notificationService.Notify("Hello, World!");
        }
    }


}