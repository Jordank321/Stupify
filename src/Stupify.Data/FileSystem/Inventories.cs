﻿using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicTacZap;
using TicTacZap.Blocks;

namespace Stupify.Data.FileSystem
{
    internal class Inventories
    {
        private const string InventoryExtension = ".INV";
        private readonly string _inventoriesPath;

        public Inventories(string dataDirectory)
        {
            _inventoriesPath = dataDirectory + @"\Inventories";
            Directory.CreateDirectory(_inventoriesPath);
        }

        public async Task<Inventory> GetInventoryAsync(int userId)
        {
            if (File.Exists(_inventoriesPath + $@"\{userId + InventoryExtension}"))
            {
                using (var stream = File.OpenText(_inventoriesPath + $@"\{userId + InventoryExtension}"))
                {
                    var fileText = await stream.ReadToEndAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<Inventory>(fileText);
                }
            }

            var inventory = new Inventory(5);
            await SaveInventoryAsync(userId, inventory).ConfigureAwait(false);
            return inventory;
        }

        public async Task SaveInventoryAsync(int userId, Inventory inventory)
        {
            var fileText = JsonConvert.SerializeObject(inventory);
            using (var stream = File.CreateText(_inventoriesPath + $@"\{userId + InventoryExtension}"))
            {
                await stream.WriteAsync(fileText).ConfigureAwait(false);
            }
        }

        public async Task AddToInventoryAsync(BlockType blockType, int quantity, int userId)
        {
            var inventory = await GetInventoryAsync(userId).ConfigureAwait(false);
            inventory.AddBlocks(blockType, quantity);
            await SaveInventoryAsync(userId, inventory).ConfigureAwait(false);
        }

        public async Task<bool> RemoveFromInventoryAsync(BlockType blockType, int quantity, int userId)
        {
            var inventory = await GetInventoryAsync(userId).ConfigureAwait(false);
            if (!inventory.RemoveBlocks(blockType, quantity)) return false;
            await SaveInventoryAsync(userId, inventory).ConfigureAwait(false);
            return true;
        }

        public async Task ResetInventoryAsync(int userId)
        {
            var inventory = await GetInventoryAsync(userId).ConfigureAwait(false);
            inventory.Reset();
            await SaveInventoryAsync(userId, inventory).ConfigureAwait(false);
        }
    }
}