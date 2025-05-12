using CCAnonymizer.Impl;
using CCAnonymizer.Impl.Maskers;
using CCAnonymizer.Interfaces;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace CCAnonymizer;

public class PluginServices
{
#pragma warning disable CS8618 

    [PluginService]
    internal static IChatGui ChatGui { get; private set; }
    
    [PluginService]
    internal static IPluginLog PluginLog { get; private set; }

    [PluginService]
    internal static IObjectTable ObjectTable { get; private set; }

    [PluginService]
    internal static IClientState ClientState { get; private set; }
    
    [PluginService]
    internal static IFramework Framework { get; private set; }
    [PluginService]
    internal static INamePlateGui NamePlateGui { get; private set; }
    
    [PluginService]
    internal static IAddonLifecycle AddonLifecycle { get; private set; }
    
    internal static Configuration Config { get; private set; }
    
    internal static IMaskerManager MaskerManager { get; private set; }
    
    internal static IMatchManager MatchManager { get; private set; }
#pragma warning restore CS8618 


    
    internal static void Initialize(IDalamudPluginInterface pluginInterface)
    {
        pluginInterface.Create<PluginServices>();
        Config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        MaskerManager = new MaskerManager(pluginInterface);
        MatchManager = new MatchManager();

    }
    
}