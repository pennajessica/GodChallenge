using UnityEngine;
using System.Collections;
using System.Threading;

namespace GodChallenge.Domain.Itens {
    public class AegisShield : Item {
        public AegisShield() {
            Nome = "Égide, o escudo";
            ValorEfeito = 10;
            Descricao = string.Format("Aumenta a defesa em {0}%", ValorEfeito);
            Icone = Resources.Load("Itens/EgideItem") as Texture;
            MiniIcone = Resources.Load("Itens/EgideItem30") as Texture;
            Custo = 300;
        }

        public override void ApplyEffect(Player player) {
            if (player.Inventario.HaveItem(this)) {
                int defense = (player.Defense * ValorEfeito) / 100;
                player.ActualDefense = defense;
            }
        }

        public override void RemoveEffect(Player player) {
            player.ActualDefense = player.Defense;
        }

    }
}