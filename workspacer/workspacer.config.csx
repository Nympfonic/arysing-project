#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"
#load "TallPlusLayoutEngine.cs"
#load "SideLayoutEngine.cs"

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using workspacer;
using workspacer.FocusIndicator;

return new Action<IConfigContext>((IConfigContext context) =>
{
    // Plugins
    context.AddFocusIndicator();

    // Config
    //context.CanMinimizeWindows = true;
    //var actionMenu = context.AddActionMenu();

    // Setup Workspaces
    var sticky = new StickyWorkspaceContainer(context);
    context.WorkspaceContainer = sticky;
    var monitors = context.MonitorContainer.GetAllMonitors();
    sticky.CreateWorkspace(monitors[0], "main", new TallPlusLayoutEngine());
    sticky.CreateWorkspace(monitors[0], "desktop", new FullLayoutEngine());
    sticky.CreateWorkspace(monitors[1], "side", new SideLayoutEngine());
    // Filters
    var router = context.WindowRouter;
    router.AddFilter(w => !w.ProcessName.Contains("Adobe"));
    router.AddFilter(w => !w.ProcessName.Equals("AfterFX"));
    router.AddFilter(w => !w.ProcessName.Contains("Agent"));
    router.AddFilter(w => !w.ProcessName.Equals("AoE2DE_s"));
    router.AddFilter(w => !w.ProcessName.Equals("Aura"));
    router.AddFilter(w => !w.ProcessName.Equals("Back4Blood"));
    router.AddFilter(w => !w.ProcessName.Equals("BatmanAK"));
    router.AddFilter(w => !w.ProcessName.Equals("Battle.net"));
    router.AddFilter(w => !w.ProcessName.Equals("BethesdaNetLauncher"));
    router.AddFilter(w => !w.ProcessName.Equals("bf"));
    router.AddFilter(w => !w.ProcessName.Equals("BF2042"));
    router.AddFilter(w => !w.ProcessName.Equals("bf4"));
    router.AddFilter(w => !w.ProcessName.Equals("BlackDesert64"));
    router.AddFilter(w => !w.ProcessName.Equals("BlackDesertEAC"));
    router.AddFilter(w => !w.ProcessName.Contains("BlackDesertPatcher"));
    router.AddFilter(w => !w.ProcessName.Equals("Blasphemous"));
    router.AddFilter(w => !w.ProcessName.Equals("BloodstainedRotN"));
    router.AddFilter(w => !w.ProcessName.Contains("Bluestacks"));
    router.AddFilter(w => !w.ProcessName.Equals("Bunny"));
    router.AddFilter(w => !w.ProcessName.Equals("Bunny2 English"));
    router.AddFilter(w => !w.ProcessName.Equals("CharaStudio"));
    router.AddFilter(w => !w.ProcessName.Equals("cheatengine-i386"));
    router.AddFilter(w => !w.ProcessName.Equals("cleanmgr"));
    router.AddFilter(w => !w.ProcessName.Equals("CLIPStudioPaint"));
    router.AddFilter(w => !w.ProcessName.Equals("CNC3EP1"));
    router.AddFilter(w => !w.ProcessName.Equals("CTR 1.1"));
    router.AddFilter(w => !w.ProcessName.Equals("Cyberpunk2077"));
    router.AddFilter(w => !w.ProcessName.Equals("DarkSoulsIII"));
    router.AddFilter(w => !w.ProcessName.Equals("deadspace3"));
    router.AddFilter(w => !w.ProcessName.Equals("destiny2"));
    router.AddFilter(w => !w.ProcessName.Equals("Diablo III64"));
    router.AddFilter(w => !w.ProcessName.Equals("DoorKickers"));
    router.AddFilter(w => !w.ProcessName.Equals("DoorKickers2"));
    router.AddFilter(w => !w.ProcessName.Equals("Game-z"));
    router.AddFilter(w => !w.ProcessName.Equals("Gaming"));
    router.AddFilter(w => !w.ProcessName.Equals("GenerateFNISforUsers"));
    router.AddFilter(w => !w.ProcessName.Equals("GenshinImpact"));
    router.AddFilter(w => !w.ProcessName.Contains("GFAlarm"));
    router.AddFilter(w => !w.ProcessName.Equals("GTA5"));
    router.AddFilter(w => !w.ProcessName.Equals("HD-Player"));
    router.AddFilter(w => !w.ProcessName.Equals("hid"));
    router.AddFilter(w => !w.ProcessName.Equals("HoneySelect2"));
    router.AddFilter(w => !w.ProcessName.Equals("InitSetting"));
    router.AddFilter(w => !w.ProcessName.Equals("Intelligent standby list cleaner ISLC"));
    router.AddFilter(w => !w.ProcessName.Equals("iTunes"));
    router.AddFilter(w => !w.ProcessName.Contains("javaw"));
    router.AddFilter(w => !w.ProcessName.Equals("Koikatu"));
    router.AddFilter(w => !w.ProcessName.Equals("LabCompass"));
    router.AddFilter(w => !w.ProcessName.Contains("Launcher"));
    router.AddFilter(w => !w.ProcessName.Contains("launcher"));
    router.AddFilter(w => !w.ProcessName.Contains("MgsGroundZeroes"));
    router.AddFilter(w => !w.ProcessName.Contains("mgsvmgo"));
    router.AddFilter(w => !w.ProcessName.Contains("mgsvtpp"));
    router.AddFilter(w => !w.ProcessName.Equals("ModOrganizer"));
    router.AddFilter(w => !w.ProcessName.Equals("MonsterHunterWorld"));
    router.AddFilter(w => !w.ProcessName.Equals("Nemesis Unlimited Behavior Engine"));
    router.AddFilter(w => !w.ProcessName.Equals("obs64"));
    router.AddFilter(w => !w.ProcessName.Equals("Origin)"));
    router.AddFilter(w => !w.ProcessName.Equals("PathOfExile_x64Steam"));
    router.AddFilter(w => !w.ProcessName.Equals("pcsx2"));
    router.AddFilter(w => !w.ProcessName.Equals("Photoshop"));
    router.AddFilter(w => !w.ProcessName.Equals("qView"));
    router.AddFilter(w => !w.ProcessName.Equals("R6-Extraction"));
    router.AddFilter(w => !w.ProcessName.Equals("RainbowSix"));
    router.AddFilter(w => !w.ProcessName.Equals("Rainmeter"));
    router.AddFilter(w => !w.ProcessName.Equals("re8"));
    router.AddFilter(w => !w.ProcessName.Equals("ReadyOrNot-Win64-Shipping"));
    router.AddFilter(w => !w.ProcessName.Equals("Reflector4"));
    router.AddFilter(w => !w.ProcessName.Equals("RemotePlay"));
    router.AddFilter(w => !w.ProcessName.Equals("rerev2"));
    router.AddFilter(w => !w.ProcessName.Equals("SC2_x64"));
    router.AddFilter(w => !w.ProcessName.Equals("seeds-of-chaos"));
    router.AddFilter(w => !w.ProcessName.Contains("setup"));
    router.AddFilter(w => !w.ProcessName.Equals("SkyrimSE"));
    router.AddFilter(w => !w.ProcessName.Equals("SpeedRunners"));
    router.AddFilter(w => !w.ProcessName.Contains("steam"));
    router.AddFilter(w => !w.ProcessName.Contains("SteamFriendsPatcher"));
    router.AddFilter(w => !w.ProcessName.Contains("Subverse"));
    router.AddFilter(w => !w.ProcessName.Equals("TeamViewer"));
    router.AddFilter(w => !w.ProcessName.Equals("TLSA"));
    router.AddFilter(w => !w.ProcessName.Equals("Unity"));
    router.AddFilter(w => !w.ProcessName.Contains("Update"));
    router.AddFilter(w => !w.ProcessName.Contains("update_notifier"));
    router.AddFilter(w => !w.ProcessName.Equals("voicemeeter"));
    router.AddFilter(w => !w.ProcessName.Equals("WorldOfTanks"));
    router.AddFilter(w => !w.ProcessName.Equals("worldpainter"));
    router.AddFilter(w => !w.Title.Contains("% complete"));
    router.AddFilter(w => !w.Title.Contains("Ableton"));
    router.AddFilter(w => !w.Title.Contains("Apple Software Update"));
    router.AddFilter(w => !w.Title.Contains("DeadByDaylight"));
    router.AddFilter(w => !w.Title.Contains("iCUE"));
    router.AddFilter(w => !w.Title.Contains("Installer"));
    router.AddFilter(w => !w.Title.Contains("Media Player Classic Home Cinema"));
    router.AddFilter(w => !w.Title.Contains("Microsoft Teams Call in progress"));
    router.AddFilter(w => !w.Title.Contains("Midnight Castle Succubus"));
    router.AddFilter(w => !w.Title.Contains("Minecraft Updater"));
    router.AddFilter(w => !w.Title.Contains("NVIDIA Display Driver"));
    router.AddFilter(w => !w.Title.Contains("PAYDAY 2"));
    router.AddFilter(w => !w.Title.Equals("Persona 5 Strikers"));
    router.AddFilter(w => !w.Title.Contains("Reflector 4"));
    router.AddFilter(w => !w.Title.Contains("SanDisk"));
    router.AddFilter(w => !w.Title.Contains("Setup"));
    router.AddFilter(w => !w.Title.Contains("Streamlabs OBS"));
    router.AddFilter(w => !w.Title.Contains("Streaming game from BlueStacks"));
    router.AddFilter(w => !w.Title.Contains("Tom Clancy's The Division"));
    router.AddFilter(w => !w.Title.Contains("Uplay"));
    router.AddFilter(w => !w.Title.Contains("Warframe"));
    router.AddFilter(w => !w.Title.Equals("Warning"));
    router.AddFilter(w => !w.Title.Equals("Windows Security Alert"));
    // Routes
    router.AddRoute(w => w.ProcessName.Contains("Discord") ? sticky["side"] : null);
    router.AddRoute(w => w.Title.Contains("Minecraft Launcher") ? sticky["desktop"] : null);

    // Keybindings
    const KeyModifiers altCtrl = KeyModifiers.LAlt | KeyModifiers.LControl;
    const KeyModifiers altWin = KeyModifiers.LAlt | KeyModifiers.LWin;

    context.Keybinds.UnsubscribeAll();
    context.Keybinds.Subscribe(MouseEvent.LButtonDown, () => context.Workspaces.SwitchFocusedMonitorToMouseLocation());

    context.Keybinds.Subscribe(altCtrl, Keys.T, () => context.Windows.ToggleFocusedWindowTiling());

    context.Keybinds.Subscribe(altCtrl, Keys.D1, () => context.Workspaces.SwitchToWorkspace(0));
    context.Keybinds.Subscribe(altCtrl, Keys.D2, () => context.Workspaces.SwitchToWorkspace(1));
    context.Keybinds.Subscribe(altCtrl, Keys.D3, () => context.Workspaces.SwitchToWorkspace(2));

    context.Keybinds.Subscribe(altCtrl, Keys.Oemcomma, () => context.Workspaces.FocusedWorkspace.IncrementNumberOfPrimaryWindows());
    context.Keybinds.Subscribe(altCtrl, Keys.OemPeriod, () => context.Workspaces.FocusedWorkspace.DecrementNumberOfPrimaryWindows());

    //context.Keybinds.Subscribe(altWin, Keys.L, () => context.Workspaces.MoveFocusedWindowToNextMonitor());
    //context.Keybinds.Subscribe(altWin, Keys.J, () => context.Workspaces.MoveFocusedWindowToPreviousMonitor());
    context.Keybinds.Subscribe(altWin, Keys.D1, () => context.Workspaces.MoveFocusedWindowToWorkspace(0));
    context.Keybinds.Subscribe(altWin, Keys.D2, () => context.Workspaces.MoveFocusedWindowToWorkspace(1));
    context.Keybinds.Subscribe(altWin, Keys.D3, () => context.Workspaces.MoveFocusedWindowToWorkspace(2));

    context.Keybinds.Subscribe(altCtrl, Keys.Q, () => context.Restart());

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
});