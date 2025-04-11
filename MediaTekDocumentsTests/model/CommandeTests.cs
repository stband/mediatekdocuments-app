using MediaTekDocuments.model;
using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;
using System;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class CommandeTests
    {
        [Test]
        public void Constructeur_Commande_InitialiseToutesLesProprietes()
        {
            var id = "00010";
            var titre = "Le vestibule des causes perdues";
            var idLivreDvd = "00010";
            var dateCommande = new DateTime(2025, 3, 27, 0, 0, 0, DateTimeKind.Local);
            var montant = 50.0;
            var nbExemplaires = 2;
            var statut = "00003";

            var cmd = new Commande(
                id,
                titre,
                idLivreDvd,
                dateCommande,
                montant,
                nbExemplaires,
                statut
            );

            cmd.Id.Should().Be(id);
            cmd.Titre.Should().Be(titre);
            cmd.IdLivreDvd.Should().Be(idLivreDvd);
            cmd.DateCommande.Should().Be(dateCommande);
            cmd.Montant.Should().BeApproximately(montant, 0.001);
            cmd.NbExemplaires.Should().Be(nbExemplaires);
            cmd.Suivi.Should().Be(statut);
        }

        [Test]
        public void CommandeDeserialisation_RemplitToutesLesProprietes()
        {
            const string json = @"
            {
                ""idCommande"": ""00010"",
                ""titre"": ""Le vestibule des causes perdues"",
                ""idLivreDvd"": ""00010"",
                ""dateCommande"": ""2025-03-27"",
                ""montant"": 50.0,
                ""nbExemplaire"": 2,
                ""statut"": ""00003""
            }";

            var cmd = JsonConvert.DeserializeObject<Commande>(json);

            cmd.Should().NotBeNull();
            cmd!.Id.Should().Be("00010");
            cmd.Titre.Should().Be("Le vestibule des causes perdues");
            cmd.IdLivreDvd.Should().Be("00010");
            cmd.DateCommande.Should().Be(new DateTime(2025, 3, 27, 0, 0, 0, DateTimeKind.Local));
            cmd.Montant.Should().BeApproximately(50.0, 0.001);
            cmd.NbExemplaires.Should().Be(2);
            cmd.Suivi.Should().Be("00003");
        }
    }
}
