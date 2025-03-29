using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace MediaTekDocuments.helper
{
    /// <summary>
    /// Classe générique qui étend BindingList pour ajouter la fonctionnalité de tri.
    /// Une fois utilisée comme source de données dans un BindingSource, 
    /// le DataGridView pourra trier automatiquement les colonnes.
    /// C'est la classe utilisée pour trier "basiquement" tous les DataGridViews de l'application.
    /// Voir un exemple de l'implémentation dans : view.FrmAbonnementRevues.cs -> LoadAbonnements().
    /// Il suffit de copier et coller la logique dans tous les DGV et l'adapter.
    /// Pour implémenter une nouvelle logique de tri il est intéressant de créer une nouvelle classe
    /// qui hérite de SortableBindingList, et de surcharger la méthode ApplySoftCore.
    /// </summary>
    /// <typeparam name="T">Type d'objet contenu dans la liste</typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public SortableBindingList() : base() { }

        /// <summary>
        /// Constructeur qui initialise la liste avec une collection d'objets.
        /// </summary>
        /// <param name="enumerable">Collection d'objets à ajouter à la liste.</param>
        public SortableBindingList(IEnumerable<T> enumerable) : base(enumerable.ToList()) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSortedValue;
        protected override ListSortDirection SortDirectionCore => sortDirectionValue;
        protected override PropertyDescriptor SortPropertyCore => sortPropertyValue;

        /// <summary>
        /// Applique le tri sur la liste en fonction de la propriété et de la direction fournies.
        /// </summary>
        /// <param name="prop">La propriété sur laquelle trier</param>
        /// <param name="direction">La direction du tri (Ascending ou Descending)</param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;
            if (items != null)
            {
                var sortedItems = direction == ListSortDirection.Ascending ?
                    items.OrderBy(x => prop.GetValue(x)) :
                    items.OrderByDescending(x => prop.GetValue(x));

                List<T> sortedList = sortedItems.ToList();

                for (int i = 0; i < sortedList.Count; i++)
                {
                    this.Items[i] = sortedList[i];
                }

                isSortedValue = true;
                sortDirectionValue = direction;
                sortPropertyValue = prop;

                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        /// <summary>
        /// Annule le tri en réinitialisant les propriétés de tri.
        /// </summary>
        protected override void RemoveSortCore()
        {
            isSortedValue = false;
            sortPropertyValue = null;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}