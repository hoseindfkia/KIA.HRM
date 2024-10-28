using Share.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.WorkReport
{
    public class MissionEntity : WorkReportBaseEntity
    {
        #region Fields

        /// <summary>
        /// پروژه ایی که روی آن کار شده
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// نوع ماموریت
        /// </summary>
        public MissionType MissionType { get; set; }

        /// <summary>
        /// شهر محل ماموریت
        /// </summary>
        public int CityId { get; set; }
        #endregion

        #region Relations
        [InverseProperty(nameof(MissionFileEntity.Mission))]
        public virtual ICollection<MissionFileEntity> MissionFiles { get; set; } = new List<MissionFileEntity>();


        #endregion


        #region Relations

        [InverseProperty(nameof(CityEntity.Missions))]
        [ForeignKey(nameof(CityId))]
        public virtual CityEntity City { get; set; }


        [ForeignKey(nameof(ProjectId))]
        [InverseProperty(nameof(ProjectEntity.Missions))]
        public virtual ProjectEntity Project { get; set; }
        #endregion



    }
}
