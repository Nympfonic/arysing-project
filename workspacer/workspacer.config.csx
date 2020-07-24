#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using workspacer;
using workspacer.Bar;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;

public class TallPlusLayoutEngine : ILayoutEngine
{
    private readonly int _numInPrimary;
    private readonly double _primaryPercent;
    private readonly double _primaryPercentIncrement;

    private int _numInPrimaryOffset = 0;
    private double _primaryPercentOffset = 0;

    //private IWindow _curWindow;

    public TallPlusLayoutEngine() : this(1, .6, .02) { }

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

    // public void MinimizeWindow()
    // {
    //     //_curWindow.ShowMinimized();
    // }

    // public void MinimizeAllButFocusedWindow()
    // { }
}

public class SideLayoutEngine : ILayoutEngine
{
    private IWindow _lastFull;

    public IEnumerable<IWindowLocation> CalcLayout(IEnumerable<IWindow> windows, int spaceWidth, int spaceHeight)
    {
        var list = new List<IWindowLocation>();
        var numWindows = windows.Count();

        if (numWindows == 0)
            return list;

        var windowList = windows.ToList();
        var noFocus = !windowList.Any(w => w.IsFocused);

        for (var i = 0; i < numWindows; i++)
        {
            var window = windowList[i];
            var forceNormal = (noFocus && window == _lastFull) || window.IsFocused || window.Title.Contains("Discord");

            if (forceNormal)
            {
                _lastFull = window;
            }

            list.Add(new WindowLocation(0, 0, spaceWidth, spaceHeight, GetDesiredState(windowList[i], forceNormal)));
        }
        return list;
    }

    public string Name => "side";

    public void ShrinkPrimaryArea() { }
    public void ExpandPrimaryArea() { }
    public void ResetPrimaryArea() { }
    public void IncrementNumInPrimary() { }
    public void DecrementNumInPrimary() { }

    private WindowState GetDesiredState(IWindow window, bool forceNormal = false)
    {
        if (window.IsFocused || forceNormal)
        {
            return WindowState.Maximized;
        }
        else
        {
            return WindowState.Minimized;
        }
    }
}

Action<IConfigContext> doConfig = (context) =>
{
    // Plugins
    context.AddFocusIndicator();
    //var actionMenu = context.AddActionMenu();

    // Custom Keybinds
    // context.Keybinds.Subscribe(KeyModifiers.Alt, Keys.OemSemicolon, () => {
    //         var curWorkspace = context.WorkspaceContainer.FocusedWorkspace;
    //         var curLayout = context.WorkspaceContainer[curWorkspace.LayoutName];
    //         MinimizeWindow();
    //     }, "minimize focused window");
    //context.Keybinds.Subscribe(KeyModifiers.Alt, Keys.OemPeriod, () => MinimizeAllButFocusedWindow(), "minimize all but focused window");
    context.WorkspaceContainer.CreateWorkspace("main", new TallPlusLayoutEngine());
    context.WorkspaceContainer.AssignWorkspaceToMonitor(context.WorkspaceContainer["main"], context.MonitorContainer.GetMonitorAtIndex(0));
    context.WorkspaceContainer.CreateWorkspace("side", new SideLayoutEngine());
    context.WorkspaceContainer.AssignWorkspaceToMonitor(context.WorkspaceContainer["side"], context.MonitorContainer.GetMonitorAtIndex(1));

    // Filters
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Paranoia2"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("Friends List"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("Steam"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("Installer"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("SanDisk"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("Reflector 3"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("Uplay"));
    context.WindowRouter.AddFilter((Window) => !Window.Title.Contains("Tom Clancy's The Division 2"));

    // Routes
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Discord") ? context.WorkspaceContainer["side"] : null);
};
return doConfig;