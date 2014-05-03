using UnityEngine;
using System.Collections;
using GUIHelper;

public class ConfigBtnPage : GUIButtonCreator {
    public GameObject[] pages;
    public bool goToNextPage = true;

    public int actualPage = 0;
    

    protected override void onMouseOver() {
        base.onMouseOver();
        fontColor.r = 1.0f;
        fontColor.g = 0.752f;
        fontColor.b = 0;
    }

    protected override void onMouseOut() {
        base.onMouseOut();
        fontColor.r = 0.996f;
        fontColor.g = 0.818f;
        fontColor.b = 0.262f;
    }

    protected override void onClick() {
        if (actualPage >= pages.Length - 1)
            goToNextPage = false;
        else if (actualPage <= 0)
            goToNextPage = true;

        if (goToNextPage) {
            actualPage++;
            
            pages[actualPage - 1].SetActive(false);
            pages[actualPage].SetActive(true);

        } else {
            actualPage--;
            
            pages[actualPage + 1].SetActive(false);
            pages[actualPage].SetActive(true);
        }
    }
}
