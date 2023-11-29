﻿using System.Text;

namespace WcConsole;

public class Wc(string input, ICountWriter writer)
{
    public int CountBytes() => Encoding.UTF8.GetByteCount(input);
    public int CountLines() => input.Split(Environment.NewLine).Length;
    public int CountWords() => input.Split(Environment.NewLine)
        .SelectMany(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .Count();
    public int CountChars() => input.Length;

    public void InvokeReadBytes()
    {
        writer.Write(CountBytes());
    }

    public void InvokeReadLines()
    {
        writer.Write(CountLines());
    }

    public void InvokeReadWords()
    {
        writer.Write(CountWords());
    }

    public void InvokeReadChars()
    {
        writer.Write(CountChars());
    }

    public void InvokeAll()
    {
        writer.Write(CountLines());
        writer.Write(CountWords());
        writer.Write(CountBytes());
    }
}
