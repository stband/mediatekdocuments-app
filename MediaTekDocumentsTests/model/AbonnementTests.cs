using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class AbonnementTests
    {
        [Test]
        public void Constructeur_Abonnement_InitialiseToutesLesProprietes()
        {
            var id = "00026";
            var idRevue = "10007";
            var titreRevue = "Telerama";
            var dateCommande = new DateTime(2025, 4, 1);
            var dateFin = new DateTime(2026, 4, 10);
            var montant = 45.5;

            var abo = new Abonnement(
                id,
                idRevue,
                titreRevue,
                dateCommande,
                dateFin,
                montant
            );

            abo.IdAbonnement.Should().Be(id);
            abo.IdRevue.Should().Be(idRevue);
            abo.TitreRevue.Should().Be(titreRevue);
            abo.DateCommande.Should().Be(dateCommande);
            abo.DateFinAbonnement.Should().Be(dateFin);
            abo.Montant.Should().BeApproximately(montant, 0.001);
        }

        [Test]
        public void AbonnementDeserialisation_RemplitToutesLesProprietes()
        {
            const string json = @"
            {
                ""idAbonnement"": ""00027"",
                ""idRevue"": ""10008"",
                ""titreRevue"": ""L'Obs"",
                ""dateCommande"": ""2025-04-10"",
                ""dateFinAbonnement"": ""2025-04-30"",
                ""montant"": ""30.0""
            }";

            var abo = JsonConvert.DeserializeObject<Abonnement>(json);

            abo.Should().NotBeNull();
            abo!.IdAbonnement.Should().Be("00027");
            abo.IdRevue.Should().Be("10008");
            abo.TitreRevue.Should().Be("L'Obs");
            abo.DateCommande.Should().Be(new DateTime(2025, 4, 10));
            abo.DateFinAbonnement.Should().Be(new DateTime(2025, 4, 30));
            abo.Montant.Should().BeApproximately(30.0, 0.001);
        }

        [Test]
        public void GetAbonnementsExpirantDans30Jours_LorsquUnExpireBientot_RetourneListeContenantLibelle()
        {
            var maintenant = DateTime.Now;
            var expirantBientot = new Abonnement("A1", "10007", "Telerama", maintenant.AddDays(-5), maintenant.AddDays(10), 10.0);
            var dejaExpire = new Abonnement("A2", "10007", "Telerama", maintenant.AddDays(-40), maintenant.AddDays(-10), 10.0);
            var expirantLointain = new Abonnement("A3", "10007", "Telerama", maintenant, maintenant.AddDays(60), 10.0);
            var listeAbonnements = new List<Abonnement> { expirantBientot, dejaExpire, expirantLointain };

            var resultat = Abonnement.GetAbonnementsExpirantDans30Jours(listeAbonnements);
            resultat.Should().ContainSingle().Which.Should().Contain("Telerama");
        }

        [Test]
        public void GetAbonnementsExpirantDans30Jours_SiAucunExpireDans30Jours_RetourneListeVide()
        {
            var maintenant = DateTime.Now;
            var dejaExpire = new Abonnement("B1", "10008", "L'Obs", maintenant.AddDays(-60), maintenant.AddDays(-30), 20.0);
            var expirantLointain = new Abonnement("B2", "10008", "L'Obs", maintenant, maintenant.AddDays(90), 20.0);

            var listeAbonnements = new List<Abonnement> { dejaExpire, expirantLointain };
            var resultat = Abonnement.GetAbonnementsExpirantDans30Jours(listeAbonnements);

            resultat.Should().BeEmpty();
        }
    }
}
