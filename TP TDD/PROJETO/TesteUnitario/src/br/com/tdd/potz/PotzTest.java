package br.com.tdd.potz;

import junit.framework.TestCase;

public class PotzTest extends TestCase {
	private Potz pt1 = new Potz("");

	public void testeCupomValido() throws Exception {
		pt1.setCupom("1234567890");
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
}
