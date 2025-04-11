using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class RevueTests
    {
        [Test]
        public void Constructeur_Revue_InitialiseToutesLesProprietes()
        {
            var id = "10007";
            var titre = "Telerama";
            var image = "";
            var idGenre = "10016";
            var genre = "Presse Culturelle";
            var idPublic = "00002";
            var @public = "Adultes";
            var idRayon = "PR002";
            var rayon = "Magazines";
            var periodicite = "HB";    // Hebdomadaire
            var delaiMiseADispo = 26;

            var revue = new Revue(
                id,
                titre,
                image,
                idGenre,
                genre,
                idPublic,
                @public,
                idRayon,
                rayon,
                periodicite,
                delaiMiseADispo
            );

            revue.Id.Should().Be(id);
            revue.Titre.Should().Be(titre);
            revue.Image.Should().Be(image);
            revue.IdGenre.Should().Be(idGenre);
            revue.Genre.Should().Be(genre);
            revue.IdPublic.Should().Be(idPublic);
            revue.Public.Should().Be(@public);
            revue.IdRayon.Should().Be(idRayon);
            revue.Rayon.Should().Be(rayon);
            revue.Periodicite.Should().Be(periodicite);
            revue.DelaiMiseADispo.Should().Be(delaiMiseADispo);
        }

        [Test]
        public void RevueDeserialisation_RemplitToutesLesProprietes()
        {
            const string json = @"
            {
                ""id"": ""10008"",
                ""titre"": ""L'Obs"",
                ""image"": """",
                ""idgenre"": ""10018"",
                ""genre"": ""Actualités"",
                ""idpublic"": ""00003"",
                ""lePublic"": ""Tous publics"",
                ""idrayon"": ""PR002"",
                ""rayon"": ""Magazines"",
                ""periodicite"": ""HB"",
                ""delaiMiseADispo"": 26
            }";

            var revue = JsonConvert.DeserializeObject<Revue>(json);

            revue.Should().NotBeNull();
            revue!.Id.Should().Be("10008");
            revue.Titre.Should().Be("L'Obs");
            revue.Image.Should().BeEmpty();
            revue.IdGenre.Should().Be("10018");
            revue.Genre.Should().Be("Actualités");
            revue.IdPublic.Should().Be("00003");
            revue.Public.Should().Be("Tous publics");
            revue.IdRayon.Should().Be("PR002");
            revue.Rayon.Should().Be("Magazines");
            revue.Periodicite.Should().Be("HB");
            revue.DelaiMiseADispo.Should().Be(26);
        }
    }
}
