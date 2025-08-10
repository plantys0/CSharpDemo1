using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAA
{
    public class b02_abstractFactory
    {

        // Abstract Products (Interfaces)
        public interface IButton { void Paint(); }
        public interface ICheckbox { void Paint(); }

        // Concrete Products (Windows Family)
        public class WindowsButton : IButton { public void Paint() => Console.WriteLine("Windows Button"); }
        public class WindowsCheckbox : ICheckbox { public void Paint() => Console.WriteLine("Windows Checkbox"); }

        // Concrete Products (Mac Family)
        public class MacButton : IButton { public void Paint() => Console.WriteLine("Mac Button"); }
        public class MacCheckbox : ICheckbox { public void Paint() => Console.WriteLine("Mac Checkbox"); }

        // Abstract Factory (Interface for creating families)
        public interface IGUIFactory
        {
            IButton CreateButton();
            ICheckbox CreateCheckbox();
        }

        // Concrete Factories (One per family)
        public class WindowsFactory : IGUIFactory
        {
            public IButton CreateButton() => new WindowsButton();
            public ICheckbox CreateCheckbox() => new WindowsCheckbox();
        }

        public class MacFactory : IGUIFactory
        {
            public IButton CreateButton() => new MacButton();
            public ICheckbox CreateCheckbox() => new MacCheckbox();
        }

        // Client Code (Uses factory without knowing concretes)
        public class Application
        {
            private readonly IGUIFactory _factory;
            public Application(IGUIFactory factory) { _factory = factory; }

            public void CreateUI()
            {
                var button = _factory.CreateButton();
                var checkbox = _factory.CreateCheckbox();
                button.Paint();
                checkbox.Paint();
            }
        }

        static void Main(string[] args)
        {
            // Windows family
            var winApp = new Application(new WindowsFactory());
            Console.WriteLine("Windows UI:");
            winApp.CreateUI();

            Console.WriteLine();

            // Mac family
            var macApp = new Application(new MacFactory());
            Console.WriteLine("Mac UI:");
            macApp.CreateUI();

            Console.ReadKey();
        }

    }


}
