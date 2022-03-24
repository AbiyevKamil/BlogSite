using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Base;

namespace Core.Entities
{
    public class Blog : IEntity, ITiming
    {
        public int Id { get; set; }

        [Required, MaxLength(60, ErrorMessage = "Blog title can't be longer than 60 characters.")]
        public string Title { get; set; }

        [Required] public string Content { get; set; }
        [Required] public string UniqueUrl { get; set; }
        [Required] public int ViewCount { get; set; } = 0;
        [Required] public string Banner { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        #region Time

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        #endregion

        #region Author

        public virtual User User { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Navigator

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        #endregion


        #region Navigator

        public virtual IList<Comment> Comments { get; set; }

        #endregion
    }
}