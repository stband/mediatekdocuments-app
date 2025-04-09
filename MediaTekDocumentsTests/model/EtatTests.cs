using NUnit.Framework;
using FluentAssertions;
using MediaTekDocuments.model;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class EtatTests
    {
        [Test]
        public void Constructeur_Etat_InitialiseToutesLesProprietes()
        {
            var id = "00002";
            var libelle = "usagé";

            var etat = new Etat(id, libelle);

            etat.Id.Should().Be(id);
            etat.Libelle.Should().Be(libelle);
        }
    }
}
