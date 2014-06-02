package br.com.tdd.util;

import br.com.util.Mod11Ck;
import junit.framework.TestCase;

public class Mod11CkTest extends TestCase {
	
	public void testeMod11Valido() throws Exception {
		assertTrue(Mod11Ck.isValido("5001234565"));//Potz de exemplo no enunciado corrigido
		assertFalse(Mod11Ck.isValido("5001234560"));
		assertFalse(Mod11Ck.isValido("5001234561"));
		assertFalse(Mod11Ck.isValido("5001234562"));
		assertFalse(Mod11Ck.isValido("5001234563"));
		assertFalse(Mod11Ck.isValido("5001234564"));
		assertFalse(Mod11Ck.isValido("5001234566"));
		assertFalse(Mod11Ck.isValido("5001234567"));
		assertFalse(Mod11Ck.isValido("5001234568"));
		assertFalse(Mod11Ck.isValido("5001234569"));
	}
}
