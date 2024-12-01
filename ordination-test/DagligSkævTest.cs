using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordination_test
{ 
[TestClass]
public class DagligSkævTests
{
    [TestMethod]
    public void TestDoegnDosis()
    {
        // Arrange
        var laegemiddel = new Laegemiddel("MedicinX", 5, 10, 15, "mg");
        var startDen = new DateTime(2024, 11, 1);
        var slutDen = new DateTime(2024, 11, 7);
        var ordination = new DagligSkæv(startDen, slutDen, laegemiddel);
        ordination.opretDosis(new DateTime(2024, 11, 1, 6, 0, 0), 10);
        ordination.opretDosis(new DateTime(2024, 11, 1, 12, 0, 0), 20);
        ordination.opretDosis(new DateTime(2024, 11, 1, 18, 0, 0), 30);
        ordination.opretDosis(new DateTime(2024, 11, 1, 23, 59, 0), 40);

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
        var ordination = new DagligSkæv(startDen, slutDen, laegemiddel);
        ordination.opretDosis(new DateTime(2024, 11, 1, 6, 0, 0), 10);
        ordination.opretDosis(new DateTime(2024, 11, 1, 12, 0, 0), 20);
        ordination.opretDosis(new DateTime(2024, 11, 1, 18, 0, 0), 30);
        ordination.opretDosis(new DateTime(2024, 11, 1, 23, 59, 0), 40);

        // Act
        var samletDosis = ordination.samletDosis();

        // Assert
        Assert.AreEqual(700, samletDosis); // 100 * 7 dage = 700
    }

    [TestMethod]
    public void TestGetType()
    {
        // Arrange
        var laegemiddel = new Laegemiddel("MedicinA", 5, 10, 15, "mg");
        var startDen = new DateTime(2024, 11, 1);
        var slutDen = new DateTime(2024, 11, 7);
        var ordination = new DagligSkæv(startDen, slutDen, laegemiddel);

        // Act
        var type = ordination.getType();

        // Assert
        Assert.AreEqual("DagligSkæv", type); // Forventet værdi for typen
    }

    [TestMethod]
    public void TestOpretDosis()
    {
        // Arrange
        var laegemiddel = new Laegemiddel("MedicinB", 5, 10, 15, "mg");
        var startDen = new DateTime(2024, 11, 1);
        var slutDen = new DateTime(2024, 11, 7);
        var ordination = new DagligSkæv(startDen, slutDen, laegemiddel);

        // Act
        ordination.opretDosis(new DateTime(2024, 11, 1, 6, 0, 0), 15);
        ordination.opretDosis(new DateTime(2024, 11, 1, 12, 0, 0), 25);

        // Assert
        Assert.AreEqual(2, ordination.doser.Count); // Der skal være 2 doser tilføjet
        Assert.AreEqual(15, ordination.doser[0].antal); // Forventet værdi for første dosis
        Assert.AreEqual(25, ordination.doser[1].antal); // Forventet værdi for anden dosis
    }
}
}

