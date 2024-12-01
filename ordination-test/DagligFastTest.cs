using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.Model;

namespace ordination_test
{
    [TestClass]
    public class DagligFastTests
    {
        [TestMethod]
        public void TestDoegnDosis()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinX", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new DagligFast(startDen, slutDen, laegemiddel, 10, 20, 30, 40);

            // Act
            var doegnDosis = ordination.doegnDosis();

            // Assert
            Assert.AreEqual(100, doegnDosis); // 10 + 20 + 30 + 40 = 100
        }

        [TestMethod]
        public void TestSamletDosis()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinY", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new DagligFast(startDen, slutDen, laegemiddel, 10, 20, 30, 40);

            // Act
            var samletDosis = ordination.samletDosis();

            // Assert
            Assert.AreEqual(700, samletDosis); // 100 * 7 dage = 700
        }

        [TestMethod]
        public void TestGetDoser()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinZ", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new DagligFast(startDen, slutDen, laegemiddel, 10, 20, 30, 40);

            // Act
            var doser = ordination.getDoser();

            // Assert
            Assert.AreEqual(4, doser.Length); // Der skal være 4 doser
            Assert.AreEqual(10, doser[0].antal); // MorgenDosis
            Assert.AreEqual(20, doser[1].antal); // MiddagDosis
            Assert.AreEqual(30, doser[2].antal); // AftenDosis
            Assert.AreEqual(40, doser[3].antal); // NatDosis
        }

        [TestMethod]
        public void TestGetType()
        {
            // Arrange
            var laegemiddel = new Laegemiddel("MedicinA", 5, 10, 15, "mg");
            var startDen = new DateTime(2024, 11, 1);
            var slutDen = new DateTime(2024, 11, 7);
            var ordination = new DagligFast(startDen, slutDen, laegemiddel, 10, 20, 30, 40);

            // Act
            var type = ordination.getType();

            // Assert
            Assert.AreEqual("DagligFast", type); // Forventet værdi for typen
        }
    }
}
