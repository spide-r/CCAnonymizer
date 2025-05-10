using System;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace CCAnonymizer.Impl;

public class ChatMasker: GenericMasker
{
    public ChatMasker()
    {
        PluginServices.ChatGui.ChatMessage += OnMessage;
    }
    public override void Dispose()
    {
        System.GC.SuppressFinalize(this);
        PluginServices.ChatGui.ChatMessage -= OnMessage;
    }
    
    private void OnMessage(XivChatType type, int timestamp, ref SeString sender, ref SeString message, ref bool isHandled)
    {
        if (!PluginServices.Config.MaskChat)
        {
            return;
        }
        try
        {
            if (!PluginServices.ClientState.IsPvPExcludingDen ||
                type is not (XivChatType.Party or XivChatType.Alliance)) return;
            var job = "";
            foreach (var payload in sender.Payloads)
            {
                if (payload.Type == PayloadType.Player)
                {
                    isHandled = true;
                }
                    
                var isNumber = int.TryParse(payload.ToString(), out _);

                if (payload.Type != PayloadType.Icon || payload.ToString()!.Contains("CrossWorld") ||
                    isNumber) continue;
                var raw = payload.ToString();
                if (raw != null) job = raw.Substring(7);
            }

            if (!isHandled) return;
                
            var chat = new XivChatEntry
            {
                Message = message,
                Name = job,
                Type = type
            };
            PluginServices.ChatGui.Print(chat);
        }
        catch (Exception e)
        {
            PluginServices.PluginLog.Error(e, "Issue in chat masker!");
        }
    }
}