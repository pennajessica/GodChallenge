using UnityEngine;
using System.Collections;
using GUIHelper;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
    public List<GUIButtonCreator> innerMenu;
    private bool canShow = false;

    void Awake() {
        innerMenu.ForEach(linq => {
            linq.fontColor.a = 0;
        });
    }

    void Update() {
        if (canShow) {
            foreach (GUIButtonCreator btn in innerMenu) {
                btn.fontColor.a = Mathf.Lerp(btn.fontColor.a, 1.0f, Time.deltaTime * 0.8f);
                if (btn.fontColor.a >= 1)
                    canShow = false;
            }
        }
    }

    public void ShowInnerMenu() {
        StartCoroutine(Util.WaitSeconds(4, () => {
            canShow = true;
            innerMenu.ForEach(linq => {
                linq.gameObject.SetActive(true);
            });
        }));
    }
}
