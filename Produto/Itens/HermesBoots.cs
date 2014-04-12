using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Itens {
    public class HermesBoots : Item {
        float speed;

        public HermesBoots() {
            Nome = "Sapatos de Hermes";
            ValorEfeito = 20;
            Descricao = string.Format("Aumenta a movimentação do personagem em {0}%", ValorEfeito);
            Icone = Resources.Load("Itens/SapatosItem") as Texture;
            MiniIcone = Resources.Load("Itens/SapatosItem30") as Texture;
            Custo = 250;
        }

        public override void ApplyEffect(Player player) {
            if (player.Inventario.HaveItem(this)) {
                speed = player.ActualSpeed;
                float newSpeed = player.ActualSpeed * ValorEfeito / 100;
                player.ActualSpeed = newSpeed;
            }
        }

        public override void RemoveEffect(Player player) {
            player.ActualSpeed = speed;
        }
    }
}