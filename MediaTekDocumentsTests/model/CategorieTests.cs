using MediaTekDocuments.model;
using NUnit.Framework;
using FluentAssertions;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class CategorieTests
    {
        [Test]
        public void Constructeur_Categorie_InitialiseToutesLesProprietes()
        {
            var id = "10004";
            var libelle = "Historique";

            var cat = new Categorie(id, libelle);

            cat.Id.Should().Be(id);
            cat.Libelle.Should().Be(libelle);
        }

        [Test]
        public void ToString_RetourneLeLibelle()
        {
            var id = "10007";
            var libelle = "Aventures";

            var cat = new Categorie(id, libelle);

            cat.ToString().Should().Be(libelle);
        }
    }
}
