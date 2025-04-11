using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class GenreTests
    {
        [Test]
        public void Constructeur_Genre_InitialiseToutesLesProprietes()
        {
            var id = "10002";
            var libelle = "Science Fiction";

            var genre = new Genre(id, libelle);

            genre.Id.Should().Be(id);
            genre.Libelle.Should().Be(libelle);
        }

        [Test]
        public void ToString_RetourneLeLibelle()
        {
            var id = "10013";
            var libelle = "Comédie";

            var genre = new Genre(id, libelle);

            genre.ToString().Should().Be(libelle);
        }
    }
}
