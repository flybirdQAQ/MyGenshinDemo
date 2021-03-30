using Common;
using SkillBridge.Message;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using GameServer.Entities;
using GameServer.Services;

namespace GameServer.Managers
{
    class StoreManager : Singleton<StoreManager>
    {

        public Result BuyItem(NetConnection<NetSession> sender, NGoodsInfo nGoods)
        {
            GoodsDefine goodsDefine;
            if (!DataManager.Instance.Goods.TryGetValue(nGoods.Id, out goodsDefine)) return Result.Failed;
            if (!goodsDefine.isActive) return Result.Failed;
            if (!sender.Session.Character.goodsLimitManager.CanBuy(nGoods, goodsDefine)) return Result.Failed;
            if (!HasCurrency(sender.Session.Character, goodsDefine, nGoods.Count)) return Result.Failed;
            ApplyCurrency(sender.Session.Character, goodsDefine, nGoods.Count);

            switch (goodsDefine.Type)
            {
                case GoodsDefine.GoodsType.Item:
                    sender.Session.Character.itemManager.AddItem(goodsDefine.ItemID, goodsDefine.Count * nGoods.Count);
                    break;
                case GoodsDefine.GoodsType.Equip:
                    sender.Session.Character.equipManager.AddEquip(goodsDefine.ItemID, goodsDefine.Count * nGoods.Count);
                    break;
                default: break;
            }
            sender.Session.Character.goodsLimitManager.Apply(nGoods, goodsDefine);
            return Result.Success;


        }

        public Result SellItem(NetConnection<NetSession> sender, NItemInfo itemInfo)
        {
            ItemDefine itemDefine;
            if (!DataManager.Instance.Items.TryGetValue(itemInfo.Id, out itemDefine)) return Result.Failed;
            if (!sender.Session.Character.itemManager.HasItem(itemInfo.Id, itemInfo.Count)) return Result.Failed;
            sender.Session.Character.itemManager.RemoveItem(itemInfo.Id, itemInfo.Count);
            ApplyCurrency(sender.Session.Character, itemDefine, itemInfo.Count);
            return Result.Success;
        }
        public Result SellEquip(NetConnection<NetSession> sender, NEquipInfo equipInfo)
        {
            Equip equip;
            if (!sender.Session.Character.equipManager.GetEquip(equipInfo.Id,out equip)) return Result.Failed;
            if (!sender.Session.Character.equipManager.RemoveEquip(equipInfo.Id)) return Result.Failed;
            ApplyCurrency(sender.Session.Character, equip.Define);
            return Result.Success;
        }

        public bool HasCurrency(Character character, GoodsDefine goodsDefine, int count)
        {
            if (goodsDefine.Price * count > character.Gold) return false;
            if (goodsDefine.CurrencyID != null)
            {
                for (int i = 0; i < goodsDefine.CurrencyID.Count; i++)
                {
                    if (!character.itemManager.HasItem(goodsDefine.CurrencyID[i], goodsDefine.CurrencyPrice[i] * count)) return false;
                }
            }

            return true;
        }
        public void ApplyCurrency(Character character, GoodsDefine goodsDefine, int count)

        {
            character.Gold -= goodsDefine.Price * count;
            if (goodsDefine.CurrencyID != null)
            {
                for (int i = 0; i < goodsDefine.CurrencyID.Count; i++)
                {
                    character.itemManager.RemoveItem(goodsDefine.CurrencyID[i], goodsDefine.CurrencyPrice[i] * count);
                }
            }

        }
        public void ApplyCurrency(Character character, ItemDefine itemDefine, int count)
        {
            character.Gold += itemDefine.SellPrice*count;
            if (itemDefine.SellCurrencyID != null)
            {
                for (int i = 0; i < itemDefine.SellCurrencyID.Count; i++)
                {
                    character.itemManager.AddItem(itemDefine.SellCurrencyID[i], itemDefine.SellCurrencyPrice[i] * count);
                }
            }
        }
        public void ApplyCurrency(Character character, EquipDefine equipDefine)
        {
            character.Gold += equipDefine.SellPrice;
            if (equipDefine.SellCurrencyID != null)
            {
                for (int i = 0; i < equipDefine.SellCurrencyID.Count; i++)
                {
                    character.itemManager.AddItem(equipDefine.SellCurrencyID[i], equipDefine.SellCurrencyPrice[i]);
                }
            }

        }



    }
}
