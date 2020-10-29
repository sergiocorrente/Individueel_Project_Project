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
    /// Interaction logic for Overzicht.xaml
    /// </summary>
    public partial class Overzicht : Window
    {
        public Overzicht()
        {
            InitializeComponent();
            using (Magazijn2Entities dc = new Magazijn2Entities())
            {
                var bestellingen = dc.BestellingProducts.Select(bp => new { BestellingsNummer = bp.BestellingID, DateOpmaak = bp.Bestelling.DatumOpgemaakt, Personeel = bp.Bestelling.PersoneelID.Voornaam + " " + bp.Bestelling.PersoneelID.Achternaam, Producten = dc.BestellingProducts.Where(x => x.BestellingID == bp.BestellingID).Select(x => x.AantalProductBesteld + " " + x.Product.Naam + " " + x.Product.Eenheid + " €" ) }).GroupBy(x => x.BestellingsNummer);
                ListViewBestellingen.ItemsSource = bestellingen.ToList();
            }
        }
    }
}
