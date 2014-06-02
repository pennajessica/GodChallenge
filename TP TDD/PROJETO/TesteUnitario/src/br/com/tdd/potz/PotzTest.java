package br.com.tdd.potz;

import junit.framework.TestCase;

public class PotzTest extends TestCase {
	private Potz pt1 = new Potz("");

	public void testeCupomValido() throws Exception {
		pt1.setCupom("5001234565");
		assertTrue(pt1.isValido());
	}

	public void testeCupomInvalidoMenor10() throws Exception {
		pt1.setCupom("12345678");
		assertFalse(pt1.isValido());
	}
	
	public void testeCupomInvalidoMaior10() throws Exception {
		pt1.setCupom("12345678901");
		assertFalse(pt1.isValido());
	}
	
	public void testeCupomIsNumero() throws Exception {
		pt1.setCupom("A012345678");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234565");
		assertTrue(pt1.isValido());
	}
	public void testeCumpomMod11() throws Exception {
		pt1.setCupom("5001234565");//Exemplo do enunciado corrigido
		assertTrue(pt1.isValido());
		pt1.setCupom("5001234560");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234561");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234562");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234563");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234564");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234566");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234567");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234568");
		assertFalse(pt1.isValido());
		pt1.setCupom("5001234569");
		assertFalse(pt1.isValido());
	}
}
