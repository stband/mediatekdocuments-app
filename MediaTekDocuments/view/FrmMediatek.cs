using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using MediaTekDocuments.helper;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region constantes de la classe

        const string ETATNEUF = "00001";
        private const string MessageInformation = "Information";

        #endregion

        #region bindingsource et données locales

        // BindingSource pour centraliser la liaison des données pour chaque onglet.
        private readonly BindingSource bindingSourceLivre = [];
        private readonly BindingSource bindingSourceDvd = [];
        private readonly BindingSource bindingSourceRevues = [];
        private readonly BindingSource bindingSourceExemplaires = [];

        // BindingSource spécifique à certaines catégories lors d'un tri.
        private readonly BindingSource bindingSourceGenre = [];
        private readonly BindingSource bindingSourcePublic = [];
        private readonly BindingSource bindingSourceRayon = [];

        // Copie en mémoire pour reset les listes originales après un trie ou un filtre.
        private List<Livre> livresOriginaux = [];
        private List<Dvd> dvdOriginaux = [];
        private List<Revue> revuesOriginaux = [];
        private List<Exemplaire> exemplairesOriginaux = [];

        // Contrôleur du formulaire unique
        private readonly FrmMediatekController controller;

        #endregion

        #region Méthodes communes - centralisation logique

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire.
        /// </summary>
        internal FrmMediatek()
        {
            InitializeComponent();
            controller = new FrmMediatekController();
        }

        /// <summary>
        /// Remplit un ComboBox (genre, public ou rayon) à partir d'une liste de catégories,
        /// et réinitialise la sélection.
        /// </summary>
        /// <param name="lesCategories">Liste des catégories à afficher (Genre, Public ou Rayon).</param>
        /// <param name="bdg">BindingSource utilisé pour gérer l’affichage des données.</param>
        /// <param name="cbx">ComboBox à alimenter.</param>
        public static void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre une liste d'objets en fonction d’une ComboBox de Categorie (Genre, Public ou Rayon),
        /// vide les TextBox de recherche et réinitialise les autres ComboBox passées en paramètre.
        /// </summary>
        /// <typeparam name="T">Type des éléments de la liste (Livre, Dvd, Revue).</typeparam>
        /// <param name="cb">Le ComboBox sur lequel on applique le filtre.</param>
        /// <param name="listeAfiltrer">La liste complète des éléments à filtrer.</param>
        /// <param name="categorieAFiltrer">Fonction qui renvoie la propriété de catégorie (l => l.Genre / l.Public / l.Rayon) qu'on va filtrer.</param>
        /// <param name="remplirListe">Action qui remet la DataGridView à jour (RemplirLivresListe, RemplirDvdListe, etc.).</param>
        /// <param name="txbTitreRecherche">Texbox qui contient le titre de la recherche.</param>
        /// <param name="autresCbAReset">Les autres ComboBox à remettre à -1 lors du filtrage.</param>
        private static void FiltrerParCategorie<T>(
            ComboBox cb,
            List<T> listeAfiltrer,
            Func<T, string> categorieAFiltrer,
            Action<List<T>> remplirListe,
            TextBox txbTitreRecherche,
            params ComboBox[] autresCbAReset
        )
        {
            // Pattern‐matching gère le cast et la null‐safety sans le préciser autrement.
            if (cb.SelectedItem is not Categorie categorie)
                return;

            txbTitreRecherche.Clear();

            var listeFiltree = listeAfiltrer
                .Where(x => !string.IsNullOrEmpty(categorieAFiltrer(x)) && string.Equals(categorieAFiltrer(x), categorie.Libelle, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Mise à jour du DataGridView.
            remplirListe(listeFiltree);

            foreach (var autresCb in autresCbAReset)
                autresCb.SelectedIndex = -1;
        }

        /// <summary>
        /// Réinitialise les TextBox, ComboBox et PictureBox passés en paramètre.
        /// C'est la méthode générale qui permet de réinitialiser des contrôles dans le formulaire.
        /// </summary>
        /// <param name="controles">Contrôles à vider ou réinitialiser.</param>
        private static void ResetControles(params Control[] controles)
        {
            foreach (var controle in controles)
            {
                switch (controle)
                {
                    case ComboBox cb: cb.SelectedIndex = -1; break;
                    case TextBox tb: tb.Clear(); break;
                    case PictureBox pb:
                        pb.Image?.Dispose();
                        pb.Image = null;
                        break;
                }
            }
        }

        #endregion

        #region Onglet 1 — Livres

        /// <summary>
        /// Chargement initial de l'onglet Livres:
        /// récupère les données depuis le contrôleur et initialise
        /// le DataGridView ainsi que les ComboBox de filtres (genre, public, rayon).
        /// </summary>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            livresOriginaux = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bindingSourceGenre, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bindingSourcePublic, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bindingSourceRayon, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le DataGridView des livres avec la liste fournie,
        /// configure le tri automatique sur les colonnes,
        /// masque les colonnes techniques et ajuste l'affichage.
        /// </summary>
        /// <param name="livres">Liste de livres à afficher.</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            var sortableLivres = new SortableBindingList<Livre>(livres);
            bindingSourceLivre.DataSource = sortableLivres;
            dgvLivresListe.DataSource = bindingSourceLivre;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvLivresListe.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;

            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Filtre dynamiquement la liste des livres en fonction du texte saisi dans la zone de recherche
        /// et met à jour le DataGridView associé.  
        /// Réinitialise également les filtres de genre, public et rayon avant d’appliquer le filtre.
        /// </summary>
        /// <param name="sender">La TextBox de recherche dont le contenu a changé.</param>
        /// <param name="e">Arguments de l’événement TextChanged.</param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            string texte = txbLivresTitreRecherche.Text.Trim();

            if (string.IsNullOrEmpty(texte))
            {
                // Remet la liste complète si champ vide.
                bindingSourceLivre.DataSource = new SortableBindingList<Livre>(livresOriginaux);
            }
            else
            {
                ResetControles(cbxLivresGenres, cbxLivresPublics, cbxLivresRayons);
                UIHelper.FiltrerBindingSource(bindingSourceLivre, txbLivresTitreRecherche, livresOriginaux, "Id", "Titre", "Auteur", "Genre", "Public", "Collection", "Rayon");
            }
        }

        /// <summary>
        /// Affiche les informations du livre sélectionné.
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Réinitialise les champs d'affichage des informations d'un livre,
        /// en vidant les TextBox et en supprimant l'image affichée.
        /// </summary>
        private void VideLivresInfos()
        {
            ResetControles(
                txbLivresAuteur,
                txbLivresCollection,
                txbLivresImage,
                txbLivresIsbn,
                txbLivresNumero,
                txbLivresGenre,
                txbLivresPublic,
                txbLivresRayon,
                txbLivresTitre,
                pcbLivresImage
            );
        }

        /// <summary>
        /// Filtre la liste des livres par genre et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxLivresGenres, livresOriginaux, livre => livre.Genre, RemplirLivresListe, txbLivresTitreRecherche, cbxLivresPublics, cbxLivresRayons);
        }

        /// <summary>
        /// Filtre la liste des livres par public et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxLivresPublics, livresOriginaux, livre => livre.Public, RemplirLivresListe, txbLivresTitreRecherche, cbxLivresGenres, cbxLivresRayons);
        }

        /// <summary>
        /// Filtre la liste des livres par rayon et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxLivresRayons, livresOriginaux, livre => livre.Rayon, RemplirLivresListe, txbLivresTitreRecherche, cbxLivresGenres, cbxLivresPublics);
        }

        /// <summary>
        /// Gère la sélection d'un livre dans le DataGridView.
        /// Si aucun livre n'est sélectionné, vide les zones d'affichage ; 
        /// sinon, affiche les informations du livre sélectionné.
        /// </summary>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            // Si l'élément courant lié au BindingSource n'est pas un Livre, l'affichage est vidé.
            if (bindingSourceLivre.Current is not Livre livre)
            {
                VideLivresInfos();
                return;
            }

            AfficheLivresInfos(livre);
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie public des livres et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie rayon des livres et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie genre des livres et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres.
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(livresOriginaux);
            VideLivresZones();
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre sélectionné.
        /// </summary>
        private void VideLivresZones()
        {
            ResetControles(cbxLivresGenres, cbxLivresPublics, cbxLivresRayons, txbLivresTitreRecherche);
        }

        #endregion

        #region Onglet 2 — Dvd

        /// <summary>
        /// Chargement initial de l'onglet DVD :
        /// récupère les données depuis le contrôleur et initialise
        /// le DataGridView ainsi que les ComboBox de filtres (genre, public, rayon).
        /// </summary>
        private void TabDvd_Enter(object sender, EventArgs e)
        {
            dvdOriginaux = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bindingSourceGenre, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bindingSourcePublic, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bindingSourceRayon, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le DataGridView des DVD avec la liste fournie,
        /// configure le tri automatique sur les colonnes,
        /// masque les colonnes techniques et ajuste l'affichage.
        /// </summary>
        /// <param name="livres">Liste de DVD à afficher.</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            var sortableDvd = new SortableBindingList<Dvd>(Dvds);
            bindingSourceDvd.DataSource = sortableDvd;
            dgvDvdListe.DataSource = bindingSourceDvd;

            foreach (DataGridViewColumn col in dgvDvdListe.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;

            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Filtre dynamiquement la liste des DVD en fonction du texte saisi dans la zone de recherche
        /// et met à jour le DataGridView associé.  
        /// Réinitialise également les filtres de genre, public et rayon avant d’appliquer le filtre.
        /// </summary>
        /// <param name="sender">La TextBox de recherche dont le contenu a changé.</param>
        /// <param name="e">Arguments de l’événement TextChanged.</param>
        private void TxbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            string texte = txbDvdTitreRecherche.Text.Trim();

            if (string.IsNullOrEmpty(texte))
            {
                // Remet la liste complète si champ vide.
                bindingSourceDvd.DataSource = new SortableBindingList<Dvd>(dvdOriginaux);
            }
            else
            {
                ResetControles(cbxDvdGenres, cbxDvdPublics, cbxDvdRayons);
                UIHelper.FiltrerBindingSource(bindingSourceDvd, txbDvdTitreRecherche, dvdOriginaux, "Id", "Titre", "Realisateur", "Genre", "Public", "Rayon", "Duree");
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Réinitialise les champs d'affichage des informations d'un DVD,
        /// en vidant les TextBox et en supprimant l'image affichée.
        /// </summary>
        private void VideDvdInfos()
        {
            ResetControles(
                txbDvdRealisateur,
                txbDvdSynopsis,
                txbDvdImage,
                txbDvdDuree,
                txbDvdNumero,
                txbDvdGenre,
                txbDvdPublic,
                txbDvdRayon,
                txbDvdTitre,
                pcbDvdImage
            );
        }

        /// <summary>
        /// Filtre la liste des DVD par genre et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxDvdGenres, dvdOriginaux, dvd => dvd.Genre, RemplirDvdListe, txbDvdTitreRecherche, cbxDvdPublics, cbxDvdRayons);
        }

        /// <summary>
        /// Filtre la liste des DVD par public et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxDvdPublics, dvdOriginaux, dvd => dvd.Public, RemplirDvdListe, txbDvdTitreRecherche, cbxDvdGenres, cbxDvdRayons);
        }

        /// <summary>
        /// Filtre la liste des DVD par rayon et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxDvdRayons, dvdOriginaux, dvd => dvd.Rayon, RemplirDvdListe, txbDvdTitreRecherche, cbxDvdGenres, cbxDvdPublics);
        }

        /// <summary>
        /// Gère la sélection d'un dvd dans le DataGridView.
        /// Si aucun dvd n'est sélectionné, vide les zones d'affichage ; 
        /// sinon, affiche les informations du dvd sélectionné.
        /// </summary>
        private void DgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (bindingSourceDvd.Current is not Dvd dvd)
            {
                VideDvdInfos();
                return;
            }

            AfficheDvdInfos(dvd);
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie public des DVD et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie rayon des DVD et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie genre des DVD et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres.
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(dvdOriginaux);
            VideDvdZones();
        }

        /// <summary>
        /// Vide les zones de recherche et de filtre des DVD.
        /// </summary>
        private void VideDvdZones()
        {
            ResetControles(cbxDvdGenres, cbxDvdRayons, cbxDvdPublics, txbDvdTitreRecherche);
        }
        #endregion

        #region Onglet 3 - Revues

        /// <summary>
        /// Chargement initial de l'onglet revues :
        /// récupère les données depuis le contrôleur et initialise
        /// le DataGridView ainsi que les ComboBox de filtres (genre, public, rayon).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabRevues_Enter(object sender, EventArgs e)
        {
            revuesOriginaux = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bindingSourceGenre, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bindingSourcePublic, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bindingSourceRayon, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le DataGridView des revues avec la liste fournie,
        /// configure le tri automatique sur les colonnes,
        /// masque les colonnes techniques et ajuste l'affichage.
        /// </summary>
        /// <param name="livres">Liste des revues à afficher.</param>
        private void RemplirRevuesListe(List<Revue> revues)
        {

            var sortableRevues = new SortableBindingList<Revue>(revues);
            bindingSourceRevues.DataSource = sortableRevues;
            dgvRevuesListe.DataSource = bindingSourceRevues;

            // Assurer le tri automatique pour chaque colonne.
            foreach (DataGridViewColumn col in dgvRevuesListe.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;

            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Filtre dynamiquement la liste des revues en fonction du texte saisi dans la zone de recherche
        /// et met à jour le DataGridView associé.  
        /// Réinitialise également les filtres de genre, public et rayon avant d’appliquer le filtre.
        /// </summary>
        /// <param name="sender">La TextBox de recherche dont le contenu a changé.</param>
        /// <param name="e">Arguments de l’événement TextChanged.</param>
        private void TxbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            string texte = txbRevuesTitreRecherche.Text.Trim();

            if (string.IsNullOrEmpty(texte))
            {
                // Remet la liste complète si champ vide.
                bindingSourceRevues.DataSource = new SortableBindingList<Revue>(revuesOriginaux);
            }
            else
            {
                ResetControles(cbxRevuesGenres, cbxRevuesPublics, cbxRevuesRayons);
                UIHelper.FiltrerBindingSource(bindingSourceRevues, txbRevuesTitreRecherche, revuesOriginaux, "Id", "Titre", "Periodicite", "DelaiMiseADispo", "Public", "Rayon", "Genre");
            }
        }

        /// <summary>
        /// Affichage les informations de la revue sélectionnée.
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Réinitialise les champs d'affichage des informations d'une revue,
        /// en vidant les TextBox et en supprimant l'image affichée.
        /// </summary>
        private void VideRevuesInfos()
        {
            ResetControles(
                txbRevuesPeriodicite,
                txbRevuesImage,
                txbRevuesNumero,
                txbRevuesGenre,
                txbRevuesPublic,
                txbRevuesRayon,
                txbRevuesTitre,
                txbRevuesDateMiseADispo,
                pcbRevuesImage
            );
        }

        /// <summary>
        /// Filtre la liste des revues par genre et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxRevuesGenres, revuesOriginaux, revue => revue.Genre, RemplirRevuesListe, txbRevuesTitreRecherche, cbxRevuesPublics, cbxRevuesRayons);
        }

        /// <summary>
        /// Filtre la liste des revues par public et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxRevuesPublics, revuesOriginaux, revue => revue.Public, RemplirRevuesListe, txbRevuesTitreRecherche, cbxRevuesGenres, cbxRevuesRayons);
        }

        /// <summary>
        /// Filtre la liste des revues par rayon et met à jour le DataGridView
        /// en utilisant <see cref="FiltrerParCategorie{T}(ComboBox,List{T},Func{T,string},Action{List{T}},TextBox,ComboBox[])"/>.
        /// </summary>
        /// <param name="sender">Le ComboBox déclencheur.</param>
        /// <param name="e">Les arguments de l'événement.</param>
        private void CbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrerParCategorie(cbxRevuesRayons, revuesOriginaux, revue => revue.Rayon, RemplirRevuesListe, txbRevuesTitreRecherche, cbxRevuesGenres, cbxRevuesPublics);
        }

        /// <summary>
        /// Gère la sélection d'une revue dans le DataGridView.
        /// Si aucune revue n'est sélectionné, vide les zones d'affichage ; 
        /// sinon, affiche les informations de la revue sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (bindingSourceRevues.Current is not Revue revue)
            {
                VideRevuesInfos();
                return;
            }

            AfficheRevuesInfos(revue);
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie public des revues et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie rayon des revues et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Réinitialise le filtre sur la catégorie genre des revues et recharge la liste complète.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres.
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(revuesOriginaux);
            VideRevuesZones();
        }

        /// <summary>
        /// Vide les zones de recherche et de filtre des revues.
        /// </summary>
        private void VideRevuesZones()
        {
            ResetControles(cbxRevuesGenres, cbxRevuesPublics, cbxRevuesRayons, txbRevuesTitreRecherche);
        }

        #endregion

        #region Onglet 4 - Parutions

        /// <summary>
        /// Chargement initial de l'onglet reception revue :
        /// récupère les données depuis le contrôleur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabReceptionRevue_Enter(object sender, EventArgs e)
        {
            revuesOriginaux = controller.GetAllRevues();
            ResetControles(txbReceptionRevueNumero);
        }

        /// <summary>
        /// Remplit le DataGridView des exemplaires avec la liste fournie,
        /// masque les colonnes techniques et ajuste l'affichage.
        /// </summary>
        /// <param name="livres">Liste des exemplaires à afficher.</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire>? exemplaires)
        {
            if (exemplaires != null)
            {
                bindingSourceExemplaires.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bindingSourceExemplaires;

                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;

                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bindingSourceExemplaires.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceptionRechercher_Click(object sender, EventArgs e)
        {
            string numeroRecherche = txbReceptionRevueNumero.Text;

            if (string.IsNullOrWhiteSpace(numeroRecherche))
            {
                MessageBox.Show("Veuillez saisir un numéro de revue.", MessageInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var revue = revuesOriginaux.Find(x => string.Equals(x.Id, numeroRecherche, StringComparison.Ordinal));

            if (revue is not null)
            {
                AfficheReceptionRevueInfos(revue);
            }
            else
            {
                MessageBox.Show($"Le numéro '{numeroRecherche}' est introuvable.", MessageInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            ResetControles(
                txbReceptionRevuePeriodicite,
                txbReceptionRevueImage,
                txbReceptionRevueDelaiMiseADispo,
                txbReceptionRevueGenre,
                txbReceptionRevuePublic,
                txbReceptionRevueRayon,
                txbReceptionRevueTitre,
                pcbReceptionRevueImage
            );
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires.
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue.
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            exemplairesOriginaux = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(exemplairesOriginaux);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques.
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            ResetControles(txbReceptionExemplaireImage, txbReceptionExemplaireNumero, pcbReceptionExemplaireImage);
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    
                    Exemplaire exemplaire = new(numero, dateAchat, photo, idEtat, idDocument);
                    
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", MessageInformation);
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", MessageInformation);
            }
        }

        /// <summary>
        /// Tri sur une colonne.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = [];
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = exemplairesOriginaux.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = exemplairesOriginaux.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = exemplairesOriginaux.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// Affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (bindingSourceExemplaires.Current is not Exemplaire exemplaire)
            {
                pcbReceptionExemplaireRevueImage.Image = null;
                return;
            }

            string image = exemplaire.Photo;

            if (string.IsNullOrWhiteSpace(image) || !File.Exists(image))
            {
                pcbReceptionExemplaireRevueImage.Image = null;
                return;
            }

            try
            {
                pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }

        #endregion
    }
}
