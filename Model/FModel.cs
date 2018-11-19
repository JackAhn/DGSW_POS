using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGSW_POS.Model
{
    //선택한 테이블 및 음식 정보 저장
    public class FModel
    {
        public List<FInfor> TData = new List<FInfor>();//테이블에서 선택한 음식, 수량, 가격 데이터, FInfor의 기본 구조 이용
        public int TNo { get; set; } //메인에서 클릭한 테이블 번호
        //public List<FInfor> TData { get; set; } 
    }
}
