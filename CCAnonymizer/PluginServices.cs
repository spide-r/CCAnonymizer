using CCAnonymizer.Impl;
using CCAnonymizer.Impl.Maskers;
using CCAnonymizer.Interfaces;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace CCAnonymizer;

public class PluginServices
{
    [PluginService]
    internal static IChatGui ChatGui { get; private set; }
    
    [PluginService]
    internal static IGameGui GameGui { get; private set; }
    
    [PluginService]
    internal static IPluginLog PluginLog { get; private set; }

    [PluginService]
    internal static IObjectTable ObjectTable { get; private set; }

    [PluginService]
    internal static IClientState ClientState { get; private set; }
    
    [PluginService]
    internal static IFramework Framework { get; private set; }
    
    [PluginService]
    internal static IGameInteropProvider GameInteropProvider { get; private set; }
    
    [PluginService]
    internal static INamePlateGui NamePlateGui { get; private set; }
    
    [PluginService]
    internal static IAddonEventManager AddonEventManager { get; private set; }
    
    [PluginService]
    internal static IAddonLifecycle AddonLifecycle { get; private set; }
    
    [PluginService]
    internal static ICondition Condition { get; private set; }
    
    internal static Configuration Config { get; private set; }
    
    internal static IMaskerManager MaskerManager { get; private set; }
    
    internal static IMatchManager MatchManager { get; private set; }

    
    internal static void Initialize(IDalamudPluginInterface pluginInterface)
    {
        pluginInterface.Create<PluginServices>();
        Config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        MaskerManager = new MaskerManager();
        MatchManager = new MatchManager();

    }
    
}