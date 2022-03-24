using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Base;

namespace Core.Entities
{
    public class User : IdentityUser, IEntity, ITiming
    {
        public string ProfilePicture { get; set; } = "defaultProfile.png";
        public string FullName { get; set; }

        public string Description { get; set; }

        #region Navigator

        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<Blog> Blogs { get; set; }

        #endregion

        #region Time

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        #endregion
    }
}