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

namespace Individueel_Project_Project
{
    /// <summary>
    /// Interaction logic for HoofdMenu.xaml
    /// </summary>
    public partial class HoofdMenu : Window
    {
        string value;
        public HoofdMenu(string _value)
        {
            InitializeComponent();
            value = _value;
            txtKeuze.Text = value;
            string gebruiker = txtKeuze.Text;

            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var role = ctx.PersoneelIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                var naam = ctx.PersoneelIDs.Where(n => n.Username == value).Select(n => n.Voornaam).FirstOrDefault().ToString();
                //MessageBox.Show(Convert.ToString (role));
                txtKeuze.Text = Convert.ToString(naam);
                switch (role)
                {
                    case "1": break;
                    case "2": btnDatabeheer.Visibility = Visibility.Hidden; break;
                    case "3": btnDatabeheer.Visibility = Visibility.Hidden; break;
                    default:
                        break;
                }
                //MessageBox.Show(txtKeuze.Text);
            }
        }
        private void btnDatabeheer_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var role = ctx.PersoneelIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                var naam = ctx.PersoneelIDs.Where(n => n.Username == value).Select(n => n.Voornaam).FirstOrDefault().ToString();
                Beheer beheer = new Beheer(role);
                beheer.Show();
                this.Close();

            }
        }

        private void btnOverzicht_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var role = ctx.PersoneelIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                Overzicht overzicht = new Overzicht();
                overzicht.Show();
                this.Close();
            }
        }

        private void btnBestelling_Click(object sender, RoutedEventArgs e)
        {
            using (Magazijn2Entities ctx = new Magazijn2Entities())
            {
                var role = ctx.PersoneelIDs.Where(x => x.Username == value).Select(y => y.RoleID).FirstOrDefault().ToString();
                MessageBox.Show(role);
                Beheer beheer = new Beheer(role);
                beheer.Show();
                this.Close();

            }
        }
    }
}
