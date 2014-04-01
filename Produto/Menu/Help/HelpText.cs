using UnityEngine;
using System.Collections;
using GUIHelper;

public class HelpText : GUITextCreator {
    //public float scrollbarValue;
    public Vector2 scrollPosition = Vector2.zero;
    public float height, width, h;
    public Rect scrollCanvas;

    void Start() {
        this.text = "<b>Modalidades de Jogo</b>\n\n";
        this.text += "O jogador pode escolher entre as opções Single Player (jogar contra á máquina) ou Multiplayer (Jogar em rede). As duas opções se encontram no menu principal do jogo.\n";
        this.text += "Escolhendo a opção Single Player o jogo se inicia imediatamente, abrindo uma batalha contra a máquina.\n";
        this.text += "Caso escolha a opção Multiplayer, aparecerá uma tela para escolher entre conectar a um servidor já existente ou criar um novo servidor de jogo.";

        this.text += "\n\n<b>Seleção de Personagem</b>\n\n";
        this.text += "Após escolher a modalidade de jogo, abrirá uma tela de seleção de personagem. Nela o jogador deverá clicar com o botão esquerdo do mouse na foto do personagem desejado.\n";
        this.text += "As fotos dos personagens estão na barra de seleção, no canto superior central da tela.";

        this.text += "\n\n<b>Movimentação de personagem</b>\n\n";
        this.text += "A movimentação de personagem é baseada em point click.\n";
        this.text += "Ao clicar o botão esquerdo do mouse em alguma parte do cenário, o personagem se desloca até o local indicado.";

        this.text += "\n\n<b>Instruções de ataque</b>\n\n";
        this.text += "O ataque é dado através da seleção de Skill na barra inferior, no centro da tela. Antes de selecionar a Skill seu personagem deve estar posicionado na direção em que deseja aplicar o poder.";

        this.text += "\n\n<b>Câmera - Zoom</b>\n\n";
        this.text += "Com o Scroll do mouse você aumenta ou diminui a distância da câmera em relação ao personagem. Basta rodá-lo pra frente caso queira dar zoom e rodá-lo para trás caso queira afastar a câmera.";

        this.text += "\n\n<b>Câmera - Movimentação</b>\n\n";
        this.text += "Ao posicionar a seta do mouse no canto da tela, seja ele qual canto for, a câmera se movimenta no sentido da seta, fazendo assim o jogador ter uma visão maior do cenário.";

        this.text += "\n\n<b>Som</b>\n\n";
        this.text += "Você pode escolher em jogar com o som Ligado ou Desligado. Para isso acesse o menu principal, pressione o botão opções e em seguida escolha a opção desejada.";
    }

    public override void onGUI() {
        

        scrollCanvas = new Rect(Canvas.xMin, Canvas.yMin, Canvas.width, Canvas.height + h);

        scrollPosition = GUI.BeginScrollView(scrollCanvas, scrollPosition, new Rect(Canvas.xMin, Canvas.yMin, Canvas.width - 17, height));
        Style.richText = true;

        base.onGUI();

        GUI.EndScrollView();
    }
}
