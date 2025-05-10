using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace CCAnonymizer;

public class PluginServices
{
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
    internal static IGameInteropProvider GameInteropProvider { get; private set; }
    
    [PluginService]
    internal static INamePlateGui NamePlateGui { get; private set; }
    
    internal static Configuration Config { get; private set; }

    
    internal static void Initialize(IDalamudPluginInterface pluginInterface)
    {
        Config = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        pluginInterface.Create<PluginServices>();
    }
}