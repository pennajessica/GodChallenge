using UnityEngine;
using System.Collections;

namespace GodChallenge.Domain.Itens {
    public class ChronosEnssence : Item {
        public ChronosEnssence() {
            Nome = "Essencia de Chronos";
            ValorEfeito = 10;
            Descricao = string.Format("Reduz o tempo de recarga em {0}%", ValorEfeito);
            Icone = Resources.Load("Itens/EssenciaItem") as Texture;
            MiniIcone = Resources.Load("Itens/EssenciaItem30") as Texture;
            Custo = 450;
        }

        public override void ApplyEffect(Player player) {
            
        }
    }
}