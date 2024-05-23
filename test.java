public class TemperatureConverter {
    public static double convertFahrenheitToCelsius(double fahrenheit) {
        return (fahrenheit - 32) * 5 / 9;
    }

    public static double convertCelsiusToFahrenheit(double celsius) {
        return celsius * 9 / 5 + 32;
    }

    public static void main(String[] args) {
        try {
            double fahrenheit = 98.6;
            double celsius = convertFahrenheitToCelsius(fahrenheit);
            System.out.println(fahrenheit + " F is " + celsius + " C");

            celsius = 37;
            fahrenheit = convertCelsiusToFahrenheit(celsius);
            System.out.println(celsius + " C is " + fahrenheit + " F");
        } catch (Exception e) {
            System.out.println("An error occurred: " + e.getMessage());
        }
    }

}

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class TemperatureConverterTest {

    @Test
    public void testConvertFahrenheitToCelsius() {
        double fahrenheit = 98.6;
        double expectedCelsius = (fahrenheit - 32) * 5 / 9;
        assertEquals(expectedCelsius, TemperatureConverter.convertFahrenheitToCelsius(fahrenheit), 0.001);
    }

    @Test
    public void testConvertCelsiusToFahrenheit() {
        double celsius = 37;
        double expectedFahrenheit = celsius * 9 / 5 + 32;
        assertEquals(expectedFahrenheit, TemperatureConverter.convertCelsiusToFahrenheit(celsius), 0.001);
    }
}