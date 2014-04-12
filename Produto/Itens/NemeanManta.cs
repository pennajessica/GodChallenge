using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Itens {

    public class NemeanManta : Item {
        int dmgCount;

        public NemeanManta() {
            Nome = "Manta de Neméia";
            ValorEfeito = 10;
            Descricao = string.Format("Reduz a repulsão em {0}%", ValorEfeito);
            Icone = Resources.Load("Itens/CapaItem") as Texture;
            MiniIcone = Resources.Load("Itens/CapaItem30") as Texture;
            Custo = 400;
        }

        public override void ApplyEffect(Player player) {
            if (player.Inventario.HaveItem(this)) {
                dmgCount = player.DamageCount;

                int dmg = player.DamageCount * ValorEfeito / 100;
                player.DamageCount = dmg;
            }
        }

        public override void RemoveEffect(Player player) {
            player.DamageCount = dmgCount;
        }
    }
}