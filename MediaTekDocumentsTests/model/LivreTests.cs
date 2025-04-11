using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class LivreTests
    {
        [Test]
        public void Constructeur_Livre_InitialiseToutesLesProprietes()
        {
            var id = "00026";
            var titre = "La planète des singes";
            var image = "";
            var isbn = "";
            var auteur = "Pierre Boulle";
            var collection = "Julliard";
            var idGenre = "10002";
            var genre = "Science Fiction";
            var idPublic = "00003";
            var @public = "Tous publics";
            var idRayon = "LV002";
            var rayon = "Littérature française";

            var livre = new Livre(
                id,
                titre,
                image,
                isbn,
                auteur,
                collection,
                idGenre,
                genre,
                idPublic,
                @public,
                idRayon,
                rayon
            );

            livre.Id.Should().Be(id);
            livre.Titre.Should().Be(titre);
            livre.Image.Should().Be(image);
            livre.Isbn.Should().Be(isbn);
            livre.Auteur.Should().Be(auteur);
            livre.Collection.Should().Be(collection);
            livre.IdGenre.Should().Be(idGenre);
            livre.Genre.Should().Be(genre);
            livre.IdPublic.Should().Be(idPublic);
            livre.Public.Should().Be(@public);
            livre.IdRayon.Should().Be(idRayon);
            livre.Rayon.Should().Be(rayon);
        }
    }
}
