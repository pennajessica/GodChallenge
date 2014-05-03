#pragma strict

var Velocidade: float;

function Update ()
{
	this.transform.Rotate(0,0,Velocidade);
}