using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.helper
{
    public static class UIHelper
    {
        /// <summary>
        /// Gère l'événement Enter d'une TextBox : efface le texte par défaut (placeholder) si celui-ci est présent.
        /// </summary>
        /// <param name="textBox">La TextBox concernée.</param>
        /// <param name="texteParDefaut">Le texte par défaut à vérifier (exemple : "Rechercher").</param>
        public static void GererEntreeTextBox(TextBox textBox, string texteParDefaut)
        {
            if (textBox.Text == texteParDefaut)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Gère l'événement Leave d'une TextBox : restaure le texte par défaut si la TextBox est vide.
        /// </summary>
        /// <param name="textBox">La TextBox concernée.</param>
        /// <param name="texteParDefaut">Le texte par défaut à réafficher (exemple : "Rechercher").</param>
        public static void GererSortieTextBox(TextBox textBox, string texteParDefaut)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = texteParDefaut;
                textBox.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Réinitialise un TextBox à son état par défaut.
        /// Peut être utilisée lors d'un clic sur un bouton Clear.
        /// </summary>
        /// <param name="textBox">La TextBox à réinitialiser.</param>
        /// <param name="texteParDefaut">Le texte par défaut (exemple : "Rechercher").</param>
        public static void ClearTextBox(TextBox textBox, string texteParDefaut)
        {
            textBox.Text = texteParDefaut;
            textBox.ForeColor = Color.Gray;
        }
    }
}
