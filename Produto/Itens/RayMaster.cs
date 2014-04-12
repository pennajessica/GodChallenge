using UnityEngine;
using System.Collections;
using System.Threading;

namespace GodChallenge.Domain.Itens {
    public class RayMaster : Item {
        private int attackDmg;

        public RayMaster() {
            Nome = "O Raio Mestre";
            ValorEfeito = 5;
            Descricao = string.Format("Aumenta o ataque em {0}%", ValorEfeito);
            Icone = Resources.Load("Itens/RaioMestreItem") as Texture;
            MiniIcone = Resources.Load("Itens/RaioMestreItem30") as Texture;
            Custo = 300;
        }

        public override void ApplyEffect(Player player) {
            if (player.Inventario.HaveItem(this)) {
                attackDmg = player.AttackDamage;
                int atk = (player.AttackDamage * ValorEfeito) / 100;
                player.AttackDamage = atk;
            }
        }

        public override void RemoveEffect(Player player) {
            player.AttackDamage = attackDmg;
        }
    }
}