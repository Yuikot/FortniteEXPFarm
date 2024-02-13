
using System;
using System.ComponentModel;
using System.Threading;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;

class Program
{

    static void Main()
    {
        TimeSpan duration = TimeSpan.FromMilliseconds(1500);
        DateTime startTime = DateTime.Now;
        TimeSpan loopDuration = TimeSpan.FromMinutes(20);
        int timer = 3;

        Thread threadAD = new Thread(() =>
        {
            while (DateTime.Now - startTime < loopDuration)
            {
                PressAndHoldKey(ConsoleKey.A, duration);
                PressAndHoldKey(ConsoleKey.D, duration);
            }
        });
        threadAD.Start();

        while (DateTime.Now - startTime < loopDuration)
        {
            PressKey(ConsoleKey.E, TimeSpan.FromMilliseconds(500));
            if ((DateTime.Now - startTime).TotalMinutes % timer == 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
        threadAD.Join();
    }

    static void PressAndHoldKey(ConsoleKey key, TimeSpan duration)
    {
        var simulator = new InputSimulator();
        simulator.Keyboard.KeyDown((VirtualKeyCode)key);
        Thread.Sleep(duration);
        simulator.Keyboard.KeyUp((VirtualKeyCode)key);
    }

    static void PressKey(ConsoleKey key, TimeSpan duration)
    {
        var simulator = new InputSimulator();
        simulator.Keyboard.KeyPress((VirtualKeyCode)key);
        Thread.Sleep(duration);
    }
}