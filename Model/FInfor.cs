using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSW_POS.Model
{
    public class FInfor
    {
        public string FName; //선택한 음식 이름
        public string FKind; //선택한 음식 카테고리
        public int FCount;//선택한 음식 수량
        public int FPrice; //선택한 음식 개당 가격
        public string FBuy; //선택한 결제방식

        public FInfor(string FName, string FKind, int FCount, int FPrice, string FBuy)
        {
            this.FName = FName;
            this.FKind = FKind;
            this.FCount = FCount;
            this.FPrice = FPrice;
            this.FBuy = FBuy;
        }

    }
}
