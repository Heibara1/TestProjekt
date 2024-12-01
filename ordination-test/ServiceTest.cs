namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetAnbefaletDosisPerDøgn_InvalidPatientId_ThrowsArgumentException()
    {
        // Arrange
        int invalidPatientId = -1; 
        int laegemiddelId = service.GetLaegemidler().First().LaegemiddelId;

        // Act
        service.GetAnbefaletDosisPerDøgn(invalidPatientId, laegemiddelId);

        
    }

    [TestMethod]
    public void GetAnbefaletDosisPerDøgn()
    {
        // Arrange
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        // Act
        double dosis = service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);

        // Assert
        
        double expectedDosis = lm.enhedPrKgPrDoegnNormal * patient.vaegt; 
        Assert.AreEqual(expectedDosis, dosis, "Dosis beregningen skal være korrekt.");
    }

}
