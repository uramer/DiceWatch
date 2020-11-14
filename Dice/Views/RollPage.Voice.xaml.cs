using Dice.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Tizen.Uix.VoiceControl;

namespace Dice.Views
{
  partial class RollPage: ContentPage
  {
    private void EnableVoice()
    {
      VoiceControlClient.Initialize();
      VoiceControlClient.Prepare();

      VoiceControlClient.RecognitionResult += OnVoiceResult;

      var commandList = new VoiceCommandList();
      var roll = new VoiceCommand
      {
        Command = "roll"
      };
      commandList.Add(roll);

      VoiceControlClient.SetCommandList(commandList, CommandType.Foreground);
    }

    private void OnVoiceResult(object sender, RecognitionResultEventArgs e)
    {
      var command = e.Result.CommandList.Current?.Command;
      if(command == "roll")
      {
        UpdateRoll();
      }
    }

    private void DisableVoice()
    {
      VoiceControlClient.Unprepare();
      VoiceControlClient.Deinitialize();
    }


  }
}
