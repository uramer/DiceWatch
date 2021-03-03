using Dice.Dice;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Security;
using Tizen.System;

namespace Dice.Services
{
    public class Storage
    {
        private const string PRIVILIGE = "http://tizen.org/privilege/mediastorage";
        private string Path;
        private bool Initialized;

        public Storage()
        {
            Initialized = false;
            PrivacyPrivilegeManager.ResponseContext context = null;
            if (PrivacyPrivilegeManager.GetResponseContext(PRIVILIGE).TryGetTarget(out context))
            {
                context.ResponseFetched += PrivilegeResponse;
            }
            Initialize();
        }

        private void PrivilegeResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.result == RequestResult.AllowForever)
                Initialize();
        }

        private void Log(string s)
        {
            Logger.Debug("[Storage]" + s);
        }

        private void Initialize()
        {
            if (Initialized)
                return;

            CheckResult result = PrivacyPrivilegeManager.CheckPermission(PRIVILIGE);
            if (result != CheckResult.Allow)
            {
                PrivacyPrivilegeManager.RequestPermission(PRIVILIGE);
                return;
            }

            var storage = StorageManager.Storages
                .Where(s => s.StorageType == StorageArea.Internal)
                .FirstOrDefault();
            Path = storage.GetAbsolutePath(DirectoryType.Documents);
            Path = System.IO.Path.Combine(Path, "dice");
            Directory.CreateDirectory(Path);
            Path = System.IO.Path.Combine(Path, "dice.bin");
            Log(Path);
            Initialized = true;
        }

        private async Task<byte[]> Read()
        {
            if (!Initialized)
                throw new IOException("Storage is not initialized!");
            return await File.ReadAllBytesAsync(Path).ConfigureAwait(false);
        }

        private IList<DiceSet> Deserialize(byte[] input)
        {
            Log($"Parsing {input.Length} bytes!");
            int i = 0;
            var result = new List<DiceSet>();
            while (i < input.Length)
            {
                (var diceSet, var length) = DiceSet.Deserialize(input, i);
                result.Add(diceSet);
                i += length;
            }
            Log($"Loaded {result.Count} sets!");
            return result;
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

        private static byte[] Serialize(IList<DiceSet> sets)
        {
            return sets.SelectMany(s => s.Serialize()).ToArray();
        }

        private async Task Write(byte[] bytes)
        {
            if (!Initialized)
                throw new IOException("Storage is not initialized!");
            Log($"Writing {bytes.Length} bytes!");
            await File.WriteAllBytesAsync(Path, bytes).ConfigureAwait(false);
        }

        public async Task WriteSets(IList<DiceSet> sets)
        {
            Log($"Saving {sets.Count} sets!");
            await Write(Serialize(sets)).ConfigureAwait(false);
        }
    }
}
