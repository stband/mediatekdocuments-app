using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class PublicTests
    {
        [Test]
        public void Constructeur_Public_InitialiseToutesLesProprietes()
        {
            var id = "00001";
            var libelle = "Jeunesse";

            var publicCat = new Public(id, libelle);

            publicCat.Id.Should().Be(id);
            publicCat.Libelle.Should().Be(libelle);
        }

        [Test]
        public void ToString_RetourneLeLibelle()
        {
            var id = "00003";
            var libelle = "Tous publics";

            var publicCat = new Public(id, libelle);

            publicCat.ToString().Should().Be(libelle);
        }
    }
}
