//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCoaches.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commentaire
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commentaire()
        {
            this.Reponses = new HashSet<Reponse>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> DateComnt { get; set; }
        public string TextComnt { get; set; }
        public Nullable<int> C_idPost { get; set; }
        public Nullable<int> C_idPersonne { get; set; }
    
        public virtual Personne Personne { get; set; }
        public virtual Post Post { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reponse> Reponses { get; set; }
    }
}
