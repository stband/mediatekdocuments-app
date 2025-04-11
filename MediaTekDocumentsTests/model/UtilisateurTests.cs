using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class UtilisateurTests
    {
        [Test]
        public void Constructeur_Utilisateur_InitialiseToutesLesProprietes()
        {
            var id = 3;
            var nom = "Durand";
            var login = "administratif";
            var service = "Administratif";

            var user = new Utilisateur(id, nom, login, service);

            user.Id.Should().Be(id);
            user.Nom.Should().Be(nom);
            user.Login.Should().Be(login);
            user.Service.Should().Be(service);
        }
    }
}
