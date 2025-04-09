using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class DocumentTests
    {
        [Test]
        public void Constructeur_Document_InitialiseToutesLesProprietes()
        {
            var id = "00001";
            var titre = "Quand sort la recluse";
            var image = "";
            var idGenre = "10014";
            var genre = "Policier";
            var idPublic = "00002";
            var @public = "Adultes";
            var idRayon = "LV003";
            var rayon = "Policiers français étrangers";

            var doc = new Document(
                id, titre, image,
                idGenre, genre,
                idPublic, @public,
                idRayon, rayon
            );

            doc.Id.Should().Be(id);
            doc.Titre.Should().Be(titre);
            doc.Image.Should().Be(image);
            doc.IdGenre.Should().Be(idGenre);
            doc.Genre.Should().Be(genre);
            doc.IdPublic.Should().Be(idPublic);
            doc.Public.Should().Be(@public);
            doc.IdRayon.Should().Be(idRayon);
            doc.Rayon.Should().Be(rayon);
        }
    }
}
