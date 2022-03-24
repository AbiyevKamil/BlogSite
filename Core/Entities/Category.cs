using System;
using System.Collections.Generic;
using Core.Entities.Base;

namespace Core.Entities
{
    public class Category : IEntity, ITiming
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Navigator

        public List<Blog> Blogs { get; set; }

        #endregion

        #region Time

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        #endregion
    }
}