//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Courses.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trainee_Courses
    {
        public int Trainnee_id { get; set; }
        public int Course_id { get; set; }
        public System.DateTime Registration_Date { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}
