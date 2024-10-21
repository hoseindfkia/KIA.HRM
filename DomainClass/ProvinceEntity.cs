using Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    /// <summary>
    /// استان
    /// </summary>
    public class ProvinceEntity : BaseEntity<int>
    {
        #region Fields
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        #endregion

        #region Relations
        [InverseProperty(nameof(CityEntity.Province))]
        public virtual ICollection<CityEntity> Cities { get; set; } = new List<CityEntity>();
        #endregion
    }
}
