package br.com.tdd.potz;

import org.junit.Test;

import junit.framework.TestCase;

public class PotzTest extends TestCase {
	
	@Test
	public void testeValidaCupomPotz() throws Exception {
		
		Potz pt1 = new Potz("1234567890");
		assertTrue(pt1.isValido());
	}

}
