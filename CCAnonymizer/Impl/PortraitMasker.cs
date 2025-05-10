using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;

namespace CCAnonymizer.Impl;

public class PortraitMasker: GenericMasker
{
    public PortraitMasker()
    {

        unsafe
        {
            var h = PluginServices.GameInteropProvider.HookFromAddress<CharaView.Delegates.GetCharacter>(CharaView.MemberFunctionPointers.GetCharacter, GetCharacterDetour);
            var h2 = PluginServices.GameInteropProvider.HookFromAddress<CharaView.Delegates.SetModelData>(CharaView.MemberFunctionPointers.SetModelData, SetModelData);
            var h3 = PluginServices.GameInteropProvider.HookFromAddress<CharaView.Delegates.SetItemSlotData>(CharaView.MemberFunctionPointers.SetItemSlotData, SetItemSlotData);
            h.Enable();
            h2.Enable();
            h3.Enable(); //todo: dispose
            /*CharaView.Delegates.SetModelData.
            CharaView.Addresses.Render
            CharaView.Addresses.GetCharacter
            CharaView.Addresses.ToggleDrawWeapon
            CharaView.Addresses.SetItemSlotData*/
            
        }
    }

    private unsafe void SetItemSlotData(CharaView* thisptr, byte slotid, uint itemid, byte stain0id, byte stain1id, uint glamouritemid, bool applycompanycrest)
    {
        PluginServices.PluginLog.Info($"{itemid} {glamouritemid} - {slotid}: {stain0id} - {stain1id}");
        thisptr->SetItemSlotData(slotid, itemid, stain0id, stain1id, glamouritemid, applycompanycrest);
    }

    private unsafe void SetModelData(CharaView* thisptr, CharaViewModelData* data)
    {
        PluginServices.PluginLog.Info($"{data->CustomizeData}{data->CustomizeData.Race}");
        thisptr->SetModelData(data);
    }

    private static unsafe Character* GetCharacterDetour(CharaView* thisptr)
    {
        var v = thisptr->GetCharacter();
        //todo: lots to do here
        PluginServices.PluginLog.Info($"N: {v->NameString} - {v->OrnamentData}");
        return thisptr->GetCharacter();
    }

    public override void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }
}