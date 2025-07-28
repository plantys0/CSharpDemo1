using System;
using System.Diagnostics.Contracts;

//•Delegate: A delegate is like a contract that defines a method’s signature (parameters and return type). It acts as a pointer to methods that match this signature, allowing them to be called indirectly.
//•Event: In C#, an event is a special kind of delegate that supports the publisher-subscriber pattern. It allows multiple methods to be "subscribed" to it, and when the event is "raised" by the publisher, all subscribed methods are called.
//•Subscription (+=): This adds a method to the event’s list of handlers. It’s like signing up for a newsletter—your method gets added to the list of recipients.
//•Invocation: When the publisher raises the event (using Invoke), all subscribed methods are called with the specified arguments.

public class TemperatureChangedEventArgs : EventArgs  // Defines a new class that inherits from EventArgs (built-in base for event data).
{
    public double Temperature { get; }  // A read-only property to hold the temperature value (double for decimals like 25.5°C).
    public TemperatureChangedEventArgs(double temp) => Temperature = temp;  // Constructor: Called when creating the object, sets the Temperature property.
}

public class WeatherStation  // Defines the publisher class.
{
    public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;  // ###1  a built-in delegate type that expects methods with the signature void (object, TemperatureChangedEventArgs)
    //public event EventHandler<TemperatureChangedEventArgs> IS SHORT FROM FOR  public delegate void EventHandler<TemperatureChangedEventArgs>(object sender, TemperatureChangedEventArgs e);

    private double _temperature;  // Private field to store the current temperature (hidden from outside).
    public double Temperature  // Public property to get/set the temperature.
    {
        get => _temperature;  // Getter: Returns the current value.
        set  // Setter: Called when setting Temperature = newValue.
        {
            _temperature = value;  // Update the internal field.
            // Raise the event: Notify subscribers with data
            TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(value));  // If event has subscribers, call their methods with sender (this) and args (new temp).
        }
    }
}

// Subscriber 1: Mobile App reacts to changes
public class MobileApp
{
    public void OnTemperatureChanged(object sender, TemperatureChangedEventArgs e) //###2 This method matches the signature as per ###1, so it can be subscribed
    {
        Console.WriteLine($"Mobile App: Temperature updated to {e.Temperature}°C. Refreshing display.");
    }
}

// Subscriber 2: Smart Thermostat reacts differently
public class SmartThermostat
{
    public void OnTemperatureChanged(object sender, TemperatureChangedEventArgs e)
    {
        if (e.Temperature > 30)
            Console.WriteLine($"Smart Thermostat: It's hot ({e.Temperature}°C)! Turning on AC.");
        else
            Console.WriteLine($"Smart Thermostat: Temperature is {e.Temperature}°C. No action needed.");
    }
}

class Program
{
    static void Main()
    {
        // Create publisher
        WeatherStation station = new WeatherStation();

        // Create subscribers
        MobileApp app = new MobileApp();
        SmartThermostat thermostat = new SmartThermostat();

        // Subscribe: Add methods to the event
        station.TemperatureChanged += app.OnTemperatureChanged; //###3 links mehod to the event
        station.TemperatureChanged += thermostat.OnTemperatureChanged;

        // Simulate temperature change (publisher broadcasts)
        Console.WriteLine("Setting temperature to 25°C...");
        station.Temperature = 25;

        Console.WriteLine("\nSetting temperature to 32°C...");
        station.Temperature = 32;

        // Unsubscribe one subscriber
        station.TemperatureChanged -= thermostat.OnTemperatureChanged;
        Console.WriteLine("\nUnsubscribed thermostat. Setting to 28°C...");
        station.Temperature = 28;
    }
}