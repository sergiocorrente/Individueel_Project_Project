using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace Individueel_Project_Project
{
    /// <summary>
    /// Interaction logic for Beheer.xaml
    /// </summary>
    public partial class Beheer : Window
    {
        public Beheer(string role)
        {
            InitializeComponent();
            CategorieList();

            string selected = role;

            ProductenfillCombobox();
  

            if (selected == "1")
            {
                TabDatabeheer.IsEnabled = true;
                tabCategorie.IsEnabled = true;
                //tabKlanten.IsEnabled = true;
                //tabLeverancier.IsEnabled = true;
                tabProducten.IsEnabled = true;
                //tabBestellingen.IsEnabled = true;
                //tabBestellingProducten.IsEnabled = true;
                //tabJsonProducten.IsEnabled = true;
                MessageBox.Show("logged as admin");
            }
            else if (selected == "2")
            {
                TabDatabeheer.IsEnabled = true;
                tabPersoneel.IsEnabled = false;
                tabCategorie.IsEnabled = false;
                tabLeverancier.IsEnabled = false;
                tabProducten.IsEnabled = false;


                MessageBox.Show("logged as Verkoper");
            }
            else if (selected == "3")
            {
                MessageBox.Show("logged as Magazinier");
            }

        }



        private void PersoneelList()
        {
            cbRoleID.Items.Clear();
            cbRoleID.Items.Add("Beheerder");
            cbRoleID.Items.Add("Verkoper");
            cbRoleID.Items.Add("Magazinier");

            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                cbPersoneelID.ItemsSource = null;
                var personeelList = ctx.PersoneelIDs.Select(x => new { Username = x.Voornaam + " " + x.Achternaam + " " + x.Username, id = x.PersoneelsID }).ToList();
                cbPersoneelID.DisplayMemberPath = "Username";
                cbPersoneelID.SelectedValuePath = "id";
                cbPersoneelID.ItemsSource = personeelList;
                cbPersoneelID.SelectedIndex = 0;
                cbRoleID.ItemsSource = null;
                var listRoleID = ctx.PersoneelIDs.Select(x => x.RoleID).ToList();

            }
        }





        private void CategorieList()
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                cbCategorie.ItemsSource = null;
                var listCategorie = ctx.Categories.Select(x => x).ToList();
                cbCategorie.DisplayMemberPath = "CategorieNaam";
                cbCategorie.SelectedValuePath = "CategorieID";
                cbCategorie.ItemsSource = listCategorie;
                cbCategorie.SelectedIndex = 0;
            }
        }

        public void LeverancierList()
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                cbLeverancier.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbLeverancier.DisplayMemberPath = "Contactpersoon";
                cbLeverancier.SelectedValuePath = "LeverancierID";
                cbLeverancier.ItemsSource = listLeverancier;
                cbLeverancier.SelectedIndex = 0;
                //EditBestellingfillCombobox();
            }
        }


        private void ProductenList()
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                cbProducten.ItemsSource = null;
                var listProducten = ctx.Products.Select(x => x).ToList();
                cbProducten.DisplayMemberPath = "Naam";
                cbProducten.SelectedValuePath = "ProductID";
                cbProducten.ItemsSource = listProducten;
                cbProducten.SelectedIndex = 0;
                //                EditBestellingProductfillCombobox();
            }
        }

        private void ProductenfillCombobox()
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                cbLeverancier.ItemsSource = null;
                var listLeverancier = ctx.Leveranciers.Select(x => x).ToList();
                cbLeverancierProducten.DisplayMemberPath = "Contactpersoon";
                cbLeverancierProducten.SelectedValuePath = "LeverancierID";
                cbLeverancierProducten.ItemsSource = listLeverancier;
                cbLeverancierProducten.SelectedIndex = 0;
                var listCategorie = ctx.Categories.Select(x => x).ToList();
                cbCategorieProducten.DisplayMemberPath = "CategorieNaam";
                cbCategorieProducten.SelectedValuePath = "CategorieID";
                cbCategorieProducten.ItemsSource = listCategorie;
                cbCategorieProducten.SelectedIndex = 0;
            }
        }










        public string WachwoordEncryptie(string inWachtwoord)
        {
            string hash = "kcuf@BV";
            //byte[] data = Convert.FromBase64String(inWachtwoord);
            byte[] data = UTF8Encoding.UTF8.GetBytes(inWachtwoord);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    Byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    inWachtwoord = Convert.ToBase64String(results, 0, results.Length);
                }

            }
            return inWachtwoord;
        }

        public string WachwoordDecryptie(string inWachtwoord)
        {
            string hash = "kcuf@BV";
            byte[] data = Convert.FromBase64String(inWachtwoord);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    Byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    inWachtwoord = UTF32Encoding.UTF8.GetString(results);
                }
            }
            return inWachtwoord;
        }

        private void tbPersoneel_Checked(object sender, RoutedEventArgs e)
        {
            if (tbPersoneel.IsChecked == true)
            {
                btnPerBew.IsEnabled = true;
                btnPerVer.IsEnabled = true;
                btnPerToe.IsEnabled = false;
                PersoneelList();
                cbPersoneelID.IsEnabled = true;
                //txtAchternaamWoord.Visibility = Visibility.Hidden;
                //txtvoornaamWoord.Visibility = Visibility.Hidden;
                txtGebruikernaamWoord.Visibility = Visibility.Hidden;
                txtWachtwoordWoord.Visibility = Visibility.Hidden;
            }
            else if (tbPersoneel.IsChecked == false)
            {

                btnPerToe.IsEnabled = true;
                btnPerBew.IsEnabled = false;
                btnPerVer.IsEnabled = false;
                cbPersoneelID.IsEnabled = false;
                btnPerVer.IsEnabled = false;
                txtPasword.Text = "Wachtwoord";
                txtvoornaam.Text = " ";
                //txtvoornaamWoord.Text = " ";
                txtGebruikernaam.Text = " ";
                txtWachtwoordWoord.Text = " ";

                txtvoornaam.Visibility = Visibility.Visible;
                txtAchternaam.Visibility = Visibility.Visible;
                txtWachtwoordWoord.Visibility = Visibility.Visible;
                txtGebruikernaam.Visibility = Visibility.Visible;

            }
        }

        private void TabDatabeheer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabDatabeheer.SelectedIndex == TabDatabeheer.Items.Count - 1)
            {
                this.DialogResult = false;

            }
        }




        private void cbPersoneelID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cbRoleID.Items.Clear();
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                if (cbPersoneelID.SelectedValue != null)
                {  // String test= Convert.ToString(cbPersoneelID.SelectedValue);
                    //    MessageBox.Show(test);

                    if (ctx.PersoneelIDs.Single(x => x.PersoneelsID == (int)cbPersoneelID.SelectedValue) != null)
                    {
                        var geslectioneerdePersoneel = ctx.PersoneelIDs.Single(p => p.PersoneelsID == (int)cbPersoneelID.SelectedValue);
                    }
                    if (ctx.PersoneelIDs.Single(p => p.PersoneelsID == (int)cbPersoneelID.SelectedValue) != null)
                    {
                        string hash = "kcuf@BV";

                        var geslectioneerdePersoneel = ctx.PersoneelIDs.Single(p => p.PersoneelsID == (int)cbPersoneelID.SelectedValue);
                        txtvoornaam.Text = geslectioneerdePersoneel.Voornaam;
                        txtAchternaam.Text = geslectioneerdePersoneel.Achternaam;
                        txtGebruikernaam.Text = geslectioneerdePersoneel.Username;
                        string gebruikerNaam = geslectioneerdePersoneel.Username;
                        String pass = Convert.ToString(geslectioneerdePersoneel.Wachtwoord);
                        var wachtwoord = ctx.PersoneelIDs.Where(x => x.Username == gebruikerNaam).Select(x => x.Wachtwoord).FirstOrDefault().ToString();
                        txtPasword.Text = WachwoordDecryptie(wachtwoord);
                        cbRoleID.SelectedIndex = geslectioneerdePersoneel.RoleID - 1;
                    }
                }
            }

        }

        private void btnPerBew_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var gesectioneerdePersoneel = ctx.PersoneelIDs.Single(g => g.PersoneelsID == (int)cbPersoneelID.SelectedValue);

                gesectioneerdePersoneel.Voornaam = txtvoornaam.Text;
                gesectioneerdePersoneel.Achternaam = txtAchternaam.Text;
                int roleID = (int)cbRoleID.SelectedIndex + 1;
                //var listRoleID = ctx.PersoneelsIDs.Select(x => x.RoleID).ToList();
                gesectioneerdePersoneel.RoleID = cbRoleID.SelectedIndex + 1;
                if (txtPasword.Text != "")
                {
                    var passwoord = WachwoordEncryptie(txtPasword.Text);
                    gesectioneerdePersoneel.Wachtwoord = passwoord;
                }
                gesectioneerdePersoneel.Username = txtvoornaam.Text;
                gesectioneerdePersoneel.Achternaam = txtvoornaam.Text;
                gesectioneerdePersoneel.Username = txtGebruikernaam.Text;
                gesectioneerdePersoneel.RoleID = roleID;
                ctx.SaveChanges();
                MessageBox.Show("Is bewerkt");
                PersoneelList();
            }

        }

        private void cbRoleID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPerToe_Click(object sender, RoutedEventArgs e)
        {
            var wachtwoord = txtPasword.Text;
            string voornaam = txtvoornaam.Text;
            string achternaam = txtAchternaam.Text;
            //string wachtwwoord = WachwoordEncryptie(wachtwoord);
            int roleID = (int)cbRoleID.SelectedIndex + 1;
            string gebruikerNaam = txtGebruikernaam.Text;

            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                ctx.PersoneelIDs.Add(new PersoneelID()
                {
                    Voornaam = voornaam,
                    Achternaam = achternaam,
                    Wachtwoord = WachwoordEncryptie(wachtwoord),
                    RoleID = roleID,
                    Username = gebruikerNaam,

                });
                ctx.SaveChanges();
                MessageBox.Show("Is toegevoegt");
                PersoneelList();
            }
        }

        private void btnPerVer_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                ctx.PersoneelIDs.Remove(ctx.PersoneelIDs.Single(p => p.PersoneelsID == (int)cbPersoneelID.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Is verwijderd");
            PersoneelList();
        }




        private void btnToeCategorie_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                ctx.Categories.Add(new Categorie { CategorieNaam = txtCategorieToevoegen.Text });
                ctx.SaveChanges();
            }
            CategorieList();

            txtCategorieToevoegen.Text = "";
            MessageBox.Show("Is Toegevoegd");

        }

        private void tbProducten_Checked(object sender, RoutedEventArgs e)
        {
            if (tbProducten.IsChecked == true)
            {
                ProductenList();
                btnBewPro.IsEnabled = true;
                btnVerPro.IsEnabled = true;
                cbProducten.IsEnabled = true;
                btnToePro.IsEnabled = false;
            }
            else if (tbProducten.IsChecked == false)
            {
                btnBewPro.IsEnabled = false;
                btnVerPro.IsEnabled = false;
                cbProducten.IsEnabled = false;
                btnToePro.IsEnabled = true;
                txtNaamProducten.Text = "Naam";
                txtMargeProducten.Text = "Marge";
                txtEenheidProducten.Text = "Eenheid";
                txtBTWProducten.Text = "BTW";
                txtAantalProducten.Text = "0";
                ProductenfillCombobox();
            }
        }

        private void btnBewCat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnToevoegenProducten_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                ctx.Products.Add(new Product
                {
                    Naam = txtNaamProducten.Text,
                    Marge = Convert.ToDecimal(txtMargeProducten.Text),
                    Eenheid = txtEenheidProducten.Text,
                    BTW = Convert.ToDecimal(txtBTWProducten.Text),
                    LeverancierID = (int)cbLeverancierProducten.SelectedValue,
                    CategorieID = (int)cbCategorieProducten.SelectedValue,
                    AantalOpVooraad = Convert.ToInt32(txtAantalProducten.Text),
                });
                ctx.SaveChanges();
                MessageBox.Show("Toevoegen");
            }

        }

        private void btnToeLev_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusLeverancier.Text != "Bus")
            { bus = txtBusLeverancier.Text; }
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                try
                {
                    ctx.Leveranciers.Add(new Leverancier()
                    {
                        Contactpersoon = txtContactpersoonLeverancier.Text,
                        Telefoonnummer = txtTelefoonnummerLeverancier.Text,
                        Emailadres = txtEmailadresLeverancier.Text,
                        Straatnaam = txtStraatnaamLeverancier.Text,
                        Huisnummer = Convert.ToInt32(txtHuisnummerLeverancier.Text),
                        Bus = bus,
                        Postcode = txtPostLeverancier.Text,
                        Gemeente = txtGemeenteLeverancier.Text
                    });
                    ctx.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Je moet alles invullen");
                }
            }
            MessageBox.Show("Toevoegen");
            LeverancierList();
            ProductenfillCombobox();
        }

        private void btnBewLev_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusLeverancier.Text != "Bus")
            { bus = txtBusLeverancier.Text; }
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var selectedLeverancier = ctx.Leveranciers.Single(k => k.LeverancierID == (int)cbLeverancier.SelectedValue);

                selectedLeverancier.Contactpersoon = txtContactpersoonLeverancier.Text;
                selectedLeverancier.Telefoonnummer = txtTelefoonnummerLeverancier.Text;
                selectedLeverancier.Emailadres = txtEmailadresLeverancier.Text;
                selectedLeverancier.Straatnaam = txtStraatnaamLeverancier.Text;
                selectedLeverancier.Huisnummer = Convert.ToInt32(txtHuisnummerLeverancier.Text);
                selectedLeverancier.Bus = bus;
                selectedLeverancier.Postcode = txtPostLeverancier.Text;
                selectedLeverancier.Gemeente = txtGemeenteLeverancier.Text;
                ctx.SaveChanges();
            }
            MessageBox.Show("Edited");
            LeverancierList();
            ProductenfillCombobox();
        }

        private void btnVerLev_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                ctx.Leveranciers.Remove(ctx.Leveranciers.Single(p => p.LeverancierID == (int)cbLeverancier.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            LeverancierList();
            ProductenfillCombobox();
        }

        private void tbBewLev_Checked(object sender, RoutedEventArgs e)
        {
            if (tbBewLev.IsChecked == true)
            {
                LeverancierList();
                btnBewLev.IsEnabled = true;
                btnVerLev.IsEnabled = true;
                cbLeverancier.IsEnabled = true;
                btnToeLev.IsEnabled = false;
            }
            else if (tbBewLev.IsChecked == false)
            {
                btnBewLev.IsEnabled = false;
                btnVerLev.IsEnabled = false;
                cbLeverancier.IsEnabled = false;
                btnToeLev.IsEnabled = true;
                txtContactpersoonLeverancier.Text = "Contactpersoon";
                txtTelefoonnummerLeverancier.Text = "Telefoon nummer";
                txtEmailadresLeverancier.Text = "e-mail";
                txtStraatnaamLeverancier.Text = "Straatnaam";
                txtHuisnummerLeverancier.Text = "Huisnummer";
                txtBusLeverancier.Text = "Bus";
                txtPostLeverancier.Text = "Postcode";
                txtGemeenteLeverancier.Text = "Gemeente";
            }
        }

        private void cbLeverancier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                if (cbLeverancier.SelectedValue != null)
                {
                    if (ctx.Leveranciers.Single(l => l.LeverancierID == (int)cbLeverancier.SelectedValue) != null)
                    {
                        var selectedPersoneel = ctx.Leveranciers.Single(p => p.LeverancierID == (int)cbLeverancier.SelectedValue);
                        txtContactpersoonLeverancier.Text = selectedPersoneel.Contactpersoon;
                        txtTelefoonnummerLeverancier.Text = selectedPersoneel.Telefoonnummer;
                        txtEmailadresLeverancier.Text = selectedPersoneel.Emailadres;
                        txtStraatnaamLeverancier.Text = selectedPersoneel.Straatnaam;
                        txtHuisnummerLeverancier.Text = selectedPersoneel.Huisnummer.ToString();
                        if (selectedPersoneel.Bus != "")
                        { txtBusLeverancier.Text = selectedPersoneel.Bus; }
                        else
                        {//txtBusEditKlant.Text = "Bus"); 
                        }
                        txtPostLeverancier.Text = Convert.ToString(selectedPersoneel.Postcode);
                        txtPostLeverancier.Text = selectedPersoneel.Gemeente;
                    }
                }
            }
        }

        private void btnBewPro_Click(object sender, RoutedEventArgs e)
        {
            string bus = "";
            if (txtBusLeverancier.Text != "Bus")
            { bus = txtBusLeverancier.Text; }
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var selectedLeverancier = ctx.Leveranciers.Single(k => k.LeverancierID == (int)cbLeverancier.SelectedValue);

                selectedLeverancier.Contactpersoon = txtContactpersoonLeverancier.Text;
                selectedLeverancier.Telefoonnummer = txtTelefoonnummerLeverancier.Text;
                selectedLeverancier.Emailadres = txtEmailadresLeverancier.Text;
                selectedLeverancier.Straatnaam = txtStraatnaamLeverancier.Text;
                selectedLeverancier.Huisnummer = Convert.ToInt32(txtHuisnummerLeverancier.Text);
                selectedLeverancier.Bus = bus;
                selectedLeverancier.Postcode = txtPostLeverancier.Text;
                selectedLeverancier.Gemeente = txtGemeenteLeverancier.Text;
                ctx.SaveChanges();
            }
            MessageBox.Show("Edited");
            LeverancierList();
            ProductenfillCombobox();
        }

        private void btnVerPro_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                ctx.Products.Remove(ctx.Products.Single(p => p.ProductID == (int)cbProducten.SelectedValue));
                ctx.SaveChanges();
            }
            MessageBox.Show("Deleted");
            ProductenList();
        }

        private void cbProducten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                if (cbProducten.SelectedValue != null)
                {
                    if (ctx.Products.Single(p => p.ProductID == (int)cbProducten.SelectedValue) != null)
                    {
                        var selectedProduct = ctx.Products.Single(p => p.ProductID == (int)cbProducten.SelectedValue);
                        txtNaamProducten.Text = selectedProduct.Naam;
                        txtMargeProducten.Text = selectedProduct.Marge.ToString();
                        txtEenheidProducten.Text = selectedProduct.Eenheid;
                        txtBTWProducten.Text = selectedProduct.BTW.ToString();
                        cbLeverancierProducten.SelectedValue = selectedProduct.LeverancierID;
                        cbCategorieProducten.SelectedValue = selectedProduct.CategorieID;
                        txtAantalProducten.Text = selectedProduct.AantalOpVooraad.ToString();
                    }
                }
            }
        }

        private void cbLeverancierProducten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                if (cbLeverancier.SelectedValue != null)
                {
                    if (ctx.Leveranciers.Single(l => l.LeverancierID == (int)cbLeverancier.SelectedValue) != null)
                    {
                        var selectedPersoneel = ctx.Leveranciers.Single(p => p.LeverancierID == (int)cbLeverancier.SelectedValue);
                        txtContactpersoonLeverancier.Text = selectedPersoneel.Contactpersoon;
                        txtTelefoonnummerLeverancier.Text = selectedPersoneel.Telefoonnummer;
                        txtEmailadresLeverancier.Text = selectedPersoneel.Emailadres;
                        txtStraatnaamLeverancier.Text = selectedPersoneel.Straatnaam;
                        txtHuisnummerLeverancier.Text = selectedPersoneel.Huisnummer.ToString();
                        if (selectedPersoneel.Bus != "")
                        { txtBusLeverancier.Text = selectedPersoneel.Bus; }
                        else
                        {//txtBusEditKlant.Text = "Bus"); 
                        }
                        txtPostLeverancier.Text = Convert.ToString(selectedPersoneel.Postcode);
                        txtPostLeverancier.Text = selectedPersoneel.Gemeente;
                    }
                }
            }
        }
    
    }
}

   

       
  
       
   
