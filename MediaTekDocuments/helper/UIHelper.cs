using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// Filtre dynamiquement une BindingSource sur les propriétés spécifiées.
        /// Réaffecte la liste d'origine si le champ de recherche est vide.
        /// </summary>
        /// <typeparam name="T">Type d'objet contenu dans la BindingSource</typeparam>
        /// <param name="bindingSource">La source de données du DataGridView</param>
        /// <param name="txtRecherche">Le TextBox contenant le texte de recherche</param>
        /// <param name="originalList">La liste d'origine complète des objets</param>
        /// <param name="colonnesAFiltrer">Les noms des propriétés à filtrer</param>
        public static void FiltrerBindingSource<T>(BindingSource bindingSource, TextBox txtRecherche, IEnumerable<T> originalList, params string[] colonnesAFiltrer)
        {
            string texteRecherche = txtRecherche.Text.ToLower();

            if (string.IsNullOrWhiteSpace(texteRecherche))
            {
                // Réaffecter la liste d'origine si le champ est vide.
                bindingSource.DataSource = new BindingList<T>(originalList.ToList());
            }
            else
            {
                // Filtrer la liste d'origine sur les propriétés spécifiées.
                var listeFiltree = originalList
                    .Where(obj => colonnesAFiltrer.Any(col =>
                    {
                        var prop = typeof(T).GetProperty(col);
                        var value = prop?.GetValue(obj, null)?.ToString();
                        return !string.IsNullOrEmpty(value) && value.ToLower().Contains(texteRecherche);
                    }))
                    .ToList();
                bindingSource.DataSource = new BindingList<T>(listeFiltree);
            }
        }
    }
}
