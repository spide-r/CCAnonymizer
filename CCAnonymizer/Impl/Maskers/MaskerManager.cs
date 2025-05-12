using System;
using CCAnonymizer.Interfaces;
using Dalamud.Plugin;

namespace CCAnonymizer.Impl.Maskers;

public class MaskerManager(IDalamudPluginInterface pluginInterface) : IMaskerManager
{
    private readonly AppearanceMasker _appearance = new(pluginInterface);
    private readonly NamePlateMasker _namePlate = new();
    private readonly PortraitMasker _portrait = new();
    private readonly TargetMasker _target = new();
    private readonly ChatMasker _chat = new();
    private readonly BattleLogMasker _battleLog = new();
    private readonly PlayerListMasker _playerList = new();
    private readonly ScoreboardMasker _scoreboard = new();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _appearance.Dispose();
        _namePlate.Dispose();
        _portrait.Dispose();
        _target.Dispose();
        _chat.Dispose();
        _battleLog.Dispose();
        _playerList.Dispose();
        _scoreboard.Dispose();
    }
}