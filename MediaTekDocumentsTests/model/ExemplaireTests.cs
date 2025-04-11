using System;
using NUnit.Framework;
using FluentAssertions;

namespace MediaTekDocuments.model.Tests
{
    [TestFixture]
    public class ExemplaireTests
    {
        [Test]
        public void Constructeur_Exemplaire_InitialiseToutesLesProprietes()
        {
            var numero = 1;
            var dateAchat = new DateTime(2025, 3, 27, 0, 0, 0, DateTimeKind.Local);
            var photo = "";
            var idEtat = "00001";
            var idDocument = "00001";

            var ex = new Exemplaire(
                numero,
                dateAchat,
                photo,
                idEtat,
                idDocument
            );

            ex.Numero.Should().Be(numero);
            ex.DateAchat.Should().Be(dateAchat);
            ex.Photo.Should().Be(photo);
            ex.IdEtat.Should().Be(idEtat);
            ex.Id.Should().Be(idDocument);
        }
    }
}
