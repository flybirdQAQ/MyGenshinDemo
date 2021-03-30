using Common.Data;
using GameServer.Entities;
using GameServer.Services;
using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Managers
{
    class ItemManager
    {

        public Character Owner;
        public Dictionary<int, Item> Items = new Dictionary<int, Item>();


        public ItemManager(Character character)
        {
            this.Owner = character;

            foreach (var item in character.Data.CharacterItems.Where(x => x.Count > 0))
            {
                this.Items.Add(item.ItemID, new Item(item));
            }
        }
        public bool AddItem(int itemId, int count)
        {
            Item item = null;
            if (this.Items.TryGetValue(itemId, out item))
            {
                item.Add(count);
            }
            else
            {

                TCharacterItem TItem = Owner.Data.CharacterItems.Where(x => x.ItemID == itemId && x.CharacterID == Owner.Id).FirstOrDefault();
                if (TItem == null)
                {
                    TItem = new TCharacterItem()
                    {
                        CharacterID = Owner.Data.ID,
                        Character = Owner.Data,
                        ItemID = itemId,
                        Count = count
                    };
                    Owner.Data.CharacterItems.Add(TItem);
                }
                else
                {
                    TItem.Count = count;
                }
                item = new Item(TItem);
                this.Items.Add(itemId, item);

            }
            Owner.statusManager.AddItemChange(itemId, count);
            return true;


        }

        public bool HasItem(int itemId, int count = 1)
        {
            Item item = null;
            if (Items.TryGetValue(itemId, out item))
            {
                return item.Count >= count;
            }
            return false;
        }
        public Item GetItem(int itemId)
        {
            Item item = null;
            Items.TryGetValue(itemId, out item);
            return item;
        }

        public bool UseItem(int itemId, int count = 1)
        {
            Item item = null;
            if (Items.TryGetValue(itemId, out item))
            {
                if (item.Count < count) return false;
                //use
                item.Remove(count);
                Owner.statusManager.AddItemChange(itemId, -count);
                //DBService.Instance.Save();
                return true;

            }
            return false;
        }
        public bool RemoveItem(int itemId, int count = 1)
        {
            Item item = null;
            if (Items.TryGetValue(itemId, out item))
            {
                if (item.Count < count) return false;
                item.Remove(count);
                if (item.Count == 0)
                {
                    Items.Remove(itemId);
                }
                Owner.statusManager.AddItemChange(itemId, -count);
                return true;
            }
            return false;
        }
        public void GetItemInfo(List<NItemInfo> list)
        {
            foreach (var item in this.Items)
            {
                list.Add(new NItemInfo()
                {
                    Id = item.Value.Id,
                    Count = item.Value.Count
                });
            }
        }

    }
}
