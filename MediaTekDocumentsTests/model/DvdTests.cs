using MediaTekDocuments.model;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class DvdTests
    {
        [Test]
        public void Constructeur_Dvd_InitialiseToutesLesProprietes()
        {
            var id = "20003";
            var titre = "Jurassic Park";
            var image = "";
            var duree = 128;
            var realisateur = "Steven Spielberg";
            var synopsis = "Un milliardaire et des généticiens créent des dinosaures à partir de clonage.";
            var idGenre = "10002";
            var genre = "Science Fiction";
            var idPublic = "00003";
            var @public = "Tous publics";
            var idRayon = "DF001";
            var rayon = "DVD films";

            var dvd = new Dvd(
                id,
                titre,
                image,
                duree,
                realisateur,
                synopsis,
                idGenre,
                genre,
                idPublic,
                @public,
                idRayon,
                rayon
            );

            dvd.Id.Should().Be(id);
            dvd.Titre.Should().Be(titre);
            dvd.Image.Should().Be(image);
            dvd.Duree.Should().Be(duree);
            dvd.Realisateur.Should().Be(realisateur);
            dvd.Synopsis.Should().Be(synopsis);
            dvd.IdGenre.Should().Be(idGenre);
            dvd.Genre.Should().Be(genre);
            dvd.IdPublic.Should().Be(idPublic);
            dvd.Public.Should().Be(@public);
            dvd.IdRayon.Should().Be(idRayon);
            dvd.Rayon.Should().Be(rayon);
        }

        [Test]
        public void DvdDeserialisation_RemplitToutesLesProprietes()
        {
            const string json = @"
            {
                ""id"": ""20003"",
                ""titre"": ""Jurassic Park"",
                ""image"": """",
                ""duree"": 128,
                ""realisateur"": ""Steven Spielberg"",
                ""synopsis"": ""Un milliardaire et des généticiens créent des dinosaures à partir de clonage."",
                ""idgenre"": ""10002"",
                ""genre"": ""Science Fiction"",
                ""idpublic"": ""00003"",
                ""lePublic"": ""Tous publics"",
                ""idrayon"": ""DF001"",
                ""rayon"": ""DVD films""
            }";

            var dvd = JsonConvert.DeserializeObject<Dvd>(json);

            dvd.Should().NotBeNull();
            dvd!.Id.Should().Be("20003");
            dvd.Titre.Should().Be("Jurassic Park");
            dvd.Image.Should().BeEmpty();
            dvd.Duree.Should().Be(128);
            dvd.Realisateur.Should().Be("Steven Spielberg");
            dvd.Synopsis.Should().Be("Un milliardaire et des généticiens créent des dinosaures à partir de clonage.");
            dvd.IdGenre.Should().Be("10002");
            dvd.Genre.Should().Be("Science Fiction");
            dvd.IdPublic.Should().Be("00003");
            dvd.Public.Should().Be("Tous publics");
            dvd.IdRayon.Should().Be("DF001");
            dvd.Rayon.Should().Be("DVD films");
        }
    }
}
