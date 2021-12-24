#r "C:\Program Files\workspacer\workspacer.Shared.dll"

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using workspacer;

public class TallPlusLayoutEngine : ILayoutEngine
{
    private readonly int _numInPrimary;
    private readonly double _primaryPercent;
    private readonly double _primaryPercentIncrement;

    private int _numInPrimaryOffset = 0;
    private double _primaryPercentOffset = 0;

    public TallPlusLayoutEngine() : this(1, .593, .024) { }

    public TallPlusLayoutEngine(int numInPrimary, double primaryPercent, double primaryPercentIncrement)
    {
        _numInPrimary = numInPrimary;
        _primaryPercent = primaryPercent;
        _primaryPercentIncrement = primaryPercentIncrement;
    }

    public string Name => "tall+";

    public IEnumerable<IWindowLocation> CalcLayout(IEnumerable<IWindow> windows, int spaceWidth, int spaceHeight)
    {
        var list = new List<IWindowLocation>();
        var numWindows = windows.Count();

        if (numWindows == 0)
            return list;

        int numInPrimary = Math.Min(GetNumInPrimary(), numWindows);

        const int spaceOffset = 80;
        const int windowSpacing = 15;
        spaceHeight -= spaceOffset;
        int primaryWidth = (int)(spaceWidth * (_primaryPercent + _primaryPercentOffset));
        int primaryHeight = spaceHeight / numInPrimary;
        int height = spaceHeight / Math.Max(numWindows - numInPrimary, 1);

        // if there are more "primary" windows than actual windows,
        // then we want the pane to actually spread the entire width
        // of the working area
        // if (numInPrimary >= numWindows)
        // {
        //     primaryWidth = spaceWidth;
        // }

        int secondaryWidth = spaceWidth - primaryWidth;

        //var windowList = windows.ToList();

        for (var i = 0; i < numWindows; i++)
        {
            //var window = windowList[i];

            if (i < numInPrimary)
            {
                list.Add(new WindowLocation(windowSpacing, (i * (primaryHeight - windowSpacing)) + ((i + 1) * windowSpacing), primaryWidth - (2 * windowSpacing), primaryHeight - windowSpacing, WindowState.Normal));
            }
            else
            {
                list.Add(new WindowLocation(primaryWidth, ((i - numInPrimary) * (height - windowSpacing)) + ((i - numInPrimary + 1) * windowSpacing), secondaryWidth - windowSpacing, height - windowSpacing, WindowState.Normal));
            }

            // if (window.IsFocused)
            // {
            //     _curWindow = window;
            // }
        }
        return list;
    }

    public void ShrinkPrimaryArea()
    {
        _primaryPercentOffset -= _primaryPercentIncrement;
    }

    public void ExpandPrimaryArea()
    {
        _primaryPercentOffset += _primaryPercentIncrement;
    }

    public void ResetPrimaryArea()
    {
        _primaryPercentOffset = 0;
    }

    public void IncrementNumInPrimary()
    {
        _numInPrimaryOffset++;
    }

    public void DecrementNumInPrimary()
    {
        if (GetNumInPrimary() > 1)
        {
            _numInPrimaryOffset--;
        }
    }

    private int GetNumInPrimary()
    {
        return _numInPrimary + _numInPrimaryOffset;
    }
}