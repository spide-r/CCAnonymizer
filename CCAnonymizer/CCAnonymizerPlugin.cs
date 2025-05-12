using CCAnonymizer.Windows;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace CCAnonymizer;

public sealed class CCAnonymizerPlugin : IDalamudPlugin
{
    private readonly WindowSystem _windowSystem = new("CCAnonymizer");
    private ConfigWindow ConfigWindow { get; init; }
    private ICommandManager CommandManager { get; init; }
    private IDalamudPluginInterface PluginInterface { get; init; }


    public CCAnonymizerPlugin(IDalamudPluginInterface pluginInterface,
        ICommandManager commandManager)
    {
        PluginServices.Initialize(pluginInterface);
        CommandManager = commandManager;
        PluginInterface = pluginInterface;

        pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUi;
        pluginInterface.UiBuilder.Draw += DrawUi;
        CommandManager.AddHandler("/panon", new CommandInfo(OnCommand)
        {
            HelpMessage = "Open Config"
        });
        ConfigWindow = new ConfigWindow(this);
        _windowSystem.AddWindow(ConfigWindow);
    }

    private void ToggleConfigUi()
    {
        ConfigWindow.Toggle();
    }

    private void DrawUi()
    {
        _windowSystem.Draw();
    }


    private void OnCommand(string command, string arguments)
    {
        ToggleConfigUi();
    }

    public void Dispose()
    {
        _windowSystem.RemoveAllWindows();

        ConfigWindow.Dispose();

        CommandManager.RemoveHandler("/panon");
        PluginInterface.UiBuilder.OpenConfigUi -= ToggleConfigUi;

    }
}