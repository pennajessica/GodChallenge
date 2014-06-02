package br.com.util;

public class Mod11Ck {
	
	public static Boolean isValido(String sequenciaNumerica) {
		String noveDigitos = sequenciaNumerica.substring(0, sequenciaNumerica.length()- 1);
		String dvSeqNum = sequenciaNumerica.substring(sequenciaNumerica.length()-1);
		String dvCalculado = calc(noveDigitos);
		return  dvSeqNum.equals(dvCalculado);
	}
	
	//REFERÊNCIA: http://w2.syronex.com/jmr/programming/mod11chk
	public static String calc(String digStr) {
	    int len = digStr.length();
	    int sum = 0, rem = 0;
	    int[] digArr = new int[len];
	    for (int k=1; k<=len; k++) // compute weighted sum
	      sum += (11 - k) * Character.getNumericValue(digStr.charAt(k - 1));
	    if ((rem = sum % 11) == 0) return "0";
	    else if (rem == 1) return "X";
	    return (new Integer(11 - rem)).toString();
	  }
}
