//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class PersoneelID
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public PersoneelID()
    {
        this.Bestellings = new HashSet<Bestelling>();
    }

    public int PersoneelsID { get; set; }
    public string Voornaam { get; set; }
    public string Achternaam { get; set; }
    public string Wachtwoord { get; set; }
    public string Username { get; set; }
    public int RoleID { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Bestelling> Bestellings { get; set; }
}
