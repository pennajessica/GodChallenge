package br.com.tdd.potz;

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
		if (cupom.length() == 10) {
			return true;
		} else
			return false;
	}

}
