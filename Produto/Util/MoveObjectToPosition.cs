using UnityEngine;
using System.Collections;

public class MoveObjectToPosition : MonoBehaviour {
    public Transform newPosition;
    public bool canMove;
    public float speed;

	void Update () {
        if(canMove)
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPosition.position, speed * Time.deltaTime);

        if (this.transform.position == newPosition.position)
            canMove = false;
	}
}
