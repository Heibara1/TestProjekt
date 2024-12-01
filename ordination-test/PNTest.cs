using Microsoft.VisualStudio.TestTools.UnitTesting;
using shared.Model;
using System;

namespace OrdinationTest
{
    [TestClass]
    public class PNTests
    {
        [TestMethod]
        public void TestGivDosis_ValidDate_ReturnsTrueAndAddsDate()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinZ", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new PN(startDen, slutDen, 5, laegemiddel);
            var validDato = new Dato { dato = new DateTime(2024, 11, 2) };

            // Act
            var result = ordination.givDosis(validDato);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, ordination.dates.Count); // Forventet antal dage registreret
            Assert.AreEqual(validDato.dato, ordination.dates[0].dato); // Forventet dato skal være den samme
        }

        [TestMethod]
        public void TestGivDosis_InvalidDate_ReturnsFalseAndDoesNotAddDate()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinZ", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new PN(startDen, slutDen, 5, laegemiddel);
            var invalidDato = new Dato { dato = new DateTime(2024, 11, 8) };

            // Act
            var result = ordination.givDosis(invalidDato);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, ordination.dates.Count); // Forventet antal dage registreret er 0
        }
        [TestMethod]
        public void TestDoegnDosis_EnkelteDatoer()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinB", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new PN(startDen, slutDen, 10, laegemiddel);
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 2) });
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 5) });

            // Act
            var doegnDosis = ordination.doegnDosis();

            // Assert
            Assert.AreEqual(10, doegnDosis); // Test med korrekt antal datoer og et gyldigt interval
        }


        [TestMethod]
        public void TestSamletDosis()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinB", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new PN(startDen, slutDen, 5, laegemiddel);
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 2) });
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 4) });
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 6) });

            // Act
            var samletDosis = ordination.samletDosis();

            // Assert
            Assert.AreEqual(15, samletDosis); // 3 dage med 5 enheder per dag = 15 enheder
        }

        [TestMethod]
        public void TestGetAntalGangeGivet()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinC", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new PN(startDen, slutDen, 5, laegemiddel);
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 2) });
            ordination.givDosis(new Dato { dato = new DateTime(2024, 11, 4) });

            // Act
            var antalGangeGivet = ordination.getAntalGangeGivet();

            // Assert

        }
    }
}