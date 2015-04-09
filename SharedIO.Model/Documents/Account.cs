using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AspNet.Identity.RavenDB.Entities;
using Raven.Abstractions.Spatial;

namespace SharedIO.Model
{

    // NB  Microsoft.AspNet.Identity.IUser defines
    //        public string Id { get; set; }
    //        public string UserName { get; set; }
    //        public string PasswordHash { get; set; }
    //        public string SecurityStamp { get; set; }

    // and  RavenUser : User provides above as well as 
    //
    //        public ICollection<RavenUserClaim> Claims { get; set; }
    //        public ICollection<RavenUserLogin> Logins { get; set; }

    // You can add profile data for the user by adding more properties to your Member class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Account : RavenUser
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Locality { get; set; }
        public DateTime Created { get; set; }
        public string Portrait { get; set; }
        public string About { get; set; }

        public static string LongId(string id)
        {
            return "account/" + id;
        }
    }
}