using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSW_POS.Model
{

    //전체 음식 데이터 저장 모델
    public class FDModel
    {
        public List<AInfor> FData = new List<AInfor>();//음식 이름, 종류, 가격, 사진 경로 데이터 저장
    }

    public class AInfor
    {
        public string FName { get; set; } //음식 이름
        public int FPrice { get; set; } //음식 개당 가격
        public string FKind { get; set; } //음식 카테고리
        public string FPath { get; set; } //음식 사진 경로

        public AInfor(string FName, int FPrice, string FKind, string FPath)
        {
            this.FName = FName;
            this.FPrice = FPrice;
            this.FKind = FKind;
            this.FPath = FPath;
        }
    }
}
