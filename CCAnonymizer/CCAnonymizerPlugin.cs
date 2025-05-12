using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace CCAnonymizer;

public sealed class CCAnonymizerPlugin : IDalamudPlugin
{

    private const string CommandName = "/panon";

    public readonly WindowSystem WindowSystem = new("CCAnon");
    public CCAnonymizerPlugin(IDalamudPluginInterface pluginInterface,
        ICommandManager commandManager)
    {
        PluginServices.Initialize(pluginInterface);
        pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;
        pluginInterface.UiBuilder.Draw += DrawUI;

    }

    private void ToggleConfigUI()
    {
    }

    private void DrawUI()
    {
        WindowSystem.Draw();
    }


    private void OnCommand(string command, string arguments)
    {
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();


    }
}