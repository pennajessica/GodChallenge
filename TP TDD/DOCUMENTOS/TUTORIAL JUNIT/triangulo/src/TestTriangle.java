
import junit.framework.*;

public class TestTriangle extends TestCase {
	
	//@SuppressWarnings("static-access")
	public void testCreateTriangle( ) {
		
		Triangle t = new Triangle(4, 2, 4);
		Triangle tri = new Triangle(4, 2, 4);
		
		//Verificando se o contrutor esta correto
		assertEquals("Primeiro lado", t.a, 4);
		assertEquals("Segundo lado ", t.b, 2);
		assertEquals("Terceiro lado", t.c, 4);

		assertFalse("NÃO é um triangulo", t.isTriangle);
		assertNull("Area é nula", t.area);
		
		int typeT = t.determineType();	//1-EQUILATERO 2-ISOSCELES 3-ESCALENO 4-INEXISTENTE
				
		assertTrue("É um triangulo", t.isTriangle);
		assertNotNull("Area NÃO é vazio", t.area);
    	assertNotSame("NÃO é o mesmo triangulo", t, tri);
    	
    	assertFalse("NÃO são iguais: tri.triangle e t.triangle", tri.equals(t));
    	//assertTrue("NÃO são iguais: tri.triangle e t.triangle", tri.equals(t));
    	//assertEquals("NÃO são iguais", tri, t.a);

    	//assertSame("", , );
		
	}
}
/*
JUnitdoc
http://junit.sourceforge.net/javadoc_40/org/junit/Assert.html
http://junit.sourceforge.net/javadoc/junit/framework/Assert.html
*/
