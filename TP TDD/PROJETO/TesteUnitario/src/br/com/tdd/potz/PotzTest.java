package br.com.tdd.potz;

import junit.framework.TestCase;

public class PotzTest extends TestCase {
	
	public void validaCupomPotz() {
		
		Potz pt1 = new Potz("1234567890");
		assertTrue(pt1.isValido());
	}

}
