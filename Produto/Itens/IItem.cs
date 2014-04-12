using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GodChallenge.Domain {
    public interface IItem {
        string Nome { get; }
        string Descricao { get; }
        int Custo { get; }
        int ValorEfeito { get; }
        Texture Icone { get; }
        Texture MiniIcone { get; }

        void ApplyEffect(Player player);
        void RemoveEffect(Player player);
        object Clone();
    }
}
