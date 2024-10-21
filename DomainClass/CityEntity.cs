using DomainClass.WorkReport;
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
    public class CityEntity : BaseEntity<int>
    {
        #region Fields
        [Required]
        [StringLength(Constant.StringLengthTitle)]
        public string Title { get; set; }
        public int ProvinceId { get; set; }
        #endregion

        #region Relations
        [InverseProperty(nameof(ProvinceEntity.Cities))]
        [ForeignKey(nameof(ProvinceId))]
        public virtual ProvinceEntity Province { get; set; }

        [InverseProperty(nameof(MissionEntity.City))]
        public virtual ICollection<MissionEntity> Missions { get; set; }
        #endregion

    }
}
