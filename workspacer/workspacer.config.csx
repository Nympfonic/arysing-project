#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using System.Collections.Generic;
using System.IO;
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

static void doConfig(IConfigContext context)
{
    // Plugins
    context.AddFocusIndicator();
    //var actionMenu = context.AddActionMenu();

    // Custom Keybinds
    var mod = KeyModifiers.Alt;
    // context.Keybinds.Subscribe(mod, Keys.OemSemicolon, () => {
    //     var curWorkspace = context.Workspaces.FocusedWorkspace;
    //     var window = curWorkspace.FocusedWindow;
    //     if (window != null)
    //     {
    //         if (!window.IsMinimized)
    //         {
    //             curWorkspace.RemoveWindow(window); // This will remove it from the workspace list entirely so below statement will not work
    //         }
    //         else
    //         {
    //             curWorkspace.AddWindow(window);
    //         }
    //     }
    // }, "minimize focused window");
    // context.Keybinds.Subscribe(KeyModifiers.Alt, Keys.OemPeriod, () => MinimizeAllButFocusedWindow(), "minimize all but focused window");

    // Setup Workspaces
    var sticky = new StickyWorkspaceContainer(context);
    context.WorkspaceContainer = sticky;
    var monitors = context.MonitorContainer.GetAllMonitors();
    sticky.CreateWorkspace(monitors[0], "main", new TallPlusLayoutEngine());
    sticky.CreateWorkspace(monitors[0], "desktop", new FullLayoutEngine());
    sticky.CreateWorkspace(monitors[1], "side", new SideLayoutEngine());
    // Filters
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("Adobe"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("AfterFX"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("BlackDesert64"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("javaw"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("obs64"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("Photoshop"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("Rainmeter"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("Reflector3"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("RemotePlay"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("setup"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("steam"));
    context.WindowRouter.AddFilter(w => !w.ProcessName.Contains("update_notifier"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Ableton"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Apple Software Update"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Black Desert Online"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("DeadByDaylight"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("iCUE"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Installer"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Media Player Classic Home Cinema"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("METAL GEAR SOLID V"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Midnight Castle Succubus"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Minecraft Updater"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Paranoia2"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("PAYDAY 2"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Reflector 3"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("SanDisk"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Setup"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Streamlabs OBS"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Tom Clancy's The Division"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Uplay"));
    context.WindowRouter.AddFilter(w => !w.Title.Contains("Warframe"));
    // Routes
    context.WindowRouter.AddRoute(w => w.ProcessName.Contains("Discord") ? context.WorkspaceContainer["side"] : null);
    context.WindowRouter.AddRoute(w => w.Title.Contains("Minecraft Launcher") ? context.WorkspaceContainer["desktop"] : null);
}
return new Action<IConfigContext>(doConfig);