
public class Triangle {

	//	 Os 3 lados do triangulo
	public int a, b, c;
	public Object area = null;
	public boolean isTriangle = false;
	public static final int EQUILATERO = 1;
	public static final int ISOSCELES = 2;
	public static final int ESCALENO = 3;
	public static final int INEXISTENTE = 4;
	
	
	public Triangle (int lado1, int lado2, int lado3) {
		a = lado1;
		b = lado2;
		c = lado3;
	}

	public int determineType( ) {

		if (a <= 0 || b <= 0 || c <= 0)
			return INEXISTENTE;
		if (! (a + b > c && a + c > b && b + c > a)) 
		
			return INEXISTENTE;
		
		isTriangle = true;
		area = 0;
		
		if (a == b && b == c) 
				return EQUILATERO;
		if (a == b || b == c || a == c) 
				return ISOSCELES;
		
		return ESCALENO;
		
	}//funcao
}
