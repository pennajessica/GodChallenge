using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GodChallenge.Domain.Itens;
using System;

namespace GodChallenge.Domain {
    [Serializable]
    public class Shop {
        public List<IItem> itens;

        public Shop() {
            itens = new List<IItem>();
            itens.Add(new CupLife());
            itens.Add(new HermesBoots());
            itens.Add(new AegisShield());
            itens.Add(new RayMaster());
            itens.Add(new NemeanManta());
            itens.Add(new ChronosEnssence());
        }

        public bool BuyItem(Player player, int indexItem) {
            if (indexItem > itens.Count)
                throw new IndexOutOfRangeException();

            IItem item = (IItem)itens[indexItem].Clone();

            if (player.Gold < item.Custo) {
                return false;
            } else {
                player.Inventario.Add(item);
                player.Gold -= item.Custo;
                return true;
            }
        }



    }
}
