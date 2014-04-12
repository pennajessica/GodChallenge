using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GodChallenge.Domain {
    public class Inventario {
        private List<IItem> itens;
        private Player player;

        public Inventario(Player player) {
            itens = new List<IItem>();
            this.player = player;
        }

        public void Add(IItem item) {
            if (!itens.Contains(item)) {
                item.ApplyEffect(player);
                this.itens.Add(item);
            }
        }

        public void Consume(IItem item) {

        }

        public void Remove(IItem item) {
            item.RemoveEffect(player);
            itens.Remove(item);
        }

        public bool HaveItem(IItem item) {
            return this.itens.Contains(item);
        }

        public void DrawItens(Rect inventRect) {
            if (itens.Count <= 0)
                return;

            GUI.BeginGroup(inventRect);
            for (int x = 0; x < itens.Count; x++) {
                IItem item = itens[x];
                if (!item.MiniIcone)
                    continue;

                GUI.DrawTexture(new Rect((32 * x), 0, item.MiniIcone.width, item.MiniIcone.height), item.MiniIcone);
            }
            GUI.EndGroup();
        }
    }
}