#region << 版 本 注 释 >>
/*----------------------------------------------------------------
// Copyright (C) 2017 xx单位
// 版权所有。 
//
// 文件名：DBEntity
// 文件功能描述：
//
// 
// 创建者：名字 (eee27)
// 时间：2018/1/18 16:19:13
//
// 修改人：
// 时间：
// 修改说明：
//
// 修改人：
// 时间：
// 修改说明：
//
// 版本：V1.0.0
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chloe.Entity;

namespace NeedntWrite
{ 
    
        
        public class Item_Info
        {
        [ColumnAttribute("DEC_ITEM_ID", IsPrimaryKey = true)]
            public int DEC_ITEM_ID { get; set; }
            public string CHR_ITEM_NAME { get; set; }
            public double REL_ITEM_PRICE { get; set; }
            public string CHR_ITEM_SIZE { get; set; }
            public int IS_DELETE { get; set; }
            public string CHR_CRE_TIME { get; set; }
            public string CHR_APP_TIME { get; set; }
            public string CHR_DEL_TIME { get; set; }

        [NotMappedAttribute]
            public string NotMappedProperty { get; set; }
        }

    public class Item_Price_Info
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public double itemPrice { get; set; }
        public string itemSize { get; set; }
        public int itemNum { get; set; }
        public double itemAllPrice { get; set; }

    }



    class DBEntity
{
}
}
