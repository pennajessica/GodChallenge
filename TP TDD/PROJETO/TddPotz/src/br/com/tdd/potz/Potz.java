package br.com.tdd.potz;

import br.com.util.Mod11Ck;

public class Potz {
	private String cupom;
	
	public Potz (String cupom) {
		this.cupom = cupom;
	}

	public String getCupom() {
		return cupom;
	}

	public void setCupom(String cupom) {
		this.cupom = cupom;
	}
	
	public Boolean isValido() {
		try {
			Double testaNumero = (Double.parseDouble(cupom));
		} catch (NumberFormatException e) {
			return false;
		}
		if (cupom.length() == 10 && Mod11Ck.isValido(cupom)) {
			return true;
		} else
			return false;
	}
	
	public int pontosObtidos() {
		if(this.isValido())
			return Integer.parseInt(cupom.substring(0,3));
		else
			return -1;
		
	}
}
