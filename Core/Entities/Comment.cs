using System;
using Core.Entities.Base;

namespace Core.Entities
{
    public class Comment : IEntity, ITiming
    {
        public int Id { get; set; }
        public string Content { get; set; }

        #region Navigator

        public virtual User User { get; set; }
        public string UserId { get; set; }

        public virtual Blog Blog { get; set; }
        public int BlogId { get; set; }

        #endregion

        #region Time

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        #endregion
    }
}