using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Enum
{
    /// <summary>
    /// نوع دسترسی ها به هر بخش
    /// مثا: در پروژه به چه عملیاتی دسترسی دارد. مانند ایجاد و مشاهده و غیره
    /// </summary>
    public enum AccessType
    {
        Project = 1,
        ProjectAction  = 2
    }
}
