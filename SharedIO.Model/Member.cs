using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AspNet.Identity.RavenDB.Entities;
using Raven.Abstractions.Spatial;

namespace SharedIO.Model
{
    // You can add profile data for the user by adding more properties to your Member class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Member : RavenUser, IMemberProfile
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal[] geo { get; set; }
        public DateTime created { get; set; }
        public string portrait { get; set; }
        public string locality { get; set; }
        public decimal balance { get; }
        public string aboutme { get; set; }
    }

    // NB  Microsoft.AspNet.Identity.IUser defines
    //        public string Id { get; set; }
    //        public string UserName { get; set; }
    //        public string PasswordHash { get; set; }
    //        public string SecurityStamp { get; set; }

    // and  RavenUser : User provides above as well as 
    //
    //        public ICollection<RavenUserClaim> Claims { get; set; }
    //        public ICollection<RavenUserLogin> Logins { get; set; }


    public interface IMemberProfile
    {
        // SPECCHANGE: id is implied to be an int in the spec but I think string is more general, 
        //             and would leaves it as implementation detail, which can be v important with things like ids
        string id { get; set; } 
        string name { get; set; }
        decimal[] geo { get; set; } // SPECNOTE: called 'geo' in spec but 'coordinates' would match GeoJSON.org
        DateTime created { get; set; } // IMPLNOTE: serialized to a javascript datetime like '1297373635' as serialization detail
        string portrait { get; set; } //  SPECNOTE: should this be portrait_url or profile_image_url a la twitter profiles?
        string locality { get; set; } //  SPECNOTE: or we could make the location above a geoJSon object with optional coordinates
        decimal balance { get; } // SPECNOTE: read only for obvious reasons. Really this is ViewModel property (derived from transactions) not a core model property so doesn't really belong here 

        string aboutme { get; set; } //SPECNOTE: I think I would prefer just 'about'

        // add these later

//        ICollection<CategoryStub> categories { get; set; } //SPECNOTE: whats the difference between a category and a tag?
//        ICollection<TagStub> tags { get; set; }



    }

    
}