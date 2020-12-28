using Dice.Dice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Security;
using Tizen.System;

namespace Dice.Services
{
  public class StorageService
  {
    private const string privilege = "http://tizen.org/privilege/mediastorage";
    private string path;
    private bool initialized;

    public StorageService()
    {
      initialized = false;
      PrivacyPrivilegeManager.ResponseContext context = null;
      if (PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context))
      {
        context.ResponseFetched += PrivilegeResponse; ;
      }
      Initialize();
    }

    private void PrivilegeResponse(object sender, RequestResponseEventArgs e)
    {
      if(e.result == RequestResult.AllowForever)
        Initialize();
    }

    private void Log(string s)
    {
      Logger.Info("[Storage]" + s);
    }

    private void Initialize()
    {
      CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
      if (result != CheckResult.Allow) {
        PrivacyPrivilegeManager.RequestPermission(privilege);
        return;
      }
      var storage = StorageManager.Storages
        .Where(s => s.StorageType == StorageArea.Internal)
        .FirstOrDefault();
      path = storage.GetAbsolutePath(DirectoryType.Documents);
      path = Path.Combine(path, "dice");
      Directory.CreateDirectory(path);
      path = Path.Combine(path, "dice.bin");
      Log(path);
      initialized = true;
    }

    private static byte[] Serialize(IList<DiceSet> sets)
    {
      var bytes = new List<byte>();
      foreach(var set in sets)
      {
        bytes.AddRange(set.Serialize());
      }
      return bytes.ToArray();
    }

    private IList<DiceSet> Deserialize(byte[] input)
    {
      Log($"Parsing {input.Length} bytes!");
      int i = 0;
      var result = new List<DiceSet>();
      while(i < input.Length)
      {
        int length;
        result.Add(DiceSet.Deserialize(input, out length, i));
        i += length;
      }
      Log($"Loaded {result.Count} sets!");
      return result;
    }

    private async Task Write(byte[] bytes)
    {
      if (!initialized) throw new IOException("Storage is not initialized!");
      Log($"Writing {bytes.Length} bytes!");
      await File.WriteAllBytesAsync(path, bytes).ConfigureAwait(false);
    }

    private async Task<byte[]> Read()
    {
      if (!initialized) throw new IOException("Storage is not initialized!");
      return await File.ReadAllBytesAsync(path).ConfigureAwait(false);
    }

    public async Task WriteSets(IList<DiceSet> sets)
    {
      Log($"Saving {sets.Count} sets!");
      await Write(Serialize(sets)).ConfigureAwait(false);
    }

    public async Task<IList<DiceSet>> ReadSets()
    {
      try
      {
        return Deserialize(await Read());
      }
      catch (IOException)
      {
        return new List<DiceSet>();
      }
    }
  }
}
