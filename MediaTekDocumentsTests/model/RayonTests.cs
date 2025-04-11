using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class RayonTests
    {
        [Test]
        public void Constructeur_Rayon_InitialiseToutesLesProprietes()
        {
            var id = "DF001";
            var libelle = "DVD films";

            var rayon = new Rayon(id, libelle);

            rayon.Id.Should().Be(id);
            rayon.Libelle.Should().Be(libelle);
        }

        [Test]
        public void ToString_RetourneLeLibelle()
        {
            var id = "LV001";
            var libelle = "Littérature étrangère";

            var rayon = new Rayon(id, libelle);

            rayon.ToString().Should().Be(libelle);
        }
    }
}
