using System;
using CCAnonymizer.Impl.Maskers;
using CCAnonymizer.Interfaces;

namespace CCAnonymizer.Impl.Maskers;

public class MaskerManager: IMaskerManager
{
    private readonly AppearanceMasker _appearance = new();
    private readonly NamePlateMasker _namePlate = new();
    private readonly PortraitMasker _portrait = new();
    private readonly TargetMasker _target = new();
    private readonly ChatMasker _chat = new();
    private readonly BattleLogMasker _battleLog = new();
    private readonly PlayerListMasker _playerList = new();
    
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
    }
}