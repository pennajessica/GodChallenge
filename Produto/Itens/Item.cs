using UnityEngine;
using System.Collections;
using System;

namespace GodChallenge.Domain {

    public abstract class Item : IItem, ICloneable {
        private string nome;
        private string descricao;
        private int custo;
        private int valorEfeito;
        private Texture icone, miniIcone;

        public Item() {
            Nome = "Item";
            Descricao = "Descrição do Item";
            //Icone = new Texture2D(20, 20);
        }


        public string Nome {
            get {
                return this.nome;
            }
            protected set {
                this.nome = value;
            }
        }

        public string Descricao {
            get {
                return this.descricao;
            }
            protected set {
                this.descricao = value;
            }
        }

        public int Custo {
            get {
                return this.custo;
            }
            protected set {
                this.custo= value;
            }
        }

        public int ValorEfeito {
            get {
                return this.valorEfeito;
            }
            protected set {
                this.valorEfeito = value;
            }
        }

        public Texture Icone {
            get {
                return this.icone;
            }
            protected set {
                this.icone = value;
            }
        }

        public Texture MiniIcone {
            get {
                return this.miniIcone;
            }
            protected set {
                this.miniIcone = value;
            }
        }

        public virtual void ApplyEffect(Player player) {
            throw new System.NotImplementedException();
        }


        public virtual void RemoveEffect(Player player) {
            throw new System.NotImplementedException();
        }

        public object Clone() {
            return this.MemberwiseClone();
        }
    }
}