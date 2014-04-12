using UnityEngine;
using System.Collections;
using System.Threading;
using System;

namespace GodChallenge.Domain.Itens {
    public class CupLife : Item {
        public CupLife() {
            Nome = "Cálice de Vida";
            ValorEfeito = 2;
            Descricao = string.Format("Recupera {0} de Vida por segundo", ValorEfeito);
            Icone = Resources.Load("Itens/PotionItem") as Texture;
            MiniIcone = Resources.Load("Itens/PotionItem30") as Texture;
            Custo = 100;
        }

        public override void ApplyEffect(Player player) {
            new Thread(() => {
                while(player.Inventario.HaveItem(this)) {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    player.Hp += ValorEfeito;
                }
            }).Start();
        }
    }
}