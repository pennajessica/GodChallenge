
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

		assertFalse("N�O � um triangulo", t.isTriangle);
		assertNull("Area � nula", t.area);
		
		int typeT = t.determineType();	//1-EQUILATERO 2-ISOSCELES 3-ESCALENO 4-INEXISTENTE
				
		assertTrue("� um triangulo", t.isTriangle);
		assertNotNull("Area N�O � vazio", t.area);
    	assertNotSame("N�O � o mesmo triangulo", t, tri);
    	
    	assertFalse("N�O s�o iguais: tri.triangle e t.triangle", tri.equals(t));
    	//assertTrue("N�O s�o iguais: tri.triangle e t.triangle", tri.equals(t));
    	//assertEquals("N�O s�o iguais", tri, t.a);

    	//assertSame("", , );
		
	}
}
/*
JUnitdoc
http://junit.sourceforge.net/javadoc_40/org/junit/Assert.html
http://junit.sourceforge.net/javadoc/junit/framework/Assert.html
*/
